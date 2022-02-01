using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static string CurrentSave;

    public static string[] GetSaveNames()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        DirectoryInfo[] info = dir.GetDirectories();
        string[] fileNames = new string[info.Length];

        for(int i = 0; i < info.Length; i++)
        {
            fileNames[i] = info[i].Name;
        }
        return fileNames;
    }
    public static void CreateNewSave(string name)
    {
        string p_Main = Path.Combine(Application.persistentDataPath, name);
        var directory = Directory.CreateDirectory(p_Main);
        CurrentSave = directory.FullName;

        string p_Levels = Path.Combine(CurrentSave, "Levels");
        Directory.CreateDirectory(p_Levels);

        

        string p_Player = Path.Combine(CurrentSave, "Player");
        Directory.CreateDirectory(p_Player);
    }
    public static void DeleteSave(string name)
    {
        string path = Path.Combine(Application.persistentDataPath, name);
        string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

        for(int i = dirs.Length-1; i >= 0; i--)
        {
            string[] files = Directory.GetFiles(dirs[i]);
            foreach (string file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(dirs[i]);
        }
        Directory.Delete(path);
    }

    #region Level_Data
    public static void Save_Level_Data(Level_Save_Package data)
    {       
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + data.level + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static Level_Save_Package Load_Level_Data(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".data";
        if (File.Exists(path))
        {
            Debug.Log("Save exists at: " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Level_Save_Package data = formatter.Deserialize(stream) as Level_Save_Package;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
    #endregion
}
