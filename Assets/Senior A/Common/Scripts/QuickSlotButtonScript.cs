using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotButtonScript : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler
{

    public enum _ButtonType { Quickslot, Viewport }
    public _ButtonType ButtonType;

    public Image ButtonImage;
    public Text ButtonText;
    public int fid;

    public void OnSelect(BaseEventData eventData)
    {
        if (ButtonType == _ButtonType.Viewport)
        {

            this.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
            Debug.Log(this.gameObject.name + " was selected");
            QuickSlotManager.CurrentlySelected = this;
        }

    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (ButtonType == _ButtonType.Viewport)
        {

            this.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            Debug.Log(this.gameObject.name + " was deselected");


        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ButtonType == _ButtonType.Quickslot && QuickSlotManager.CurrentlySelected)
        {
            QuickSlotManager.AddToQuickSlot(this);
        }
    }


}
