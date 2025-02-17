using System;
using UnityEngine;

public class Punchable_Obj : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("Punch", true);
        Invoke("Reset_Punch", .25f);
    }
    

    private void Reset_Punch()
    {
        anim.SetBool("Punch", false);
    }
}
