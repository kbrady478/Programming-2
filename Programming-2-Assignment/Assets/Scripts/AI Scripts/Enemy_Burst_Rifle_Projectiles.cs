using UnityEngine;

public class Enemy_Burst_Rifle_Projectiles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    
}// end script
