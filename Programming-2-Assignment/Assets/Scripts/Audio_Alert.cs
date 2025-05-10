using System;
using UnityEngine;

public class Audio_Alert : MonoBehaviour
{
    [SerializeField] private AudioSource audio_Source;
    [SerializeField] private LayerMask layer_Mask;
    
    public void Play_Sound()
    {
        
        audio_Source.Play();
        
        Collider[] enemies = Physics.OverlapSphere(transform.position, audio_Source.minDistance, layer_Mask);

        foreach (Collider enemy in enemies)
        {
            Enemy_State_Controller state_Controller = enemy.gameObject.GetComponent<Enemy_State_Controller>();
            Vector3 distance = enemy.transform.position - transform.position;
            
            if (distance.magnitude < audio_Source.minDistance / 3)
                state_Controller.Change_State(4);
            else 
                state_Controller.Change_State(3);
                
        }
    }
    
}// end Audio_Alert
