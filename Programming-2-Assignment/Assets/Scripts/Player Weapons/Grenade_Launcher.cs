using System;
using UnityEngine;

public class Grenade_Launcher : MonoBehaviour
{

    [Header("References")] 
    private Input_Manager input_Manager_Script;
    [SerializeField] private GameObject projectile_Prefab;
    [SerializeField] private Transform projectile_Spawn;

    [Header("Projectile Force")]
    [SerializeField] private float projectile_Force;
    [SerializeField] private float projectile_Upward_Force;


    private void Start()
    {
        input_Manager_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Input_Manager>();
    }// end Start()

    public void Shoot()
    {
        print("bang");
        GameObject projectile = Instantiate(projectile_Prefab, projectile_Spawn.position, Quaternion.identity);
        projectile.transform.rotation = projectile_Spawn.transform.rotation;
        
        
        Rigidbody projectile_Rigidbody = projectile.GetComponent<Rigidbody>();
        
        Vector3 force_Direction = (input_Manager_Script.cursor_Position - projectile_Spawn.forward).normalized;
        
        projectile_Rigidbody.AddForce(force_Direction * projectile_Force + transform.up * projectile_Upward_Force, ForceMode.Impulse);
        
        
    }// end Shoot()
    
    
    
}// end Grenade_Launcher
