using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotManager : MonoBehaviour
{
    [SerializeField] List<Sprite> _buttonIcons;

    public Transform GridLayout;

    public static QuickSlotButton CurrentlySelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void AddToQuickSlot(QuickSlotButton quickslotButton)
    {
        // Äü½½·Ô µî·Ï
        quickslotButton.SpecificExpression.sprite = CurrentlySelected.SpecificExpression.sprite;
        quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;

        // ¹öÆÛ ºñ¿ì±â
        CurrentlySelected = null;
    }
}
