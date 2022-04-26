using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingButton : MonoBehaviour
{
    private Button _button;
    private string _partName;

    public Color _color;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Select);

        // 비어있을 경우 에러가 발생하기 때문에 Body로 초기화
        _partName = "Body";
    }

    private void Select()
    {
        CharacterAppearance _appearance = PlayerManager.Player.Apperance;
        GameObject go = _appearance.CustomParts[_partName];
        _appearance.ApplyColor(go, _color);
    }

    public void SetInfo(string partName)
    {
        _partName = partName;
    }
}