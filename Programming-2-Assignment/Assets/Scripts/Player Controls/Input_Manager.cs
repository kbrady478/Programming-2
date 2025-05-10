using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionAsset input_Action_Maps;
    [SerializeField] private Player_Movement player_Movement_Script;
    [SerializeField] private Burst_RIfle rifle_Script;
    [SerializeField] private Animator animator;
    private Weapon_Manager weapon_Manager_Script;
    private Save_System save_System_Script;
    private InputActionMap player_Action_Map;
    private InputActionMap ui_Action_Map;

    [SerializeField] private GameObject inventory_Screen;
    [SerializeField] private GameObject limb_Selection_Screen;

    [Header("Play Modes - Combat/Looting")] 
    [SerializeField] private LayerMask loot_Layer;
    [SerializeField] private float loot_Check_Radius; // Check if lootable items are within range
    [SerializeField] private float enemy_Check_Radius; // Essentially aim assist, accuracy will be dealt with on attack
    public bool combat_Mode = false;
    public Vector3 cursor_Position;


    private void Start()
    {
        weapon_Manager_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon_Manager>();
        save_System_Script = GameObject.FindGameObjectWithTag("Save Controller").GetComponent<Save_System>();
    }

    private void Update()
    {
        Get_Cursor_Position();
    }//  end Update()
    
    #region --- Action Map Basics ---
    
    private void Awake()
    {
        player_Action_Map = input_Action_Maps.FindActionMap("Player");
        ui_Action_Map = input_Action_Maps.FindActionMap("UI");
    }

    private void OnEnable()
    {
        Switch_To_Player_Controls();
    }
    #endregion

    #region --- Switch Control Scheme ---
    private void Switch_To_Player_Controls()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ui_Action_Map.Disable();
        player_Action_Map.Enable();
    }// end Switch_To_Player_Controls()

    private void Switch_To_UI_Controls()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        player_Action_Map.Disable();
        ui_Action_Map.Enable();
    }// end Switch_To_UI_Controls()
    
    #endregion

    #region --- Inventory Controls ---
    public void OnEnterInventory()
    {
        if (combat_Mode == true)
            combat_Mode = false;
            
        if (limb_Selection_Screen.activeInHierarchy == true)
            limb_Selection_Screen.SetActive(false);
        
        Switch_To_UI_Controls();
        inventory_Screen.SetActive(true);
    }

    public void OnExitInventory()
    {
        Switch_To_Player_Controls();
        inventory_Screen.SetActive(false);
    }
    #endregion

    #region --- Limb Selection Controls ---

    public void OnEnterLimbSelection()
    {
        if (combat_Mode == true)
            combat_Mode = false;
            
        if (inventory_Screen.activeInHierarchy == true)
            inventory_Screen.SetActive(false);
        
        Switch_To_UI_Controls();
        limb_Selection_Screen.SetActive(true);
    }

    public void OnExitLimbSelection()
    {
        Switch_To_Player_Controls();
        limb_Selection_Screen.SetActive(false);
    }
    #endregion
    
    #region --- Mouse Controls ---
    
    private void Get_Cursor_Position()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                cursor_Position = hit.point;
            }
        }// end Get_Cursor_Position()
    
    
    // Right click to swap between combat and looting
    public void OnChangeMode()
    {
        combat_Mode = !combat_Mode;
    }// end OnChangeMode()


    public void OnShoot()
    {
        animator.SetBool("is_Shooting", true);
        rifle_Script.Shoot_Projectile();
    }
    
    // Left click to interact with environment or attack
    public void OnInteract()
    {

        
        
        // For use when game was isometric
        /*
        if (combat_Mode == true) 
            weapon_Manager_Script.Attack_Input();
        */

        print("attempting pickup");
        Collider[] max_Item_Pickup = new Collider[1];
        // Change transform.position back to cursor when backtracking
        if (Physics.OverlapSphereNonAlloc(transform.position, loot_Check_Radius, max_Item_Pickup, loot_Layer) == 1)
        {
           foreach (Collider collider in max_Item_Pickup)
           {
               if (collider.gameObject.CompareTag("Metal Scrap"))
               {
                   if (collider.gameObject.TryGetComponent(out IGetLoot loot_Interface))
                   {
                       print("attemtping to get loot");
                       loot_Interface.Loot_Obtained();
                   }
               }    
           } 
        }
            
        
    }// end OnInteract()
    #endregion
    
    #region --- Save & Load ---

    public void OnSave()
    {
        save_System_Script.Save_Data();
    }// end OnSave()

    public void OnLoad()
    {
        save_System_Script.Load_Data();
    }// end OnLoad()
    
    #endregion
    
}// end Input_Manager

interface IGetLoot
{
    void Loot_Obtained();
}
