using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlotManager : MonoBehaviour, IPointerClickHandler

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
        // ������ ���
        quickslotButton.SpecificExpression.sprite = CurrentlySelected.SpecificExpression.sprite;
        quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;

        // ���� ����
        CurrentlySelected = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ����
        if (CurrentlySelected != null)
        {
            CurrentlySelected.Unselect();
            CurrentlySelected = null;
        }

    }

}