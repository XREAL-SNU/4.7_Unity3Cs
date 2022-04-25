using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotManager : MonoBehaviour
{
    [SerializeField] List<Sprite> _buttonIcons;
    public Transform GridLayout;
    public GameObject _viewportButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonInstance;
        QuickSlotButtonScript buttonProp;

        for (int i = 0; i < _buttonIcons.Count; i++)
        {
            buttonInstance = Instantiate(_viewportButtonPrefab, GridLayout);
            Debug.Log("check ---------" + buttonInstance);
            buttonProp = buttonInstance.GetComponent<QuickSlotButtonScript>();

            buttonProp.ButtonImage.sprite = _buttonIcons[i];
            buttonProp.ButtonText.text = _buttonIcons[i].name;

        }
    }

}
