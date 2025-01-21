using System;
using UnityEngine;

public class Remove_From_Inventory : MonoBehaviour
{
    private Inventory_Item_List inventory_Item_List;
    public int position_In_List = 0;
    
    private void Start()
    {
        inventory_Item_List = GameObject.FindWithTag("Inventory").GetComponent<Inventory_Item_List>();
    }

    public void Trigger_Remove_From_Inventory()
    {
        inventory_Item_List.Remove_Item_From_Inventory(position_In_List);    
    }
    
}
