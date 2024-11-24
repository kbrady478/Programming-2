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
    public bool combat_Mode = false;
    public Vector3 cursor_Position;

    private void Update()
    {
        Get_Cursor_Position();
    }//  end Update()

    private void Get_Cursor_Position()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            cursor_Position = hit.point;
        }
    }// end Get_Cursor_Position()
    
    #region --- Action Map Basics ---

    private void Start()
    {
        Switch_To_Player_Controls();
    }

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
        Switch_To_UI_Controls();
        inventory_Screen.SetActive(true);
    }

    public void OnExitInventory()
    {
        Switch_To_Player_Controls();
        inventory_Screen.SetActive(false);
    }
    #endregion

    // Right click to swap between combat and looting
    public void OnChangeMode()
    {
        combat_Mode = !combat_Mode;
    }// end OnChangeMode()
    
    
}// end script
