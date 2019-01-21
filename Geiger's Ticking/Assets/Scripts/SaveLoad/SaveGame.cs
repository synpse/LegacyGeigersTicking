﻿using UnityEngine;

public class SaveGame
{

    //serialized
    public string SaveID = "Save";
    public System.DateTime time = System.DateTime.Now;

    public Vector3 PlayerPos = Vector3.zero;

    private static string _gameDataFileName = "data.json";

    private static SaveGame _instance;
    public static SaveGame Instance
    {
        get
        {
            if (_instance == null)
                Load();
            return _instance;
        }

    }

    public static void Save()
    {
        FileManager.Save(_gameDataFileName, _instance);
    }

    public static void Load()
    {
        _instance = FileManager.Load<SaveGame>(_gameDataFileName);
    }
}