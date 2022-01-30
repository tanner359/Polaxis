using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
public class Level_Data : ScriptableObject
{
    public bool isComplete;

    [Header("Magnet Settings")]
    public GameObject Positive_Prefab;
    public GameObject Negative_Prefab;
    public int Magnet_Limit;
    public int Magnet_Count;

    [Header("Time Settings")]
    public int max_time;
    private float f_time;
    public float Completion_Time{get { return f_time; }set { f_time = value; }}

    [Header("Scoring")]
    public int Max_Score = 10000;
    public int Node_Penalty = 100;
    public int Time_Penalty = 200;

    private int f_score;
    public int Score{
        get { return (int)(Mathf.Pow(Completion_Time, 1.3f) * (-Time_Penalty) + Magnet_Count * (-Node_Penalty)) + Max_Score; }
    }
}
