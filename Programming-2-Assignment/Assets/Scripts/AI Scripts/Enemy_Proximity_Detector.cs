using System;
using UnityEngine;

public class Enemy_Proximity_Detector : MonoBehaviour
{
    [SerializeField] private Enemy_State_Controller state_Controller;
    [SerializeField] private LayerMask layer_Mask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player too close");
            state_Controller.Change_State(4);
 
        }
    }
    
}// end Enemy_Proximity_Detector
