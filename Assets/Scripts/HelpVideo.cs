using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class HelpVideo : MonoBehaviour
{
    public GameObject myVideo;

    bool isPlaying;
    float playTime = 0.0f;

    VideoPlayer myPlayer;

    public GameObject[] buttons;
    public VideoClip[] clips;
    
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
    }

    public void playVideo(int n)
    {
        myVideo.SetActive(true);

        myVideo.transform.SetParent(buttons[n].transform);
        myVideo.transform.position = Vector3.zero;
        myPlayer.clip = clips[n];
        myPlayer.Play();

        isPlaying = true;
    }

    void endVideo()
    {
        myVideo.SetActive(false);
    }
}
