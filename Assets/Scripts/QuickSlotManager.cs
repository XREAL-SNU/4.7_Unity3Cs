using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlotManager : MonoBehaviour, IPointerClickHandler

{
    [SerializeField] List<Sprite> _buttonIcons;
    [SerializeField] List<QuickSlotButton> _quickSlots;
    [SerializeField] List<Texture> _faceTextures;

    public static List<Texture> s_faceTextures;

    public Transform GridLayout;

    public static QuickSlotButton CurrentlySelected;
    public static List<QuickSlotButton> s_quickSlots;

    // Start is called before the first frame update
    void Start()
    {
        QuickSlotButton currentButton;

        for(int i = 0; i < GridLayout.childCount; i++)
        {
            currentButton = GridLayout.GetChild(i).GetComponent<QuickSlotButton>();
            currentButton.fid = i;
        }

        // _quickSlots의 값 모두 추가
        s_quickSlots = new List<QuickSlotButton>();
        for (int i = 0; i < _quickSlots.Count; i++)
        {
            s_quickSlots.Add(_quickSlots[i]);
        }

        // _faceTextures의 값 모두 추가
        s_faceTextures = new List<Texture>();
        for (int i = 0; i < _faceTextures.Count; i++)
        {
            s_faceTextures.Add(_faceTextures[i]);
        }
    }

    static void SwapButtons(QuickSlotButton quickslotButton, int index)
    {
        QuickSlotButton temp = Instantiate(quickslotButton);

        quickslotButton.copiedByOtherButton(s_quickSlots[index]);
        s_quickSlots[index].copiedByOtherButton(temp);
    }

    public static void AddToQuickSlot(QuickSlotButton quickslotButton)
    {
        int index = CheckDistinct();
        // 퀵슬롯 등록
        Debug.Log(index);
        if (index == -1)
        {
            quickslotButton.copiedByOtherButton(CurrentlySelected);
        } else
        {
            SwapButtons(quickslotButton, CurrentlySelected.fid);
        }

        CurrentlySelected.Unselect();
        CurrentlySelected = null;

        // 버퍼 비우기

        
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

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버퍼 비우기
        Debug.Log(eventData);
        if (CurrentlySelected != null)
        {
            CurrentlySelected.Unselect();
            CurrentlySelected = null;
        }

    }

}
