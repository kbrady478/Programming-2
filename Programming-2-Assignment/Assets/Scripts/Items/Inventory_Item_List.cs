// Tutorial Used: https://www.youtube.com/watch?v=v3P5kSDJiKE

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Item_List : MonoBehaviour
{
    [SerializeField] private GameObject content = default;
    [SerializeField] private GameObject menu_Item_Template = default;
    [SerializeField] private List<object> inventory_List = default;


    private void Start()
    {
        //Populate_Inventory_Items();
    }// end Start()

    // Populates the list with items, only called in Awake()
    
    // !! Come back to when scene changes are implemented
    private void Populate_Inventory_Items()
    {
        for (int i = 0; i < inventory_List.Count; i++)
        {
            //Add_Item_To_Inventory(inventory_List[i]);
        }   
    }// end Populate_Inventory_Item()

    // Add item to the inventory list, called when picking up new item
    public void Add_Item_To_Inventory(Inventory_Item_Data item_Data, GameObject item_Object)
    {
        
        GameObject new_Item_Listing = Instantiate(menu_Item_Template, transform.position, transform.rotation);

        string label = $"   {item_Data.name}";
        new_Item_Listing.name = label;
        new_Item_Listing.transform.SetParent(content.transform, true);
        new_Item_Listing.SetActive(true);
        new_Item_Listing.GetComponentInChildren<TextMeshProUGUI>().text = label;
        // Reset scale because it goes weird
        new_Item_Listing.transform.localScale = Vector3.one;
    }// end Add_Item_To_Inventory()
    
}// end script
