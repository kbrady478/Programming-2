using System;
using UnityEngine;


interface IAttack_Triggered
{
    public void Attack();
}

public class Weapon_Manager : MonoBehaviour
{
    [SerializeField] private Grenade_Launcher grenade_Launcher_Script;
    private Input_Manager input_Manager;

    private void Start()
    {
        input_Manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Input_Manager>();
    }


    public void Attack_Input()
    {
        grenade_Launcher_Script.Shoot();
    }
    
    
    
    
}// end Weapon_Manager
