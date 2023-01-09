using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalDataManager
{
    public static string BasePath = "saveData.json";
    
    public static void Save<T>(T data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, BasePath);
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);
    }

    public static T Load<T>()
    {
        string filePath = Path.Combine(Application.persistentDataPath, BasePath);
        string jsonData = File.ReadAllText(filePath);
        var data = JsonUtility.FromJson<T>(jsonData);
        return data;
    }
}
