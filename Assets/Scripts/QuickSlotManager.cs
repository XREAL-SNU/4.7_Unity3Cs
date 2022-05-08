using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlotManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<Sprite> _buttonIcons;
    [SerializeField] List<QuickSlotButton> _quickSlots;
    public Transform GridLayout;
    public GameObject _viewportButtonPrefab;
    public static QuickSlotButton CurrentlySelected;
    public static List<QuickSlotButton> s_quickSlots;

    [SerializeField] List<Texture> _faceTextures;
    public static List<Texture> s_faceTextures;

    void Start()
    {
        GameObject buttonInstance;  // Instantiate의 반환값을 담을 변수
        QuickSlotButton buttonProp; // QuickSlotButton 컴포넌트를 담을 변수

        for (int i = 0; i < _buttonIcons.Count; i++)
        {
            // Instantiate + GetComponenet
            buttonInstance = Instantiate(_viewportButtonPrefab, GridLayout);
            buttonProp = buttonInstance.GetComponent<QuickSlotButton>();

            // Set QuickSlotButton Properties
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

        // _faceTextures의 값 모두 추가
        s_faceTextures = new List<Texture>();
        for (int i = 0; i < _faceTextures.Count; i++)
        {
            s_faceTextures.Add(_faceTextures[i]);
        }

        gameObject.SetActive(false);
    }
    public static void AddToQuickSlot(QuickSlotButton quickslotButton)
    {
        int index = CheckDistinct();

        if (index == -1)
        {
            quickslotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
            quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;
            quickslotButton.fid = CurrentlySelected.fid;
        }
        else
        {
            SwapButtons(quickslotButton, index);
        }

        CurrentlySelected = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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
}
