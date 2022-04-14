using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class QuickSlotManager : MonoBehaviour
{
   [SerializeField] List<Sprite> _buttonIcons;
   [SerializeField] List<QuickSlotButton> _quickSlots;


   public Transform GridLayout; // 버튼 한꺼번에 참조 -> Transform.GetChild()  
   public GameObject _viewportButtonPrefab;
   
   public static QuickSlotButton CurrentlySelected; 
   public static List<QuickSlotButton> s_quickSlots;

    void Start()
    {
        GameObject buttonInstance; // Instantiate 의 반환갑 저장 변수
        QuickSlotButton buttonProp; // QuickSlotButton 컴포넌트 저장 번수
        

        for(int i = 0; i < _buttonIcons.Count; i++)
        {
            // Instantiate + GetComponent
            buttonInstance = (GameObject)Instantiate(_viewportButtonPrefab, GridLayout); //
            buttonProp = buttonInstance.GetComponent<QuickSlotButton>(); // refer to QuickSlotManager

            // Set QuickSortButton Properties
            buttonProp = GridLayout.GetChild(i).GetComponent<QuickSlotButton>();
            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
            buttonProp.fid = i;
        }

        s_quickSlots = new List<QuickSlotButton>();
        for (int i = 0; i < _quickSlots.Count; i++)
        {
            s_quickSlots.Add(_quickSlots[i]);
        }


    }

    public static void AddToQuickSlot(QuickSlotButton quickslotButton)
    {
       int index = CheckDistinct();
       
       if(index == -1)
       {
           //Debug.Log(CurrentlySelected.fid);
           // register quickslot
            quickslotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
            quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;
            quickslotButton.fid = CurrentlySelected.fid;
       }        
       else
       {
           SwapButtons(quickslotButton, index);
       }

        // vacate buffer
        CurrentlySelected = null;
    }

    static int CheckDistinct() 
    {
        int retVal = -1; // not regsitered && not overlapped => -1

        for (int i = 0; i < s_quickSlots.Count; i++)
        {
            if(s_quickSlots[i].fid == CurrentlySelected.fid)
                retVal = i;
        }
        //Debug.Log("list length: " + s_quickSlots.Count);
        return retVal;
    }

    static void SwapButtons(QuickSlotButton quickslotButton, int index)
    {
        Sprite spriteBuf = quickslotButton.ButtonImage.sprite;
        string textBuf = quickslotButton.ButtonText.text; 
        int idBuf = quickslotButton.fid;

        quickslotButton.ButtonImage.sprite = s_quickSlots[index].ButtonImage.sprite;
        quickslotButton.ButtonText.text = s_quickSlots[index].ButtonText.text;
        quickslotButton.fid = s_quickSlots[index].fid;

        s_quickSlots[index].ButtonImage.sprite = spriteBuf;
        s_quickSlots[index].ButtonText.text = textBuf;
        s_quickSlots[index].fid = idBuf;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // vacate buffer
        CurrentlySelected = null;
    }


}
