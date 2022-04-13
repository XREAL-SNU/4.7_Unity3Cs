using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class QuickSlotManager : MonoBehaviour
{
   [SerializeField] List<Sprite> _buttonIcons;

   public Transform GridLayout; // 버튼 한꺼번에 참조 -> Transform.GetChild()  
   public GameObject _viewportButtonPrefab;



    void Start()
    {
        GameObject buttonInstance; // Instantiate 의 반환갑 저장 변수
        QuickSlotButton buttonProp; // QuickSlotButton 컴포넌트 저장 번수
        

        for(int i = 0; i < _buttonIcons.Count; i++)
        {
            // Instantiate + GetComponent
            buttonInstance = Instantiate(_viewportButtonPrefab, GridLayout); //
            buttonProp = buttonInstance.GetComponent<QuickSlotButton>(); //

            // Set QuickSortButton Properties
            buttonProp = GridLayout.GetChild(i).GetComponent<QuickSlotButton>();
            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;
        }


    }
}
