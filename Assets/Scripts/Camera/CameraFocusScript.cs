using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class CameraFocusScript : MonoBehaviour
{
    public Vector2 size = new Vector2(5, 5);
    public bool lockTop = true;
    public bool lockRight = true;
    public bool lockBottom = true;
    public bool lockLeft = true;

    public Rect GetRect()
    {
        Vector2 center = (Vector2)transform.position;
        return new Rect(center - size * 0.5f, size);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var rect = GetRect();

        var bottomLeft = new Vector3(rect.xMin, rect.yMin, 0);
        var bottomRight = new Vector3(rect.xMax, rect.yMin, 0);
        var topRight = new Vector3(rect.xMax, rect.yMax, 0);
        var topLeft = new Vector3(rect.xMin, rect.yMax, 0);

        Handles.color = Color.white.WithAlpha(0.15f);
        Handles.DrawWireCube(rect.center, rect.size);

        var color = Color.green;
        var thickness = 5;
        if (lockTop) DrawLine(topLeft, topRight, color, thickness);
        if (lockRight) DrawLine(topRight, bottomRight, color, thickness);
        if (lockBottom) DrawLine(bottomRight, bottomLeft, color, thickness);
        if (lockLeft) DrawLine(bottomLeft, topLeft, color, thickness);
    }

    private void DrawLine(Vector3 p1, Vector3 p2, Color color, float thickness)
    {
        Handles.color = color;
        Handles.DrawBezier(p1, p2, p1, p2, color, null, thickness);
    }
#endif
}

#if UNITY_EDITOR

[CustomEditor(typeof(CameraFocusScript))]
public class CameraFocusEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("To be able to see the camera focus rectangle, you must enable Gizmos.", MessageType.Info);
        EditorGUILayout.HelpBox("For optimal camera control, the locked sides should never be crossed by the player.\nTherefore, it is best if the locked sides are fully inside walls.", MessageType.Warning);
        base.OnInspectorGUI();
    }

    void OnSceneGUI()
    {
        var focusArea = (CameraFocusScript)target;

        Vector3 pos = focusArea.transform.position;
        Vector2 size = focusArea.size;

        // The center positions of each edge
        Vector3 left = pos + (size.x / 2) * Vector3.left;
        Vector3 right = pos + (size.x / 2) * Vector3.right;
        Vector3 top = pos + (size.y / 2) * Vector3.up;
        Vector3 bottom = pos + (size.y / 2) * Vector3.down;

        EditorGUI.BeginChangeCheck();

        // Add edge handles
        left = Handles.Slider(left, Vector3.left);
        right = Handles.Slider(right, Vector3.right);
        top = Handles.Slider(top, Vector3.up);
        bottom = Handles.Slider(bottom, Vector3.down);

        // Add drag handle in the center
        Handles.color = Color.yellow;
        Vector3 newPos = Handles.FreeMoveHandle(
            pos,
            HandleUtility.GetHandleSize(pos) * 0.1f,
            Vector3.zero,
            Handles.SphereHandleCap
        );

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObjects(new Object[] {
                focusArea.transform,
                focusArea
            }, "Modify CameraFocus");

            if (newPos != pos)
            {
                // If center moved, update position
                focusArea.transform.position = newPos;
            }
            else
            {
                // Otherwise, recompute size + center from edges
                float newWidth = Vector3.Distance(left, right);
                float newHeight = Vector3.Distance(top, bottom);

                var updatedCenter = new Vector3(
                    (left.x + right.x) / 2,
                    (top.y + bottom.y) / 2,
                    pos.z
                );

                focusArea.size = new Vector2(newWidth, newHeight);
                focusArea.transform.position = updatedCenter;
            }
        }
    }
}

#endif
