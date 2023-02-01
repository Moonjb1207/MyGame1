using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public static EffectSoundManager Inst = null;

    float effectVolume;

    public float EffectVolume
    {
        get => effectVolume;
    }

    private void Awake()
    {
        Inst = this;
        
        effectVolume = SettingManager.Inst.EFSound;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume(float f)
    {
        effectVolume = f;
    }

    public void CreateEffectSound(Vector3 pos)
    {

    }
}
