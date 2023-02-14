using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {

        FieldOfView fov = (FieldOfView)target;
        //view Radius
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        //report Radius
        Handles.color = Color.magenta;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.reportRadius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.viewAngle / 2);

        Vector3 viewAngle03 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.alertAngle / 2);
        Vector3 viewAngle04 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.alertAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.viewRadius);

        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle03 * fov.alertRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle04 * fov.alertRadius);

        if (fov.isPlayerSight)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerGO.transform.position);
        }

        if (fov.isPlayerSeen)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, fov.playerGO.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
