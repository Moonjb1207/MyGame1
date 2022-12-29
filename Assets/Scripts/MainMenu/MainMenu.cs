using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    enum MenuState
    {
        Main, Play, Help, Quit,
    }

    MenuState mycur = MenuState.Main;

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
}
