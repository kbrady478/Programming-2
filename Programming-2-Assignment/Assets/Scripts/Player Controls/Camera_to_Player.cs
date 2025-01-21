using System;
using UnityEngine;

public class Camera_to_Player : MonoBehaviour
{
    private GameObject target;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        gameObject.transform.position = target.transform.position;
    }
}// end script
