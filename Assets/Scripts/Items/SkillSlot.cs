using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IDropHandler
{
    public Transform myIconSlot;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        SkillIcon child = GetComponentInChildren<SkillIcon>();
        SkillIcon icon = eventData.pointerDrag.GetComponent<SkillIcon>();

        if (child != null)
        {
            child.SetParent(icon.myParent);
        }

        PlayerController.Inst.gameObject.GetComponent<Inventory>().changeSlot(num, icon.seat);

        icon?.SetParent(this);
    }
}
