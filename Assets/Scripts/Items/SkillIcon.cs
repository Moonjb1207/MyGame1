using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class SkillIcon : UIProperty, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent myEvent;
    public Image myIcon;
    public int type;
    public ItemData myPotions;

    public SkillSlot myParent = null;
    public Rect Inven;

    public int seat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!eventData.dragging) OnAction();
    }

    Vector2 dragOffset = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)transform.position - eventData.position;
        transform.SetParent(transform.parent.parent.parent);
        myIcon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Inven.position.x + Inven.xMin > eventData.position.x ||
            Inven.position.x + Inven.yMax < eventData.position.x ||
            Inven.position.y + Inven.yMin > eventData.position.y ||
            Inven.position.y + Inven.yMax < eventData.position.y)
        {
            PlayerController.Inst.gameObject.GetComponent<Inventory>().throwPotion(seat, type);
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(myParent.myIconSlot);
            myIcon.raycastTarget = true;
        }
    }

    public void OnAction()
    {
        myEvent?.Invoke();
    }

    public void SetParent(SkillSlot slot)
    {
        myParent = slot;
        transform.SetParent(myParent.myIconSlot);
        myRT.anchoredPosition = Vector2.zero;
    }

    public void UsePotion()
    {
        PlayerController.Inst.players[PlayerController.Inst.CurIndex].getPotion(myPotions.potions[type].value);
    }

    public void countZero()
    {
        Destroy(gameObject);
    }
}
