using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager Inst = null;

    public GameObject[] myLight = new GameObject[12];
    public GameObject mySun;
    public Material skyboxDay;
    public Material skyboxNight;

    bool IsDay = true;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skyboxDay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightOnOff()
    {
        if (IsDay)
        {
            IsDay = !IsDay;

            //RenderSettings.skybox = skyboxNight;
            mySun.SetActive(false);
            for (int i = 0; i < 12; i++)
            {
                myLight[i].SetActive(true);
            }
        }
        else
        {
            IsDay = !IsDay;

            //RenderSettings.skybox = skyboxDay;
            mySun.SetActive(true);
            for (int i = 0; i < 12; i++)
            {
                myLight[i].SetActive(false);
            }
        }
    }
}
