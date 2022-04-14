using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotButton : MonoBehaviour, ISelectHandler
{
    public enum _ButtonType { Quickslot, Viewport }

    public _ButtonType ButtonType;
    public Image ButtonImage;
    public Text ButtonText;
    public int fid;
    public void OnSelect(BaseEventData eventData)
    {
        if (ButtonType == _ButtonType.Quickslot && QuickSlotManager.CurrentlySelected != null)
        {
            QuickSlotManager.AddToQuickSlot(this);
        }
        if (ButtonType == _ButtonType.Viewport) //viewport하는 이유는 퀵슬롯클릭하고 뷰포트 버튼 누르면 퀵슬롯->뷰포트로 넘어가는 버그때문인가?
        {
            QuickSlotManager.CurrentlySelected = this;
        }  
    }
}