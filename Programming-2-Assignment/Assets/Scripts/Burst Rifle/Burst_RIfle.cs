using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst_RIfle : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Camera camera;

    [Header("Burst Rifle")] 
    [SerializeField] private GameObject burst_Rifle_Projectile_Prefab;
    [SerializeField] private GameObject muzzle_Flash;
    [SerializeField] private AudioSource audio_Source;
    [SerializeField] private AudioClip audio_Clip;
    [SerializeField] private Transform bullet_Spawn;
    [SerializeField] private float bullet_Force;
    [SerializeField] private float fire_Cooldown; // General cooldown inbetween bursts, wont be too long as its main weapon
    [SerializeField] private float burst_Cooldown; // Time between shots within the burst, will be very short
    [SerializeField] private int bullets_Per_Burst; // Amount of shots fired in single burst
    public bool can_Fire;
    


    public void Shoot_Projectile()
    {
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
