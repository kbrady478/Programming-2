using System;
using UnityEngine;

public class in_Loadout : MonoBehaviour
{
    private GameObject hold_Point;
    private GameObject equipable_Container;
    private GameObject player_Controller;
    public bool within_Loadout;
    
    void Start()
    {
        hold_Point = GameObject.FindGameObjectWithTag("Hold Point");    
        equipable_Container = GameObject.FindGameObjectWithTag("Equipable Container");
        player_Controller = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            within_Loadout = !within_Loadout;
            Change_Loadout_Status();    
        }
    }

    public void Change_Loadout_Status()
    {
        if (within_Loadout == true)
        {
            gameObject.transform.parent = hold_Point.transform;
            gameObject.transform.position = hold_Point.transform.position;
            
            // Must subtract 45 from the Y to ensure it is facing the correct direction w/ Iso view
            gameObject.transform.rotation = Quaternion.Euler(player_Controller.transform.rotation.x, player_Controller.transform.rotation.y - 45, player_Controller.transform.rotation.z);
        }
        if (within_Loadout == false)
        {
            gameObject.transform.parent = equipable_Container.transform;
            gameObject.transform.position = equipable_Container.transform.position;
        }
    }
    
}// end script
