using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGSoundManager : MonoBehaviour
{
    public static BGSoundManager Inst = null;
    public AudioSource myBG = null;

    public AudioClip MainBG;
    public AudioClip PlayBG;

    private void Awake()
    {
        Inst = this;
        myBG = GetComponentInChildren<AudioSource>();

        myBG.volume = SettingManager.Inst.BGSound;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
