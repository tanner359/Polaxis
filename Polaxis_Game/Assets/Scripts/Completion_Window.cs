using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Completion_Window : MonoBehaviour
{
    [SerializeField] private Level_Data level_Data;

    public TMP_Text score;
    public TMP_Text magnet_count;
    public TMP_Text time_completed;

    public GameObject new_record;

    public Transform leaderboard_container;
    public GameObject entry_prefab;

    public TMP_InputField input_field;

    private void OnEnable()
    {     
        level_Data = Game_Manager.instance.Level_Data;
        Load();
        score.text = "SCORE: " + level_Data.Score;
        magnet_count.text = "Magnets x " + level_Data.Magnet_Count;
        time_completed.text = "Time: " + level_Data.Completion_Time;

        if (level_Data.best_scores.Length == 0) { return; }
        if (level_Data.best_scores[0] == null || level_Data.Score > level_Data.best_scores[0].best_score)
        {
            new_record.SetActive(true);
        }
        Update_Leaderboard();
        Save();
    }

    public void Save()
    {
        SaveSystem.Save_Level_Data(new Level_Save_Package(level_Data));
    }

    public void Load()
    {
        Level_Save_Package data = SaveSystem.Load_Level_Data(level_Data.name);
        if (data == null) { return; }
        level_Data.best_scores = data.leaderboards;
    }

    public void Next_Level()
    {
        Game_Manager.instance.Next_Level();
    }

    bool entry_added = false;
    public void Add_Entry()
    {
        if (entry_added) { return; }
        for (int i = 0; i < level_Data.best_scores.Length; i++)
        {
            if (level_Data.best_scores[i] == null || level_Data.Score >= level_Data.best_scores[i].best_score)
            {
                Shift_Entries(i);
                level_Data.best_scores[i] = new Record(input_field.text, level_Data.Score);
                Update_Leaderboard();
                Save();
                break;
            }
        }
        entry_added = true;
    }

    public void Shift_Entries(int from)
    {
        Record[] temp = new Record[10];
        for (int i = 0; i < level_Data.best_scores.Length; i++)
        {
            if(i < from) { temp[i] = level_Data.best_scores[i]; continue; }
            if( i+1 > temp.Length-1)
            {
                level_Data.best_scores[i] = null;
                break;
            }
            temp[i + 1] = level_Data.best_scores[i];
        }
        level_Data.best_scores = temp;
    }

    private void Update_Leaderboard()
    {
        foreach (Transform t in leaderboard_container)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < level_Data.best_scores.Length; i++)
        {
            if(level_Data.best_scores[i] == null) { continue; }
            GameObject entry = Instantiate(entry_prefab, leaderboard_container);
            Entry e = entry.GetComponent<Entry>();
            e.rank.text = '#' + (i + 1).ToString();
            e.name.text = level_Data.best_scores[i].scorer_name;
            e.score.text = level_Data.best_scores[i].best_score.ToString();

            switch (i)
            {
                case 0:
                    e.rank.color = new Color(1.0f, 0.82f, 0.0f);
                    e.rank.fontSize *= 1.30f;
                    break;

                case 1:
                    e.rank.color = new Color(0.8f, 0.83f, 0.88f);
                    e.rank.fontSize *= 1.20f;
                    break;

                case 2:
                    e.rank.color = new Color(0.97f, 0.56f, 0.13f);
                    e.rank.fontSize *= 1.10f;
                    break;

            }
        }
    }
}
