using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NUnit.Framework;
using Unity.VisualScripting;

// Struct containing references to other structs to be saved, will reference this when referencing file instead of other structs

[Serializable]
public struct Umbrella_Save_Struct
{
    public Player_Movement_Data_Struct player_Movement_Data;
    public Inventory_Data_Struct inventory_Data;
    public float current_Currency;
    //public List<Transform> scrap_Item_Transform = new List<Transform>();
}

public class Save_System : MonoBehaviour
{
    [Header("References")]
    Player_Movement player_Movement_Script;
    Inventory_Item_List player_Inventory_Script;
    public List<Transform> scrap_Item_Transform = new List<Transform>();
    
    // Houses other structs, will compile DataStruct into this to then be saved
    public Umbrella_Save_Struct umbrella_Struct;
    
    private string file_Path;
    
    #region --- Application Lifecycle Events ---

    private void Awake()
    {
        Load_Data();
    }

    private void OnApplicationQuit()
    {
        Save_Data();
    }

    #endregion
    
    private void Start()
    {
        Find_References();
        
        umbrella_Struct = new Umbrella_Save_Struct();
        
        file_Path = Application.persistentDataPath;
        Debug.Log(file_Path);
    }// end Start()

    // Saves all DataStruct in umbrella_Struct to file
    public void Save_Data()
    {
        Update_Umbrella_Data();
        
        // Serialize struct into Json strings
        string umbrella_Data_String = JsonUtility.ToJson(umbrella_Struct, true);
        // Write text file containing string value as plain text
        File.WriteAllText(file_Path + "/save_Data", umbrella_Data_String);
        Debug.Log($"File saved to: {file_Path}");
    }// end Save_Data()

    // Attempts to load all DataStruct from file to umbrella_Struct, then applies DataStruct
    public void Load_Data()
    {
      
        if (File.Exists(file_Path + "/save_Data"))
        {
            // Load contents as string
            string loaded_Json = File.ReadAllText(file_Path + "/save_Data");

            // Deserialize string into Umbrella struct
            umbrella_Struct = JsonUtility.FromJson<Umbrella_Save_Struct>(loaded_Json);
            Debug.Log("File loaded");
            
            Find_References();
            Apply_Umbrella_Data();
        }
        else
        {
            Debug.Log("Cannot load: File not found");
        }
        
    }// end Load_Data()
    
    // Find references to other scripts
    private void Find_References()
    {
        player_Movement_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        player_Inventory_Script = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory_Item_List>();
        
        scrap_Item_Transform.Clear();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Metal Scrap");

        foreach (GameObject target in targets)
        {
            scrap_Item_Transform.Add(target.transform);
        }
    }// end Find_References()

    // Updates all DataStruct to be saved in struct before writing to file
    private void Update_Umbrella_Data()
    {
        umbrella_Struct.player_Movement_Data = player_Movement_Script.player_Movement_Data;
        umbrella_Struct.inventory_Data = player_Inventory_Script.inventory_Data;
        umbrella_Struct.current_Currency = player_Inventory_Script.currency_Amount;
        
        /*
        for(int i = 0; i < scrap_Item_Transform.Count; i++)
        {
            Transform temp = scrap_Item_Transform[i];
            print(temp.position);
            
            umbrella_Struct.scrap_Item_Transform.Add(scrap_Item_Transform[i]);
        }
        */
    }// end Update_Umbrella_Data()

    // Takes DataStruct from file to be applied to various scripts when loading from file
    private void Apply_Umbrella_Data()
    {
        player_Movement_Script.transform.position = umbrella_Struct.player_Movement_Data.Position;
        player_Inventory_Script.inventory_Data = umbrella_Struct.inventory_Data;
        player_Inventory_Script.currency_Amount = umbrella_Struct.current_Currency;

        /*
        for (int i = 0; i < scrap_Item_Transform.Count; i++)
        {
            scrap_Item_Transform[i].transform.position = umbrella_Struct.scrap_Item_Transform[i].transform.position;
        }
        */
    }// end Apply_Umbrella_Data()
    
}// end Save_System

