using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    enum MenuState
    {
        Main, Setting, Help,
    }

    MenuState myState = MenuState.Main;
    public GameObject[] myMenu;

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
                break;
            case MenuState.Setting:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Setting].SetActive(true);
                break;
            case MenuState.Help:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Help].SetActive(true);
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

    }

    public void MainQuitButton()
    {

    }

    public void SettingBackButton()
    {
        ChangeState(MenuState.Main);
    }

    public void SettingSaveButton()
    {

    }

    public void HelpBackButton()
    {
        ChangeState(MenuState.Main);
    }
}
