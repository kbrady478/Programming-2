using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Item", order = 1)]
public class Inventory_Item_Data : ScriptableObject
{
    public new string name;
    public string item_Type;
    public int place_In_Inv = -1;


}// end script
