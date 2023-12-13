using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static public PauseMenu Inst = null;

    private void Awake()
    {
        Inst = this;
    }

    enum MenuState
    {
        Main, Setting, Help,
    }

    MenuState myState = MenuState.Main;
    public GameObject[] myMenu;
    public bool IsPause = false;

    void ChangeState(MenuState s)
    {
        if (myState == s) return;

        myState = s;

        switch(myState)
        {
            case MenuState.Main:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Main].SetActive(true);
                CurrentImg.Inst.unActiveImg();
                break;
            case MenuState.Setting:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Setting].SetActive(true);
                CurrentImg.Inst.ActiveImg((int)MenuState.Setting);
                break;
            case MenuState.Help:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Help].SetActive(true);
                CurrentImg.Inst.ActiveImg((int)MenuState.Help);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainSettingButton()
    {
        ChangeState(MenuState.Setting);
    }

    public void MainHelpButton()
    {
        ChangeState(MenuState.Help);
    }

    public void MainMainSceneButton()
    {
        Time.timeScale = 1.0f;
        LoadManager.Inst.ChangeScene("MainMenu");
    }

    public void MainQuitButton()
    {
        Application.Quit();
    }

    public void MainBackButton()
    {
        if (PlayerController.Inst.GS_Pause == null)
        {
            return;
        }

        Time.timeScale = 1.0f;
        PlayerController.Inst.GS_Pause.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        PlayerController.Inst.GS_myCam.IsLive = true;
    }

    public void SettingBackButton()
    {
        ChangeState(MenuState.Main);
    }

    public void SettingSaveButton()
    {
        SoundSettingManager.Inst.SaveButton();
    }

    public void HelpBackButton()
    {
        ChangeState(MenuState.Main);
    }
}
