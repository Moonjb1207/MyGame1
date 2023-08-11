using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CurrentImg : MonoBehaviour
{
    public static CurrentImg Inst = null;

    public GameObject[] MenuFirstButton;

    // Start is called before the first frame update
    void Start()
    {
        if (Inst == null)
        {
            Inst = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveCurrent(GameObject parent)
    {
        transform.SetParent(parent.transform);

        transform.localPosition = new Vector3(-parent.GetComponent<RectTransform>().rect.width / 2 - GetComponent<RectTransform>().rect.width / 2, 0, 0);
    }

    public void clickButton()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;

        if (button != null)
        {
            MoveCurrent(button);
        }

        button = null;
    }

    public void ActiveImg(int n)
    {
        gameObject.SetActive(true);
        MoveCurrent(MenuFirstButton[n]);
    }

    public void unActiveImg()
    {
        gameObject.SetActive(false);
    }
}
