using UnityEngine;

interface IEnemy_Damage
{
    void Recieve_Enemy_Rifle_Damage();
}

public class Enemy_Rifle_Projectiles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter2D called. other's tag was {other.tag}.");

        if (other.tag == "Enemy Projectile" || other.tag == "Enemy")
            return;
        
        if (other.tag == "Player")
        {
            IEnemy_Damage player = other.gameObject.GetComponent<IEnemy_Damage>();
            player.Recieve_Enemy_Rifle_Damage();
        }
        
        Destroy(gameObject);
    }
    
}// end script
