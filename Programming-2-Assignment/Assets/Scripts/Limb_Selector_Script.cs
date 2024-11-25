using System;
using TMPro;
using UnityEngine;

public class Limb_Selector_Script : MonoBehaviour
{
    [Header("Limb Point References")] 
    [SerializeField] private GameObject head_Position;
    [SerializeField] private GameObject torso_Position;
    [SerializeField] private GameObject right_Arm_Position;
    [SerializeField] private GameObject left_Arm_Position;
    [SerializeField] private GameObject right_Leg_Position;
    [SerializeField] private GameObject left_Leg_Position;
    
    private GameObject player_Reference;
    private GameObject current_Head;
    private GameObject current_Torso;
    private GameObject current_Right_Arm;
    private GameObject current_Left_Arm;
    private GameObject current_Left_Leg;
    private GameObject current_Right_Leg;

    [Header("For stupid work around")]
    [SerializeField] private GameObject limb_Content_List;

    private void Start()
    {
        player_Reference = GameObject.FindGameObjectWithTag("Player");
        
        // Ridiculous work around because i couldnt initialise it within the Limb_Selector_Button script
        Limb_Selector_Button[] listings = limb_Content_List.GetComponentsInChildren<Limb_Selector_Button>();
        foreach (var listing in listings)
        {
            listing.GetComponent<Limb_Selector_Button>().Start_Sequence();
        }
    }

    public void Equip_Limb(GameObject limb_Object, string limb_Type)
    {
        if (limb_Type == "Head")
        {
            current_Head.gameObject.transform.SetParent(gameObject.transform);
            current_Head.gameObject.transform.localPosition = Vector3.zero;
            current_Head = limb_Object;
            current_Head.gameObject.transform.SetParent(head_Position.transform);
            current_Head.gameObject.transform.localPosition = Vector3.zero;
            current_Head.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Torso")
        {
            current_Torso.gameObject.transform.SetParent(gameObject.transform);
            current_Torso.gameObject.transform.localPosition = Vector3.zero;
            current_Torso = limb_Object;
            current_Torso.gameObject.transform.SetParent(torso_Position.transform);
            current_Torso.gameObject.transform.localPosition = Vector3.zero;
            current_Torso.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Right_Arm")
        {
            current_Right_Arm.gameObject.transform.SetParent(gameObject.transform);
            current_Right_Arm.gameObject.transform.localPosition = Vector3.zero;
            current_Right_Arm = limb_Object;
            current_Right_Arm.gameObject.transform.SetParent(right_Arm_Position.transform);
            current_Right_Arm.gameObject.transform.localPosition = Vector3.zero;
            current_Right_Arm.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Left_Arm")
        {
            current_Left_Arm.gameObject.transform.SetParent(gameObject.transform);
            current_Left_Arm.gameObject.transform.localPosition = Vector3.zero;
            current_Left_Arm = limb_Object;
            current_Left_Arm.gameObject.transform.SetParent(left_Arm_Position.transform);
            current_Left_Arm.gameObject.transform.localPosition = Vector3.zero;
            current_Left_Arm.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Right_Leg")
        {
            current_Right_Leg.gameObject.transform.SetParent(gameObject.transform);
            current_Right_Leg.gameObject.transform.localPosition = Vector3.zero;
            current_Right_Leg = limb_Object;
            current_Right_Leg.gameObject.transform.SetParent(right_Leg_Position.transform);
            current_Right_Leg.gameObject.transform.localPosition = Vector3.zero;
            current_Right_Leg.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Left_Leg")
        {
            current_Left_Leg.gameObject.transform.SetParent(gameObject.transform);
            current_Left_Leg.gameObject.transform.localPosition = Vector3.zero;
            current_Left_Leg = limb_Object;
            current_Left_Leg.gameObject.transform.SetParent(left_Leg_Position.transform);
            current_Left_Leg.gameObject.transform.localPosition = Vector3.zero;
            current_Left_Leg.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        } 


    }// end Equip_Limb()

    public void Initialise_Limb(GameObject limb_Object, string limb_Type)
    {
        if (limb_Type == "Head")
        {
            current_Head = limb_Object;
            current_Head.gameObject.transform.SetParent(head_Position.transform);
            current_Head.gameObject.transform.localPosition = Vector3.zero;
            current_Head.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Torso")
        {
            current_Torso = limb_Object;
            current_Torso.gameObject.transform.SetParent(torso_Position.transform);
            current_Torso.gameObject.transform.localPosition= Vector3.zero;
            current_Torso.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Right_Arm")
        {
            current_Right_Arm = limb_Object;
            current_Right_Arm.gameObject.transform.SetParent(right_Arm_Position.transform);
            current_Right_Arm.gameObject.transform.localPosition = Vector3.zero;
            current_Right_Arm.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Left_Arm")
        {
            current_Left_Arm = limb_Object;
            current_Left_Arm.gameObject.transform.SetParent(left_Arm_Position.transform);
            current_Left_Arm.gameObject.transform.localPosition = Vector3.zero;
            current_Left_Arm.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Right_Leg")
        {
            current_Right_Leg = limb_Object;
            current_Right_Leg.gameObject.transform.SetParent(right_Leg_Position.transform);
            current_Right_Leg.gameObject.transform.localPosition = Vector3.zero;
            current_Right_Leg.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        }
        if (limb_Type == "Left_Leg")
        {
            current_Left_Leg = limb_Object;
            current_Left_Leg.gameObject.transform.SetParent(left_Leg_Position.transform);
            current_Left_Leg.gameObject.transform.localPosition = Vector3.zero;
            current_Left_Leg.gameObject.transform.localRotation = Quaternion.Euler(-90, player_Reference.transform.rotation.y , -180);
        } 
    }// end Initialise_Limb()
    
}// end Limb_Selector_Script
