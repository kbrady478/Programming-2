using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private InputActionAsset input_Action_Maps;
    private InputActionMap player_Action_Map;
    private InputActionMap ui_Action_Map;

    [SerializeField] private GameObject inventory_Screen;
    

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
    
}// end script
