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
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject menu_Item_Template;
    private List<Inventory_Object> inventory_Object_List = new List<Inventory_Object>();
    private List<GameObject> menu_Item_List = new List<GameObject>();
    private GameObject world_Item_Container;
    private GameObject removed_Item_Dispenser;
    
    private void Start()
    {
        world_Item_Container = GameObject.FindWithTag("World Item Container");
        removed_Item_Dispenser = GameObject.FindWithTag("Removed Item Dispenser");
    }// end Start()

    // Populates the list with items, only called in Awake()
    
    // !! Come back to when scene changes are implemented
    private void Populate_Inventory_Items()
    {
        for (int i = 0; i < inventory_Object_List.Count; i++)
        {
            //Add_Item_To_Inventory(inventory_Object_List[i]);
        }   
    }// end Populate_Inventory_Item()

    private void Update()
    {
        // just for testing list
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory_Object_List.Count == 0) return;
            
            for (int i = 0; i < inventory_Object_List.Count + 1; i++)
            {
                if (menu_Item_List[i].gameObject.GetComponentInChildren<Remove_From_Inventory>() == null) return;
                Debug.Log(inventory_Object_List[i].data.name);
                Debug.Log(menu_Item_List[i].gameObject.GetComponentInChildren<Remove_From_Inventory>().position_In_List);
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
        
        // Add to list
        inventory_Object_List.Add(new_Inventory_Object);
        
        // Assign place in the list once known
        new_Inventory_Object.data.place_In_Inv = inventory_Object_List.Count - 1;
        
        // Relocate object to inventory location in scene
        new_Inventory_Object.game_Object.transform.SetParent(gameObject.transform);
        new_Inventory_Object.game_Object.transform.localPosition = Vector3.zero;
        
        Change_Item_RigidBody_Active(new_Inventory_Object.data.place_In_Inv);
        
        // Add listing to inventory screen    
        GameObject new_Item_Listing = Instantiate(menu_Item_Template, transform.position, transform.rotation);
        menu_Item_List.Add(new_Item_Listing);
        
        string label = $"   {item_Data.name}";
        new_Item_Listing.name = label;
        new_Item_Listing.transform.SetParent(content.transform, true);
        new_Item_Listing.SetActive(true);
        
        new_Item_Listing.GetComponentInChildren<TextMeshProUGUI>().text = label;
        
        new_Item_Listing.GetComponentInChildren<Remove_From_Inventory>().position_In_List = inventory_Object_List.Count - 1;
        
        // Reset scale because it goes weird
        new_Item_Listing.transform.localScale = Vector3.one;
        
    }// end Add_Item_To_Inventory()
    
    public void Remove_Item_From_Inventory(int position_In_Inventory)
    {
        // Destroy listing in UI, first to stop bug where list shifts and deletes next item instead
        Destroy(menu_Item_List[position_In_Inventory]);        
        
        // Spit item out
        Change_Item_RigidBody_Active(position_In_Inventory);
        inventory_Object_List[position_In_Inventory].game_Object.gameObject.transform.position = removed_Item_Dispenser.transform.position;
        inventory_Object_List[position_In_Inventory].game_Object.gameObject.GetComponent<Rigidbody>().AddForce(removed_Item_Dispenser.transform.position * 1.1f, ForceMode.Impulse);
        
        // Move out of inventory back to world
        inventory_Object_List[position_In_Inventory].game_Object.transform.SetParent(world_Item_Container.transform);
        
        // Remove from internal lists
        inventory_Object_List.RemoveAt(position_In_Inventory);
        menu_Item_List.RemoveAt(position_In_Inventory);
        
        // Adjust remaining items self-contained positions
        for (int i = position_In_Inventory; i < inventory_Object_List.Count; i++)
            menu_Item_List[i].gameObject.GetComponentInChildren<Remove_From_Inventory>().position_In_List -= 1;
        
    }// end Remove_Item_From_Inventory()

    private void Change_Item_RigidBody_Active(int position_In_Inventory)
    {
        print("enter rb check");
        if (inventory_Object_List[position_In_Inventory].game_Object.gameObject.GetComponent<Rigidbody>().isKinematic == true)
            inventory_Object_List[position_In_Inventory].game_Object.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        
        else if (inventory_Object_List[position_In_Inventory].game_Object.gameObject.GetComponent<Rigidbody>().isKinematic == false)
            inventory_Object_List[position_In_Inventory].game_Object.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }// end Change_Item_Rigidbody_Active()
    
    
}// end script
