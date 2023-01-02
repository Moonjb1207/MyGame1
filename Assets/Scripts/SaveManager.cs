using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Inst = null;
    public bool IsExist;

    public string StageSavefp = "";

    private void Awake()
    {
        if (Inst == null)
            Inst = this;

        StageSavefp = Application.dataPath + @"\Stage.data";
    }

    public void SaveFile<T>(string fpath, T data)
    {
        if (!File.Exists(fpath))
        {
            File.Create(fpath).Close();
        }

        string ToJsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(fpath, ToJsonData);
    }

    public T LoadFile<T>(string fpath)
    {
        T data = default;

        if (File.Exists(fpath))
        {
            IsExist = true;
            string FromJsonData = File.ReadAllText(fpath);
            data = JsonUtility.FromJson<T>(FromJsonData);
        }
        else
        {
            Debug.Log(fpath + "파일이 존재하지 않습니다.");
            IsExist = false;
        }

        return data;
    }
}
