using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<Sprite> _buttonIcons;

    public Transform GridLayout;

    public static QuickSlotButton CurrentlySelected = null;

    public GameObject buttonInstance;  // Instantiate의 반환값을 담을 변수

    [SerializeField] Transform[] quickSlots;

    [SerializeField] List<QuickSlotButton> _quickSlotsButton;
    public static List<QuickSlotButton> s_quickSlots;
    void Start()
    {
        for(int i = 0; i < quickSlots.Length; i++)
        {
            quickSlots[i].GetComponent<QuickSlotButton>().fid = i;
            quickSlots[i].GetChild(0).GetComponent<Image>().sprite = _buttonIcons[i];
            quickSlots[i].GetChild(1).GetComponent<Text>().text = _buttonIcons[i].name;
        }

        QuickSlotButton buttonProp; // QuickSlotButton 컴포넌트를 담을 변수

        for (int i = 0; i < _buttonIcons.Count; i++)
        {
            // Instantiate + GetComponenet
            buttonInstance = Instantiate(buttonInstance, GridLayout);
            buttonProp = buttonInstance.GetComponent<QuickSlotButton>();

            buttonProp = GridLayout.GetChild(i).GetComponent<QuickSlotButton>();
            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
            buttonProp.fid = i;
        }
        s_quickSlots = new List<QuickSlotButton>();
        for (int i = 0; i < _quickSlotsButton.Count; i++)
        {
            s_quickSlots.Add(_quickSlotsButton[i]);
        }
    }

    public static void AddToQuickSlot(QuickSlotButton quickslotButton)
    {
        int retval = CheckDistinct();

        if (retval == -1)
        {
            quickslotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
            quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;
            quickslotButton.fid = CurrentlySelected.fid;
        }
        else
        {
            SwapPosition(quickslotButton, retval);
        }
        // 버퍼 비우기
        CurrentlySelected = null;
    }
    static void SwapPosition(QuickSlotButton quickslotButton, int index)
    {
        QuickSlotButton qb = quickslotButton.GetComponent<QuickSlotButton>(); //바뀌는 애
        Sprite bufferImage = qb.ButtonImage.sprite;
        string bufferText = qb.ButtonText.text;
        int idBuf = qb.fid;

        quickslotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
        quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;
        quickslotButton.fid = CurrentlySelected.fid;

        s_quickSlots[index].ButtonImage.sprite = bufferImage;
        s_quickSlots[index].ButtonText.text = bufferText;
        s_quickSlots[index].fid = idBuf;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버퍼 비우기
        CurrentlySelected = null;
    }

    static int CheckDistinct()
    {
        int retVal = -1;

        for (int i = 0; i < s_quickSlots.Count; i++)
        {
            if (s_quickSlots[i].fid == CurrentlySelected.fid)
                retVal = i;
        }


        return retVal;
    }
}