using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
public class QuickSlotButton : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler
{
    public enum _ButtonType {Quickslot, Viewport}

    public _ButtonType ButtonType;
    public Image ButtonImage; 
    public Text ButtonText;
    public int fid;


    public void OnSelect(BaseEventData eventData) 
    {
        if (ButtonType == _ButtonType.Viewport)
        {
            QuickSlotManager.CurrentlySelected = this;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(ButtonType == _ButtonType.QuickSlot && QuickSlotManager.CurrentlySelected != null)
        {

        }
    }

    void Start()
    {
        QuickSlotButton buttonProp;
        
        for (int i = 0; i < GridLayout.childCount; i++)
        {
            buttonPop = GridLayout.GetChild(i).GetComponent<QuickSlotButton>();
            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
            buttonProp.fid = i;
        }
    }

    public static void AddToQuickSlot(QuickSlotButton quickslotButton)
    {
        quickslotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
        quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;
        quickslotButton.fid = CurrentlySelected.fid;

        CurrentlySelected = null;
    }
    // not registered: true, registerd: false
    static int CheckDistinct()
    {
        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/