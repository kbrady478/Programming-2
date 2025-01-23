using System;
using UnityEngine;

public class Player_Status : MonoBehaviour
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

    private void Update_Health()
    {
        
        print(current_Health);
        
    }// end Update_Health()
    

}// end Player_Status
