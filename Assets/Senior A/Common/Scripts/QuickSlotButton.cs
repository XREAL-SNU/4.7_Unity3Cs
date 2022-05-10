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
        if (ButtonType == _ButtonType.Viewport) //viewport�ϴ� ������ ������Ŭ���ϰ� ����Ʈ ��ư ������ ������->����Ʈ�� �Ѿ�� ���׶����ΰ�?
        {
            QuickSlotManager.CurrentlySelected = this;
        }  
    }
}