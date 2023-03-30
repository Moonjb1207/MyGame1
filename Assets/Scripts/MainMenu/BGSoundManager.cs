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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBG()
    {
        myBG.volume = SettingManager.Inst.BGSound;
        myBG.clip = MainBG;

        myBG.Play();
    }
}
