// This was taken from a project in Semester 1 and adapted
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Burst_RIfle : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Camera camera;

    [Header("Burst Rifle")] 
    [SerializeField] private GameObject burst_Rifle_Projectile_Prefab;
    [SerializeField] private Transform bullet_Spawn;
    [SerializeField] private float bullet_Force;
    [SerializeField] private Audio_Alert audio_Alert;
    public bool can_Fire;
    


    public void Shoot_Projectile()
    {
        if (can_Fire == false)
            return;
        
        can_Fire = false;
        
        audio_Alert.Play_Sound();
        
        // Find point to shoot projectile at
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        // Check if ray hits
        Vector3 target_Point;
        if (Physics.Raycast(ray, out hit))
            target_Point = hit.point;
        else
            target_Point = ray.GetPoint(50); // random value far away, for looking at sky etc
        
        // Calculate direction
        Vector3 projectile_Direction = target_Point - bullet_Spawn.position;
        
        GameObject projectile = Instantiate(burst_Rifle_Projectile_Prefab, bullet_Spawn.position, Quaternion.identity);
        projectile.transform.forward = projectile_Direction.normalized;
        projectile.GetComponent<Rigidbody>().AddForce(projectile_Direction.normalized * bullet_Force);
    }// end Shoot_Projectile()
    
    
}// end script
