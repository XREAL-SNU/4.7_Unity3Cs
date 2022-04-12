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
        // 퀵슬롯 등록
        quickslotButton.SpecificExpression.sprite = CurrentlySelected.SpecificExpression.sprite;
        quickslotButton.ButtonText.text = CurrentlySelected.ButtonText.text;

        // 버퍼 비우기
        CurrentlySelected = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버퍼 비우기
        if (CurrentlySelected != null)
        {
            CurrentlySelected.Unselect();
            CurrentlySelected = null;
        }

    }

}
