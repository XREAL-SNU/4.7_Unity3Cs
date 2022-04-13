using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotButton : MonoBehaviour, ISelectHandler
{
    public enum _ButtonType {Quickslot, Viewport}

    public _ButtonType ButtonType;
    public Image ButtonImage; 
    public Text ButtonText;
    
    public void OnSelect(BaseEventData eventData) 
    {
        
    }
}
