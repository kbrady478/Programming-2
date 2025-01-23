using System;
using System.Collections;
using UnityEngine;

public class Security_Turret : MonoBehaviour
{

    [Header("References")] 
    private GameObject target;
    [SerializeField] private GameObject projectile_Prefab;
    [SerializeField] private Transform projectile_Spawn;
    
    [Header("General")] 
    [SerializeField] private float rotation_Speed;
    [SerializeField] private float fire_Rate;
    private bool can_Fire = true;
    
    [Header("Projectile Force")]
    [SerializeField] private float projectile_Force;
    [SerializeField] private float projectile_Upward_Force;
    private bool enemy_In_Range;
    
    
    private void Update()
    {

    }// end Update()

    private IEnumerator Follow_Target()
    {
        while (enemy_In_Range == true)
        {
            if (can_Fire == true)
            {
                Shoot();
                can_Fire = false;
                Invoke("Reset_Fire", fire_Rate);
            }
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotation_Speed * Time.deltaTime);            
            yield return null; 
        }

        
    }// end Follow_Target()

    private void Shoot()
    {
        print("bang");
        GameObject projectile = Instantiate(projectile_Prefab, projectile_Spawn.position, Quaternion.identity);
        projectile.transform.rotation = projectile_Spawn.transform.rotation;
        
        Rigidbody projectile_Rigidbody = projectile.GetComponent<Rigidbody>();
        
        projectile_Rigidbody.AddForce(projectile_Spawn.transform.forward * projectile_Force + transform.up * projectile_Upward_Force, ForceMode.Impulse);
        
    }// end Shoot()

    private void Reset_Fire()
    {
        can_Fire = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy_In_Range = true;
            target = other.gameObject;
            StartCoroutine(Follow_Target());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy_In_Range = false;
            target = null;
            StopCoroutine(Follow_Target());
        }
    }
}// end Security_Turret
