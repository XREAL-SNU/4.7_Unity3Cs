using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlotManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<Sprite> _buttonIcons;
    [SerializeField] List<QuickSlotButton> _quickSlots;
    [SerializeField] List<Texture> _faceTextures;


    public Transform GridLayout;

    public GameObject _viewportButtonPrefab;
    public static QuickSlotButton CurrentlySelected;
    public static List<QuickSlotButton> s_quickSlots;
    public static List<Texture> s_faceTextures;
    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonInstance;
        QuickSlotButton buttonProp;

        for(int i=0; i<_buttonIcons.Count; i++)
        {
            buttonInstance = Instantiate(_viewportButtonPrefab, GridLayout);
            buttonProp = buttonInstance.GetComponent<QuickSlotButton>();

            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
            buttonProp.fid = i;
        }

        s_quickSlots = new List<QuickSlotButton>();
        for(int i=0;i<_quickSlots.Count;i++)
        {
            s_quickSlots.Add(_quickSlots[i]);
        }

        s_faceTextures = new List<Texture>();
        for(int i=0;i<_faceTextures.Count;i++)
        {
            s_faceTextures.Add(_faceTextures[i]);
        }
    }

    public static void AddToQuickSlot(QuickSlotButton quickSlotButton)
    {
        int index = CheckDistinct();
        if(index == -1)
        {
            quickSlotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
            quickSlotButton.ButtonText.text = CurrentlySelected.ButtonText.text;
            quickSlotButton.fid = CurrentlySelected.fid;
        }
        else
        {
            SwapButtons(quickSlotButton, index);
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
        for(int i=0;i<s_quickSlots.Count;i++)
        {
            if(s_quickSlots[i].fid == CurrentlySelected.fid)
            {
                retVal = i;
            }
        }
        return retVal;
    }

    static void SwapButtons(QuickSlotButton quickSlotButton, int index)
    {
        Sprite spriteBuf = quickSlotButton.ButtonImage.sprite;
        string textBuf = quickSlotButton.ButtonText.text;
        int idBuf = quickSlotButton.fid;

        quickSlotButton.ButtonImage.sprite = s_quickSlots[index].ButtonImage.sprite;
        quickSlotButton.ButtonText.text = s_quickSlots[index].ButtonText.text;
        quickSlotButton.fid = s_quickSlots[index].fid;

        s_quickSlots[index].ButtonImage.sprite = spriteBuf;
        s_quickSlots[index].ButtonText.text = textBuf;
        s_quickSlots[index].fid = idBuf;
    }
}
