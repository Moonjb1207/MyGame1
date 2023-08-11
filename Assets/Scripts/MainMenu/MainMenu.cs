using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectOfType<LoadManager>() == null)
        {
            Instantiate(Resources.Load("Prefabs/LoadManager") as GameObject);
        }

        BGSoundManager.Inst.playBG();
    }

    public TMPro.TMP_Text curText;

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
                CurrentImg.Inst.unActiveImg();
                break;
            case MenuState.Play:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Play].SetActive(true);
                CurrentImg.Inst.ActiveImg((int)MenuState.Play);
                break;
            case MenuState.Help:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Help].SetActive(true);
                CurrentImg.Inst.ActiveImg((int)MenuState.Help);
                break;
            case MenuState.Setting:
                foreach (GameObject i in myMenu)
                {
                    i.SetActive(false);
                }
                myMenu[(int)MenuState.Setting].SetActive(true);
                CurrentImg.Inst.ActiveImg((int)MenuState.Setting);
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
        Application.Quit();
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
        LoadManager.Inst.ChangeScene("GamePlay");
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
        SoundSettingManager.Inst.SaveButton();
    }

    public void SettingBackButton()
    {
        ChangeMenu(MenuState.Main);
    }
}
