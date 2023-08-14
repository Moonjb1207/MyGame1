using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class HelpVideo : MonoBehaviour
{
    public GameObject myVideo;
    public GameObject VideoImg;

    bool isPlaying;
    float playTime = 0.0f;

    VideoPlayer myPlayer;

    public GameObject[] buttons;
    public VideoClip[] clips;

    public GameObject[] contents;

    int curbutton = -1;
    Sprite playImg;

    public Sprite pauseImg;

    enum menuState
    { 
        move, attack, play
    }

    menuState mycur = menuState.move;

    void ChangeMenu(menuState n)
    {
        if (mycur == n) return;
        mycur = n;

        switch (mycur)
        {
            case menuState.move:
                foreach (GameObject i in contents)
                {
                    i.SetActive(false);
                }
                contents[(int)menuState.move].SetActive(true);
                break;
            case menuState.attack:
                foreach (GameObject i in contents)
                {
                    i.SetActive(false);
                }
                contents[(int)menuState.attack].SetActive(true);
                break;
            case menuState.play:
                foreach (GameObject i in contents)
                {
                    i.SetActive(false);
                }
                contents[(int)menuState.play].SetActive(true);
                break;
        }
    }

    public void HelpMoveButton()
    {
        ChangeMenu(menuState.move);
    }

    public void HelpAttackButton()
    {
        ChangeMenu(menuState.attack);
    }

    public void HelpPlayButton()
    {
        ChangeMenu(menuState.play);
    }

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = myVideo.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            playTime += Time.deltaTime;
        }

        if (myPlayer.clip != null)
        {
            if (isPlaying && playTime > myPlayer.length)
            {
                isPlaying = false;
                playTime = 0.0f;
                endVideo();
            }
        }
        else
        {
            return;
        }
    }

    public void playVideo(int n)
    {
        if (isPlaying && n == curbutton)
        {
            endVideo();

            return;
        }

        VideoImg.SetActive(true);

        VideoImg.transform.SetParent(buttons[n].transform);
        VideoImg.transform.localPosition =
            new Vector3(-VideoImg.GetComponent<RectTransform>().rect.width / 2 
                        - buttons[n].GetComponent<RectTransform>().rect.width, 0, 0);
        myPlayer.clip = clips[n];

        if (myPlayer.clip == null)
        {
            return;
        }

        myPlayer.Play();

        playImg = buttons[n].GetComponent<Image>().sprite;
        buttons[n].GetComponent<Image>().sprite = pauseImg;

        curbutton = n;

        isPlaying = true;
    }

    void endVideo()
    {
        VideoImg.SetActive(false);
        buttons[curbutton].GetComponent<Image>().sprite = playImg;

        curbutton = -1;

        myPlayer.clip = null;
    }
}
