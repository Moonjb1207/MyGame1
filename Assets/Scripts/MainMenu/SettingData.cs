using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class SettingData
{
    public List<KeyCode> keys = new List<KeyCode>();
    public float msSound;
    public float bgSound;
    public float efSound;
}
