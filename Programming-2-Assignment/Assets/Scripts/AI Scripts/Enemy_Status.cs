using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_Status : MonoBehaviour
{
    [FormerlySerializedAs("movement_Script")]
    [Header("General")] 
    [SerializeField] private Enemy_State_Controller stateControllerScript;
    [SerializeField] private int total_HP;
    
    private Collider collider;
    private SkinnedMeshRenderer[] renderers;
    private Color hit_Color = Color.green;
    private Color base_Color;
    
    public int current_HP;
    
    private void Start()
    {

        current_HP = total_HP;
        collider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        base_Color = Color.white;
    }

    private void Update()
    {
        if (current_HP <= 0)
        {
            stateControllerScript.Change_State(6);
            collider.enabled = false;
        }
    }
    
    
}// end Enemy_Status
