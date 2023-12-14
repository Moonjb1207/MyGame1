using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGSoundManager : MonoBehaviour
{
    public static BGSoundManager Inst = null;

    [SerializeField] AudioSource myBG = null;

    [SerializeField] AudioClip MainBG;
    [SerializeField] AudioClip PlayBG;

    #region get_set

    public AudioClip GS_MainBG
    {
        get => MainBG;
    }

    public AudioClip GS_PlayBG
    {
        get => PlayBG;
    }

    public AudioSource GS_myBG
    {
        get => myBG;
    }

    #endregion

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
