using System;
using UnityEngine;
using UnityEngine.Serialization;

public class in_Loadout : MonoBehaviour
{
    [SerializeField] private GameObject equip_Point;
    private GameObject equipable_Container;
    private GameObject player_Controller;
    public bool within_Loadout;
    
    void Start()
    {
        equipable_Container = GameObject.FindGameObjectWithTag("Equipable Container");
        player_Controller = GameObject.FindGameObjectWithTag("Player");
        Change_Loadout_Status();
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
            gameObject.transform.parent = equip_Point.transform;
            gameObject.transform.position = equip_Point.transform.position;
            
            
            gameObject.transform.rotation = Quaternion.Euler(0,-player_Controller.transform.rotation.eulerAngles.y,0);
        }
        if (within_Loadout == false)
        {
            gameObject.transform.parent = equipable_Container.transform;
            gameObject.transform.position = equipable_Container.transform.position;
        }
    }
    
}// end script
