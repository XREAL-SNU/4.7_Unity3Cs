using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotButton : MonoBehaviour, ISelectHandler, IPointerClickHandler
{
    public enum _ButtonType { Quickslot, Viewport }
    public _ButtonType ButtonType;

    public Image ButtonImage;
    public Text ButtonText;
    public Image SpecificExpression;
    public int fid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void copiedByOtherButton(QuickSlotButton other)
    {
        this.SpecificExpression.sprite = other.SpecificExpression.sprite;
        this.ButtonText.text = other.ButtonText.text;
        this.fid = other.fid;
    }
    public void OnSelect(BaseEventData eventData)
    {
        
        if(ButtonType == _ButtonType.Viewport)
        {
            ButtonImage.color = Color.yellow;
            QuickSlotManager.CurrentlySelected = this;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ButtonType == _ButtonType.Quickslot && QuickSlotManager.CurrentlySelected != null)
        {
            QuickSlotManager.AddToQuickSlot(this);
        }
    }

    public void Unselect()
    {
        ButtonImage.color = Color.white;
    }

}
