using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingManager : MonoBehaviour
{
    public Slider[] Sound;
    public TMPro.TMP_InputField[] Soundnum;
    public static SoundSettingManager Inst = null;

    private void Awake()
    {
        Inst = this;

        Sound[0].value = SettingManager.Inst.MSSound;
        Sound[1].value = SettingManager.Inst.BGSound;
        Sound[2].value = SettingManager.Inst.EFSound;

        Soundnum[0].text = (SettingManager.Inst.MSSound * 100).ToString();
        Soundnum[1].text = (SettingManager.Inst.BGSound * 100).ToString();
        Soundnum[2].text = (SettingManager.Inst.EFSound * 100).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveButton()
    {
        SettingManager.Inst.MSSound = Sound[0].value;
        SettingManager.Inst.BGSound = Sound[1].value;
        SettingManager.Inst.EFSound = Sound[2].value;

        BGSoundManager.Inst.myBG.volume = SettingManager.Inst.BGSound;

        SettingManager.Inst.SettingSave(Application.dataPath + @"SettingData.data");
    }

    public void ChangeSound(int num)
    {
        if (num > 0 && Sound[num].value > Sound[0].value)
        {
            Sound[num].value = Sound[0].value;
        }

        Sound[num].value = float.Parse(Soundnum[num].text) / 100;
    }

    public void ChangeSound_Slider(int num)
    {
        if (num > 0 && Sound[num].value > Sound[0].value)
        {
            Sound[num].value = Sound[0].value;
        }

        Soundnum[num].text = ((int)(Sound[num].value * 100)).ToString();
    }
}
