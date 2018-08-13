using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySearchController))]
public class EnemyControllerEditor : Editor {

    void OnSceneGUI()
    {
        EnemySearchController fow = (EnemySearchController)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.Radius);
        Vector3 AngleA = fow.DirFromAngle(-fow.Angle / 2, false);
        Vector3 AngleB = fow.DirFromAngle(fow.Angle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + AngleA * fow.Radius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + AngleB * fow.Radius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.VisibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}
