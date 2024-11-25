// Tutorial Used: https://www.youtube.com/watch?v=v3P5kSDJiKE

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct Inventory_Object
{
    public Inventory_Item_Data data;
    public GameObject game_Object;
}

public class Inventory_Item_List : MonoBehaviour
{
    [SerializeField] private GameObject content = default;
    [SerializeField] private GameObject menu_Item_Template = default;
    private List<Inventory_Object> inventory_List = new List<Inventory_Object>();


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

    private void Update()
    {
        // just for testing list
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory_List.Count == 0) return;
            
            for (int i = 0; i < inventory_List.Count; i++)
            {
                Debug.Log(inventory_List[i].data.name);
            } 
        }
    }

    // Add item to the inventory list, called when picking up new item
    public void Add_Item_To_Inventory(Inventory_Item_Data item_Data, GameObject item_Object)
    {
        // Assign item data and transform to struct
        Inventory_Object new_Inventory_Object = new Inventory_Object();
        
        new_Inventory_Object.data = item_Data;
        new_Inventory_Object.game_Object = item_Object;
        
        // Add to list, relocate off-screen to inventory transform
        inventory_List.Add(new_Inventory_Object);
        new_Inventory_Object.game_Object.transform.SetParent(gameObject.transform);
        new_Inventory_Object.game_Object.transform.localPosition = Vector3.zero;
        
        // Add listing to inventory screen    
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
