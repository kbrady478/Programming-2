using System;
using UnityEngine;

public class Metal_Item : MonoBehaviour, IGetLoot
{
    [SerializeField] private Inventory_Item_Data metal_Scrap;
    private Inventory_Item_List inventory_Script;
    private GameObject inventory_Obj;
    
    
    private void Start()
    {
        inventory_Script = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory_Item_List>();
    }// end Start()

    public void Loot_Obtained()
    {
        inventory_Script.Add_Item_To_Inventory(metal_Scrap, gameObject);
    }// end Loot_Obtained()
    
}// end script
