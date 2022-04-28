using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotButton : MonoBehaviour, ISelectHandler, IPointerClickHandler
{
    public enum _Buttontype { Quickslot, Viewport }
    
    public _Buttontype Buttontype;
    public Image ButtonImage;
    public Text ButtonText;
    public int fid;

    public void OnSelect(BaseEventData eventData)
    {
        if(Buttontype == _Buttontype.Viewport)
        {
            QuickSlotManager.CurrentlySelected = this;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Buttontype == _Buttontype.Quickslot && QuickSlotManager.CurrentlySelected != null)
        {
            QuickSlotManager.AddToQuickSlot(this);
        }
    }
}
