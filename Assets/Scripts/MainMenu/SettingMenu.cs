using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public GameObject Vol;
    public GameObject Key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingKeyMenu()
    {
        Vol.SetActive(false);
        Key.SetActive(true);
    }

    public void SettingVolMenu()
    {
        Vol.SetActive(true);
        Key.SetActive(false);
    }
}
