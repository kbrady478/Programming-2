// Tutorial used for iso movement: https://www.youtube.com/watch?v=8ZxVBCvJDWk&t=52s

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player_Movement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Rigidbody rb;
    private InputSystem_Actions input_System;
    private Vector3 input_Vector;
    
    [Header("Walk Speeds")]
    [SerializeField] private float walk_Speed;
    [SerializeField] private float turn_Speed;
    
    [Header("Jumping")] 
    [SerializeField] private float jump_Force;
    
    [Header("Ground Check")]
    [SerializeField] private LayerMask ground_Layer;
    [SerializeField] private Transform ground_Check_Pos;
    [SerializeField] private float ground_Check_Radius;
    [SerializeField] private float jump_Cooldown;
    
    [Header("Bool Checks - Exposed for viewing")]
    [SerializeField] private bool is_Jumping;
    [SerializeField] private bool is_Grounded = false;
    [SerializeField] private bool can_Jump = true;
    
    
    #region --- Input System Setup ---
    
    private void Awake()
    {
        input_System = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input_System.Player.Enable();
    }

    private void OnDisable()
    {
        input_System.Player.Disable();
    }
    
    #endregion
    
    
    private void FixedUpdate()
    {
        is_Grounded = Physics.CheckSphere(ground_Check_Pos.position, ground_Check_Radius, ground_Layer);
        
        if (is_Jumping && is_Grounded && can_Jump)
            is_Jumping = false;
        
        Take_Input();

        if (is_Jumping) return;
        Rotate_Character();      
        Move_Character();
    }// end FixedUpdate()
    
    
    private void Take_Input()
    {
        input_Vector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
    }// end Take_Input()

    
    private void Rotate_Character()
    {
        if (input_Vector == Vector3.zero) return;
        
        // For changing the movement so that forward is up on screen, in line with Iso angle
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        
        // Input influenced by rotation matrix
        var skewed_Input = matrix.MultiplyPoint3x4(input_Vector);
        
        
        var current_Rotation = Quaternion.LookRotation(skewed_Input, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, current_Rotation, turn_Speed * Time.deltaTime );
    }// end Rotate_Character
    
    
    private void Move_Character()
    {
        Vector3 direction = input_Vector.normalized;
        
        rb.MovePosition(rb.position + transform.forward * direction.magnitude * walk_Speed * Time.fixedDeltaTime);
    }// end Move_Character
    
    
    public void OnJump()
    {
        if (is_Grounded == false || can_Jump == false) return;

        is_Jumping = true;
        can_Jump = false;
        
        Invoke("Reset_Jump", jump_Cooldown);
        
        rb.AddForce(jump_Force * Vector3.up, ForceMode.Impulse);
        rb.AddForce((jump_Force * 1.5f) * gameObject.transform.forward, ForceMode.Impulse);
    }// end OnJump()

    
    private void Reset_Jump()
    {
        can_Jump = true;
    }// end Reset_Jump()
    
}// end script
