using System.Collections;
using TMPro;
using UnityEngine;

public class Limb_Selector_Button : MonoBehaviour
{
    [SerializeField] private Limb_Selector_Script limb_Selector_Script;
    [Header("Currently Equipped")]
    public bool limb_Equipped;
    
    [Header("Object References")] 
    [SerializeField] private GameObject limb_Object;
    [SerializeField] private string limb_Type;
    
    [Header("UI Elements")]
    [SerializeField] private string limb_Name;
    private TextMeshProUGUI name_Text;

    private void Start()
    {
        //limb_Selector_Script = GameObject.FindWithTag("Limb Selector").GetComponent<Limb_Selector_Script>();
        
        name_Text = GetComponentInChildren<TextMeshProUGUI>();
        name_Text.text = limb_Name;
        
        if (limb_Selector_Script == null)
            Debug.LogError("Limb_Selector_Script is null!");

    }// end Start()

    public void Equip_Limb_Trigger()
    {
        if (limb_Equipped == true) return;
        
        limb_Selector_Script.Equip_Limb(limb_Object, limb_Type);
    }// end Equip_Limb()

    public void Start_Sequence()
    {
        if (limb_Equipped == true)
            limb_Selector_Script.Initialise_Limb(limb_Object, limb_Type);
    }
}// end Limb_Selector_Button
