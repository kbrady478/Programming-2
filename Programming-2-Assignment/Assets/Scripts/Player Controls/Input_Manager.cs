using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private InputActionAsset input_Action_Maps;
    [SerializeField] private Player_Movement player_Movement_Script;
    private InputActionMap player_Action_Map;
    private InputActionMap ui_Action_Map;

    [SerializeField] private GameObject inventory_Screen;

    [Header("Play Modes - Combat/Looting")] 
    [SerializeField] private LayerMask loot_Layer;
    [SerializeField] private float loot_Check_Radius; // Check if lootable items are within range
    [SerializeField] private float enemy_Check_Radius; // Essentially aim assist, accuracy will be dealt with on attack
    public bool combat_Mode = false;
    public Vector3 cursor_Position;

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
        ui_Action_Map.Disable();
        player_Action_Map.Enable();
    }// end Switch_To_Player_Controls()

    private void Switch_To_UI_Controls()
    {
        player_Action_Map.Disable();
        ui_Action_Map.Enable();
    }// end Switch_To_UI_Controls()
    
    #endregion

    #region --- Inventory Controls ---
    public void OnEnterInventory()
    {
        if (combat_Mode == true)
            combat_Mode = false;
            
        Switch_To_UI_Controls();
        inventory_Screen.SetActive(true);
    }

    public void OnExitInventory()
    {
        Switch_To_Player_Controls();
        inventory_Screen.SetActive(false);
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

    // Left click to interact, with environment or attack
    public void OnInteract()
    {
        print("click registered");
        if (combat_Mode == true) return;

        if (combat_Mode == false)
        {
            Collider[] hit_Colliders = Physics.OverlapSphere(cursor_Position, loot_Check_Radius, loot_Layer);

            foreach (Collider collider in hit_Colliders)
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
        }// end pick up item
        
    }// end OnInteract()
    #endregion
    
}// end Input_Manager

interface IGetLoot
{
    void Loot_Obtained();
}
