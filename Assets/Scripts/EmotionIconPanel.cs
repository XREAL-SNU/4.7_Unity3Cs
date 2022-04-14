using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;


public class EmotionIconPanel : UIBase
{
    enum Images {
        EmotionIcon,
        EmotionImage
    }

    enum Texts {
        EmotionName
    }

    private string emotionName;

    private void Start() {
        Init();
    }

    public override void Init() {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        Text emotionText = GetText((int)Texts.EmotionName);
        emotionText.text = emotionName;
        Image emotionImage = GetImage((int)Images.EmotionImage);
        emotionImage.sprite = Resources.Load<Sprite>("Expressions_UI/" + emotionName);
        GetImage((int)Images.EmotionIcon).gameObject.BindEvent(OnClick_Icon);

        GameManager.Instance().AddEmotions(emotionName, this.GetComponent<EmotionIconPanel>());
    }

    public void OnClick_Icon(PointerEventData data) {
        GameManager.Instance().selectEmotion(this.GetComponent<EmotionIconPanel>());
    }

    public void SetInfo(string _emotionName) {
        emotionName = _emotionName;
    }

    public void Clicked() {
        GetImage((int)Images.EmotionIcon).color = Color.yellow;
    }

    public void unClicked() {
        GetImage((int)Images.EmotionIcon).color = Color.white;
    }

    public string getName() {
        return emotionName;
    }
}
