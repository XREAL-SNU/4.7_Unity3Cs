using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingTab : MonoBehaviour
{
    private Button _button;
    private string _partName;
    private CustomizingButton[] _customizingButtons;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Select);

        _partName = gameObject.name;

        _customizingButtons = FindObjectsOfType(typeof(CustomizingButton)) as CustomizingButton[];
    }

    private void Select()
    {
        foreach (CustomizingButton customizingButton in _customizingButtons)
        {
            customizingButton.SetInfo(_partName);
        }
    }
}
