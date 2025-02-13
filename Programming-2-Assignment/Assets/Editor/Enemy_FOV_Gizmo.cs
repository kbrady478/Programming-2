using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Enemy_FOV))]
public class Enemy_FOV_Gizmo : Editor
{
    private void OnSceneGUI()
    {
        Enemy_FOV fov = (Enemy_FOV)target; 
        
        // Draw detection sphere range
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        // Calculate both sides of the view cone
        Vector3 view_Angle_Left = Direction_From_Angle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 view_Angle_Right = Direction_From_Angle(fov.transform.eulerAngles.y, fov.angle / 2);
        
        // Draw view cone
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + view_Angle_Left * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + view_Angle_Right * fov.radius);

        if (fov.entity_Visible)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.entity_Object.transform.position);
        }
    }// end OnSceneGUI()


    private Vector3 Direction_From_Angle(float euler_Y, float angle_In_Degrees)
    {
        angle_In_Degrees += euler_Y;
        
        return new Vector3(Mathf.Sin(angle_In_Degrees * Mathf.Deg2Rad), 0, Mathf.Cos(angle_In_Degrees * Mathf.Deg2Rad));
        
    }// end Direction_From_Angle()
    
    
}// end Enemy_FOV_Gizmo
