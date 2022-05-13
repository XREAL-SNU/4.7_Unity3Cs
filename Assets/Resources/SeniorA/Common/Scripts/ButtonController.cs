using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour, IPointerClickHandler
{
    public enum _buttonType { QuickSlot, ViewPort };
    public _buttonType buttonType;
    public Image buttonImage;
    public Text buttonText;
    private bool _isImaged;
    public Sprite tempImage;
    public string tempText;
    private void Init()
    {
        buttonImage = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        buttonText = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
    }
    private void Start()
    {
        Init();
    }
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonType == _buttonType.ViewPort)
        {
            ChangeFace.Instance().selectedBtn = this.gameObject;
        }
        else if (buttonType == _buttonType.QuickSlot)
        {
            Debug.Log(_isImaged);
            if (ChangeFace.Instance().selectedBtn != null)
            {
                int existedNum=ChangeFace.Instance().CheckIsSameFace();
                Debug.Log(existedNum);
                if (existedNum<5)
                {
                    
                    tempImage = buttonImage.sprite;
                    tempText = buttonText.text;
                    buttonImage.sprite = ChangeFace.Instance().selectedBtn.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    buttonText.text = ChangeFace.Instance().selectedBtn.transform.GetChild(1).gameObject.GetComponent<Text>().text;

                    ChangeFace.Instance().quickEmotions[existedNum].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tempImage;
                    ChangeFace.Instance().quickEmotions[existedNum].gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = tempText;
                    
                }
                else //quick slot에 이미지가 없다면
                {
                    _isImaged = !_isImaged;

                    buttonImage.sprite = ChangeFace.Instance().selectedBtn.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    buttonText.text = ChangeFace.Instance().selectedBtn.transform.GetChild(1).gameObject.GetComponent<Text>().text;
                }
                
                ChangeFace.Instance().selectedBtn = null;
            }
        }
        else
        {
            ChangeFace.Instance().selectedBtn = null;
        }
    }

    
}
