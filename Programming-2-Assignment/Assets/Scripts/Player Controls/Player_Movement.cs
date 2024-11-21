// Tutorial used for iso movement: https://www.youtube.com/watch?v=8ZxVBCvJDWk&t=52s

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Rigidbody rb;
    private InputSystem_Actions input_System;
    
    [Header("Walk Speeds")]
    [SerializeField] private float walk_Speed;
    [SerializeField] private float turn_Speed;
   
    private Vector3 input_Vector;
    
    
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

    #region --- Basic Updates ---
    
    private void Update()
    {
        Take_Input();
        Rotate_Character();
    }// end Update()

    private void FixedUpdate()
    {
        Move_Character();
    }// end FixedUpdate()

    #endregion
    
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
    
    
}// end script
