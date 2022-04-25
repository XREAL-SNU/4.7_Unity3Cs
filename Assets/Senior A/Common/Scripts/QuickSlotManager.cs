using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlotManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<Sprite> _buttonIcons;
    [SerializeField] List<QuickSlotButtonScript> _quickSlots;
    [SerializeField] List<Texture> _faceTextures;

    public Transform GridLayout;
    public GameObject _viewportButtonPrefab;

    public static QuickSlotButtonScript CurrentlySelected;
    public static List<QuickSlotButtonScript> s_quickSlots;
    public static List<Texture> s_faceTextures;

    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonInstance;
        QuickSlotButtonScript buttonProp;

        for (int i = 0; i < _buttonIcons.Count; i++)
        {
            buttonInstance = Instantiate(_viewportButtonPrefab, GridLayout);

            buttonProp = buttonInstance.GetComponent<QuickSlotButtonScript>();

            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
            buttonProp.fid = i;

        }

        s_quickSlots = new List<QuickSlotButtonScript>();
        for (int i = 0; i < _quickSlots.Count; i++)
        {
            s_quickSlots.Add(_quickSlots[i]);
            s_quickSlots[i].fid = i;
        }

        // _faceTextures의 값 모두 추가
        s_faceTextures = new List<Texture>();
        for (int i = 0; i < _faceTextures.Count; i++)
        {
            s_faceTextures.Add(_faceTextures[i]);
        }
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
    static void SwapButtons(QuickSlotButtonScript quickslotButton, int index)
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

    public static void AddToQuickSlot(QuickSlotButtonScript quickslotButton)
    {
        int index = CheckDistinct();
        Debug.Log("index" + index);
        // 퀵슬롯 등록
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
        // 버퍼 비우기
        CurrentlySelected = null;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버퍼 비우기
        CurrentlySelected = null;
    }

}
