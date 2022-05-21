using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Movable), true)]
public class Movable_Editor : Editor
{

    public Texture2D pointSprite;
    public bool pathEdit = false;
    public bool boundryEdit = false;
    public float handleSize = 1;
    private Movable lastInspected;
    private Movable currentTarget;
    static Vector2 lastPos;
    Vector2 handle_01, handle_02;

    public Dictionary<string, Vector2> handles = new Dictionary<string, Vector2>();

    Vector2 Bottom_L, Bottom_R, Top_L, Top_R;

    public GUIStyle speed_label;



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        speed_label = new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Bold,
            border = new RectOffset(5, 5, 5, 5),
        };

        Rect windowSize = new Rect(0, 0, EditorGUIUtility.currentViewWidth / 2, 35);

        GUILayout.Space(15);
        GUILayout.BeginHorizontal();       
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Edit Point", GUILayout.Width(windowSize.width / 2), GUILayout.Height(35)))
        {
            pathEdit = !pathEdit;
            EditorWindow view = EditorWindow.GetWindow<SceneView>();
            view.Repaint();
        }
        if (GUILayout.Button("Add Point", GUILayout.Width(windowSize.width / 2), GUILayout.Height(35)))
        {
            currentTarget.Add_New_Link();
            pathEdit = true;
            EditorWindow view = EditorWindow.GetWindow<SceneView>();
            view.Repaint();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();       
    }

    public void OnSceneGUI()
    {
        if ((Movable)target != lastInspected || currentTarget == null)
        {
            currentTarget = (Movable)target;
        }

        #region PATH EDITOR
        if (pathEdit)
        {
            //  [0 1] [1 2] [2 3] [3 4] [4 5]
            //  0a|0b   1b    2b    3b    4b
            List<Link> links = currentTarget.links;

            for (int i = 0; i < links.Count; i++)
            {
                if (i != 0)
                {
                    if (handles.TryGetValue(i - 1 + "b".ToString(), out Vector2 value1) == false)
                    {
                        handles.Add(i - 1 + "b".ToString(), links[i].location_02);
                    }
                    continue;
                }

                if (handles.TryGetValue(i + "a".ToString(), out Vector2 value2) == false)
                {
                    handles.Add(i + "a".ToString(), links[i].location_01);
                }
                if (handles.TryGetValue(i + "b".ToString(), out Vector2 value3) == false)
                {
                    handles.Add(i + "b".ToString(), links[i].location_02);
                }
            }

            for (int i = 0; i < links.Count; i++)
            {
                EditorGUI.BeginChangeCheck();

                if (i == 0)
                {
                    Handles.color = Color.blue;
                    if (!Application.isPlaying){links[i].location_01 = currentTarget.transform.position;}
                    handles[i + "a".ToString()] = Handles.FreeMoveHandle(links[i].location_01, Quaternion.identity, handleSize, new Vector3(0, 0, 0), Handles.SphereHandleCap);
                    Handles.color = Color.white;
                    handles[i + "b".ToString()] = Handles.FreeMoveHandle(links[i].location_02, Quaternion.identity, handleSize, new Vector3(0, 0, 0), Handles.SphereHandleCap);
                }
                else
                {
                    Handles.color = Color.white;
                    if (currentTarget.loopingTrack == Movable.Options.Enabled && i == links.Count - 1)
                    {
                        handles[0 + "a".ToString()] = Handles.FreeMoveHandle(links[i].location_02, Quaternion.identity, handleSize, new Vector3(0, 0, 0), Handles.SphereHandleCap);
                    }
                    handles[i + "b".ToString()] = Handles.FreeMoveHandle(links[i].location_02, Quaternion.identity, handleSize, new Vector3(0, 0, 0), Handles.SphereHandleCap);
                }

                if (Event.current.type == EventType.Repaint)
                {
                    Draw_Connections(links);
                    Draw_Labels(links);
                }


                // [0 1] [1 2] [2 3] [3 4] [4 5] 

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(currentTarget, "Locations Changed");
                    if (i != 0)
                    {
                        links[i].location_01 = handles[i - 1 + "b".ToString()];
                        links[i].location_02 = handles[i + "b".ToString()];
                        if (i != links.Count - 1) { links[i + 1].location_01 = handles[i + "b".ToString()]; }
                    }
                    else
                    {
                        links[i].location_01 = handles[i + "a".ToString()];
                        links[i].location_02 = handles[i + "b".ToString()];
                        if (i != links.Count - 1) { links[i + 1].location_01 = handles[i + "b".ToString()]; }
                    }
                }
            }
        }
        #endregion
    }

    public void Draw_Labels(List<Link> links)
    {
        foreach (Link n in links)
        {
            Handles.Label(n.Middle, "SPEED: " + n.speed, speed_label);
        }
    }

    public void Draw_Connections(List<Link> links)
    {
        Handles.color = Color.white;
        foreach (Link n in links)
        {
            Handles.DrawLine(n.location_01, n.location_02);
        }

        if (currentTarget.loopingTrack == Movable.Options.Enabled)
        {
            Handles.DrawLine(links[links.Count - 1].location_02, links[0].location_01);
        }
    }
}
