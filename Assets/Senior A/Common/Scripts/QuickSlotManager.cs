using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<Sprite> _buttonIcons;

    public Transform GridLayout;

    public static QuickSlotButton CurrentlySelected;

    void Start()
    {
        QuickSlotButton buttonProp;

        for (int i = 0; i < GridLayout.childCount; i++)
        {
            buttonProp = GridLayout.GetChild(i).GetComponent<QuickSlotButton>();
            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
        }
    }

    public static void AddToQuickSlot(QuickSlotButton quickslotButton)
    {
        // Äü½½·Ô µî·Ï
        quickslotButton.ButtonImage.sprite = CurrentlySelected.ButtonImage.sprite;
        quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;

        // ¹öÆÛ ºñ¿ì±â
        CurrentlySelected = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CurrentlySelected = null;
    }
}