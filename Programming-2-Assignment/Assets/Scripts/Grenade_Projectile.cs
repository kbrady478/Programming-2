using System;
using UnityEngine;

public class Grenade_Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int remaining_Bounces;


    private void OnCollisionEnter(Collision other)
    {
        if (remaining_Bounces == 0)
            Explode();
        
        else if (other.gameObject.CompareTag("Player"))
        {
            Player_Status status_Script = other.gameObject.GetComponent<Player_Status>();
            status_Script.Take_Damage(damage);
            
            
            Explode();
        }

        else
            remaining_Bounces--;
        
    }// end OnCollisionEnter()


    private void Explode()
    {
        print("boom");
        Destroy(gameObject);
    }// end Explode()
    
    
}// end Grenade_Projectile
