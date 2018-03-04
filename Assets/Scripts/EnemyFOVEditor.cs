using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyFOV))]
public class FieldOfViewEditor : Editor
{
    Transform trackPlayerPosition;

    void OnSceneGUI()
    {
        EnemyFOV fow = (EnemyFOV)target;

        // Draw the circle for the max distance of field of view
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);

        // Draw the cone for field of view of the object
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2 + 90, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2 + 90, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        // Draws a line from enemy to object's tracking position for each object in the enemy's field of view
        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visiblePlayers)
        {
            trackPlayerPosition = visibleTarget.Find("TrackPlayerPosition");
            Handles.DrawLine(fow.transform.position, trackPlayerPosition.position);
        }
    }
}
