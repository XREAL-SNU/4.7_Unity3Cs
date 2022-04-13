using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class QuickSlotManager : MonoBehaviour
{
    [SerializedField] List<Sprite> _buttonIcons;
    [SerializedField] List<QuickSlotButton> _quickSlots;

    public Transform GridLayout; 

    public static QuickSlotButton CurrentlySelected;
    public static List<QuickSlotButton> s_quickSlots;

    void Start()
    {
        GameObject buttonInstance; //?
        QuickSlotButton buttonProp;

        for(int i = 0; i < _buttonIcons.Count; i++)
        {
            buttonInstance = Instantiate(_viewportButtonPrefab, GridLayout); //?
            buttonProp = buttonInstance.GetComponent<QuickSlotButton_Sol>(); //?

            buttonProp = GridLayout.GetChild(i).GetComponent<QuickSlotButton>();
            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
        }

        s_quickSlots = new List<QuickSlotButton>();
        for (int i = 0; i < _quickSlots.Count; i++)
        {
            s_quickSlots.Add(_quickSlots[i]);
        }

    }

    static int CheckDistinct() 
    {
        int retVal = -1;

        for (int i = 0; i < s_quickSlots.Count; i++)
        {
            if(s_quickSlots[i].fid == CurrentlySelected.fid)
                retVale = i;
        }
        
        return retVal;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (Buttontype == _ButtonType.Viewport)
        {
            QuickSlotManager.CurrentlySelected = this;
        }
    }


    public static void AddToQuickSlot(QuickSlotButton quickSlotButton)
    {
        int index = CheckDistinct;
        
        if (index == -1)
        {
            // register quickslot
            quickslotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
            quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;
            quickslotButton.fid = CurrentlySelected.fid;

        }
        else
        {

        }
       
        // vacate buffer
        CurrentlySelected = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // vacate buffer
        CurrentlySelected = null;
    }

    static void SwapButtons(QuickSlotButton quickslotButton, int index)
    {
        Sprite spriteBuf = quicksotButton.ButtonImage.sprite;
        string textBuf = quickslotButton.ButtonText.text; 
        int idBuf = quickslotButton.fid;

        quickslotButton.ButtonImage.sprite = s_quickSlots[index].ButtonImage.sprite;
        quickslotButton.ButtonImage.text = s_quickSlots[index].ButtonText.text;
        quickslotButton.fid = s_quickSlots[indx].fid;

        s_quickSlots[index].ButtonImage.sprite = spriteBuf;
        s_quickSlots[index].ButtonText.text = textBuf;
        s_quickSlots[index].fid = idBuf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
