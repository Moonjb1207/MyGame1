using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public enum KeyAction
{
    Change, Pause, Get, Mouse, Run, Jump, KeyCount,
}

public class SettingManager : MonoBehaviour
{
    public SettingData mySettingDatas = new SettingData();
    public Dictionary<KeyAction, KeyCode> KeyPairs = new Dictionary<KeyAction, KeyCode>();
    public float MSSound;
    public float BGSound;
    public float EFSound;
    public static SettingManager Inst = null;

    KeyCode[] defaultKeys = new KeyCode[]
    {
        KeyCode.X, KeyCode.P, KeyCode.Z, KeyCode.LeftAlt, KeyCode.LeftShift, KeyCode.Space
    };

    float defaultSound = 1.0f;

    private void Awake()
    {
        if (Inst == null)
            Inst = this;

        string path = Application.dataPath + @"SettingData.data";

        if (!File.Exists(path))
        {
            for (int i = 0; i < (int)KeyAction.KeyCount; i++)
            {
                mySettingDatas.keys.Add(defaultKeys[i]);
            }

            for (int i = 0; i < (int)KeyAction.KeyCount; i++)
            {
                KeyPairs.Add((KeyAction)i, defaultKeys[i]);
            }

            mySettingDatas.msSound = defaultSound;
            mySettingDatas.bgSound = defaultSound;
            mySettingDatas.efSound = defaultSound;

            string data = JsonUtility.ToJson(mySettingDatas);

            File.Create(path).Close();
            File.WriteAllText(path, data);
        }
        else
        {
            SettingLoad(path);
        }
    }

    public void SettingLoad(string path)
    {
        string data = File.ReadAllText(path);

        mySettingDatas = JsonUtility.FromJson<SettingData>(data);

        for (int i = 0; i < (int)KeyAction.KeyCount; i++)
        {
            KeyPairs.Add((KeyAction)i, mySettingDatas.keys[i]);
        }

        MSSound = mySettingDatas.msSound;
        BGSound = mySettingDatas.bgSound;
        EFSound = mySettingDatas.efSound;
    }

    public void SettingSave(string path)
    {
        for (int i = 0; i < (int)KeyAction.KeyCount; i++)
        {
            mySettingDatas.keys.Add(KeyPairs[(KeyAction)i]);
        }

        mySettingDatas.msSound = MSSound;
        mySettingDatas.bgSound = BGSound;
        mySettingDatas.efSound = EFSound;

        string data = JsonUtility.ToJson(mySettingDatas);

        File.WriteAllText(path, data);
    }
}
