using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Status : MonoBehaviour, IEnemy_Damage
{
    [Header("General")]
    [SerializeField] private int max_Health;
    [SerializeField] private float current_Health;


    private void Start()
    {
        current_Health = max_Health;
    }// end Start()

    public void Take_Damage(float damage)
    {
        current_Health -= damage;
        Update_Health();
    }// end Take_Damage()

    private void Update()
    {
        if (current_Health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update_Health()
    {

        print(current_Health);
        
    }// end Update_Health()

    public void Recieve_Enemy_Rifle_Damage()
    {
        current_Health -= 15;
    }

}// end Player_Status
