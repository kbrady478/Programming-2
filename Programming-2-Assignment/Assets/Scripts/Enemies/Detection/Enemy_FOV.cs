using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class Enemy_FOV : MonoBehaviour
{
    [FormerlySerializedAs("player_Object")]
    [Header("References")]
    [HideInInspector] public GameObject entity_Object;
    [SerializeField] private LayerMask entity_Layer;
    [SerializeField] private LayerMask obstruction_Layer;
    
    
    [Header("FOV Range")]
    public float radius;
    [Range(0, 360)]
    public float angle;

    [Header("Bool Checks")]
    public bool entity_Visible;
    
    private void Start()
    {
        StartCoroutine(FOV_Routine());
    }// end Start()

    // Coroutine with a delay so it is not checked every frame for performance
    private IEnumerator FOV_Routine()
    {
        // Every 0.2 seconds, checked 5 times per second instead of every frame
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV_Detection();
        }
        
    }// end FOV_Routine()

    private void FOV_Detection()
    {
        Collider[] entities_In_Range = Physics.OverlapSphere(transform.position, radius, entity_Layer);

        // *** Will need to change to check all of array if adding ability to target entities other than players
        if (entities_In_Range.Length != 0)
        {
            Transform target = entities_In_Range[0].transform;
            Vector3 direction_To_Target = (target.position - transform.position).normalized;

            // If entity is within view angle
            if (Vector3.Angle(transform.forward, direction_To_Target) < angle / 2)
            {
                float distance_To_Target = Vector3.Distance(transform.position, target.position);

                // If entity is not obstructed
                if (!Physics.Raycast(transform.position, direction_To_Target, distance_To_Target, obstruction_Layer))
                {
                    entity_Visible = true;
                    entity_Object = entities_In_Range[0].gameObject;
                }
                else
                {
                    entity_Visible = false;
                    entity_Object = null;  
                }
                
            }// end check if entity within view angle
            
            else
            {
                entity_Visible = false;
                entity_Object = null;
            }
            
        } // end check if entity detected
        
        // Reset if entity no longer in view
        else if (entity_Visible)
        {
            entity_Visible = false;
            entity_Object = null;
        }
        
    }// end FOV_Detection()
    
}// end Enemy_FOV
