using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;


public class MyEmotionIconPanel : UIBase
{
    enum Images
    {
        EmotionIcon,
        EmotionImage
    }

    enum Texts
    {
        EmotionName
    }

    private int index;
    private string emotionName;

    private void Start()
    {
        Init();

    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        Text emotionText = GetText((int)Texts.EmotionName);
        emotionText.text = emotionName;
        Image emotionImage = GetImage((int)Images.EmotionImage);
        emotionImage.sprite = Resources.Load<Sprite>("Expressions_UI/" + emotionName);

        GameManager.Instance().AddMyEmotions(index, this.GetComponent<MyEmotionIconPanel>());
        GetImage((int)Images.EmotionIcon).gameObject.BindEvent(OnClick_changeEmotion);
    }

    public void OnClick_changeEmotion(PointerEventData data)
    {
        GameManager.Instance().changeEmotion(index, this.GetComponent<MyEmotionIconPanel>());
    }

    public void changeEmotion(string _emotionName)
    {
        emotionName = _emotionName;
        Text emotionText = GetText((int)Texts.EmotionName);
        emotionText.text = emotionName;
        Image emotionImage = GetImage((int)Images.EmotionImage);
        emotionImage.sprite = Resources.Load<Sprite>("Expressions_UI/" + emotionName);
    }

    public void SetInfo(int _index, string _emotionName)
    {
        index = _index;
        emotionName = _emotionName;
    }

}
