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
    [SerializeField] private List<Inventory_Item_Data> inventory_List = default;


    private void Start()
    {
        Populate_Inventory_Items();
    }// end Start()

    // Populates the list with items, only called in Awake()
    private void Populate_Inventory_Items()
    {
        for (int i = 0; i < inventory_List.Count; i++)
        {
            Add_Item_To_Inventory(inventory_List[i]);
        }   
    }// end Populate_Inventory_Item()

    // Add item to the inventory list, called when picking up new item
    private void Add_Item_To_Inventory(Inventory_Item_Data item)
    {
        GameObject new_Item = Instantiate(menu_Item_Template, transform.position, transform.rotation);

        string label = $"   {item.name}";
        new_Item.name = label;
        new_Item.transform.SetParent(content.transform, true);
        new_Item.SetActive(true);
        new_Item.GetComponentInChildren<TextMeshProUGUI>().text = label;
        // Reset scale because it goes weird
        new_Item.transform.localScale = Vector3.one;
    }// end Add_Item_To_Inventory()
    
}// end script
