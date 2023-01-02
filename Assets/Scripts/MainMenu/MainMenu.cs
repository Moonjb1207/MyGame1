using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    enum MenuState
    {
        Main, Play, Help, Setting, Quit,
    }

    MenuState mycur = MenuState.Main;
    [SerializeField] PlayMenu myPMenu = null;

    public List<GameObject> myMenu = new List<GameObject>();

    void ChangeMenu(MenuState n)
    {
        if (mycur == n) return;
        mycur = n;

        switch (mycur)
        {
            case MenuState.Main:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Main].SetActive(true);
                break;
            case MenuState.Play:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Play].SetActive(true);
                break;
            case MenuState.Help:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Help].SetActive(true);
                break;
            case MenuState.Setting:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Setting].SetActive(true);
                break;
            case MenuState.Quit:
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

    public void MainPlayButton()
    {
        ChangeMenu(MenuState.Play);
    }

    public void MainSettingButton()
    {
        ChangeMenu(MenuState.Setting);
    }

    public void MainHelpButton()
    {
        ChangeMenu(MenuState.Help);
    }

    public void MainQuitButton()
    {

    }

    public void PlayPrevButton()
    {
        myPMenu.PrevStage();
    }

    public void PlayNextButton()
    {
        myPMenu.NextStage();
    }

    public void PlayPlayButton()
    {
        myPMenu.selectStage();
        LoadManager.Inst.ChangeScene();
    }

    public void PlayBackButton()
    {
        ChangeMenu(MenuState.Main);
    }

    public void HelpBackButton()
    {
        ChangeMenu(MenuState.Main);
    }

    public void SettingSaveButton()
    {

    }

    public void SettingBackButton()
    {
        ChangeMenu(MenuState.Main);
    }

    public void SettingKeyButton()
    {

    }
}
