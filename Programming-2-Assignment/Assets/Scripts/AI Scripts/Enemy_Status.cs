using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_Status : MonoBehaviour, IBurst_Rifle_Damage
{
    [Header("General")] 
    [SerializeField] private int total_HP;
    

    public int current_HP;
    
    private void Start()
    {

        current_HP = total_HP;

    }

    private void Update()
    {
        if (current_HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Recieve_Burst_Rifle_Damage()
    {
        current_HP -= 35;
    }
    
}// end Enemy_Status
