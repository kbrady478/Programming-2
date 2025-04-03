using System;
using UnityEngine;
using System.IO;

// Struct containing references to other structs to be saved, will reference this when referencing file instead of other structs

[Serializable]
public struct Umbrella_Save_Struct
{
    public Player_Movement_Data_Struct player_Movement_Data;
}

public class Save_System : MonoBehaviour
{
    [Header("References")]
    Player_Movement player_Movement_Script;
    
    // Houses other structs, will compile data into this to then be saved
    public Umbrella_Save_Struct umbrella_Struct;
    
    private string file_Path;
    
    
    private void Start()
    {
        Find_References();
        
        umbrella_Struct = new Umbrella_Save_Struct();
        
        file_Path = Application.persistentDataPath;
        Debug.Log(file_Path);
    }// end Start()

    // Saves all data in umbrella_Struct to file
    public void Save_Data()
    {
        Update_Umbrella_Data();
        
        // Serialize struct into Json strings
        string player_Movement_Json = JsonUtility.ToJson(umbrella_Struct, true);
        // Write text file containing string value as plain text
        File.WriteAllText(file_Path + "/save_Data", player_Movement_Json);
        Debug.Log("File saved");
    }// end Save_Data()

    // Attempts to load all data from file to umbrella_Struct, then applies data
    public void Load_Data()
    {
      
        if (File.Exists(file_Path + "/save_Data"))
        {
            // Load contents as string
            string loaded_Json = File.ReadAllText(file_Path + "/save_Data");

            // Deserialize string into Umbrella struct
            umbrella_Struct = JsonUtility.FromJson<Umbrella_Save_Struct>(loaded_Json);
            Debug.Log("File loaded");
            
            Apply_Umbrella_Data();
        }
        
    }// end Load_Data()
    
    // Find references to other scripts
    private void Find_References()
    {
        player_Movement_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
    }// end Find_References()

    // Updates all data to be saved in struct before writing to file
    private void Update_Umbrella_Data()
    {
        umbrella_Struct.player_Movement_Data = player_Movement_Script.player_Movement_Data;
    }// end Update_Umbrella_Data()

    // Takes data from file to be applied to various scripts when loading from file
    private void Apply_Umbrella_Data()
    {
        player_Movement_Script.transform.position = umbrella_Struct.player_Movement_Data.Position;
    }// end Apply_Umbrella_Data()
    
}// end Save_System

