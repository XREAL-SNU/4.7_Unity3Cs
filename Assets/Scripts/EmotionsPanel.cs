using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class EmotionsPanel : UIPopup
{
    enum Buttons
    {
        CloseButton
    }

    enum GameObjects
    {
        MyEmotionsContainer,
        ScrollContent
    }
    private string[] emotionsName = { "afraid", "amazed", "angry", "bored", "calm", "cool", "disgust", "excited", "fear", "funny", "guilty", "happy", "nervous", "playful", "sad", "shy", "sleepy", "surprised", "thoughtful", "tired", "worried" };
    private string[] myEmotionsName = { "afraid", "amazed", "angry", "bored" };


    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClick_Close);

        Bind<GameObject>(typeof(GameObjects));
        GameObject scrollContent = GetUIComponent<GameObject>((int)GameObjects.ScrollContent);
        scrollContent.BindEvent(OnClick_Panel);
        GameObject myEmotionsContainer = GetUIComponent<GameObject>((int)GameObjects.MyEmotionsContainer);


        foreach (EmotionNames emotionName in Enum.GetValues(typeof(EmotionNames)))
        {
            GameObject emotionIconPanel = UIManager.UI.MakeSubItem<EmotionIconPanel>(scrollContent.transform).gameObject;
            EmotionIconPanel emotionIconPanelScript = emotionIconPanel.GetOrAddComponent<EmotionIconPanel>();
            emotionIconPanelScript.SetInfo(emotionName.ToString());
        }
        int index = 0;
        string[] customEmotionsName = EmotionManager.getEmotionManager().getCustomEmotionNames();
        foreach (string emotionName in customEmotionsName)
        {
            GameObject myEmotionIconPanel = UIManager.UI.MakeSubItem<MyEmotionIconPanel>(myEmotionsContainer.transform).gameObject;
            MyEmotionIconPanel myEmotionIconPanelScript = myEmotionIconPanel.GetOrAddComponent<MyEmotionIconPanel>();
            myEmotionIconPanelScript.SetInfo(index, emotionName);
            index += 1;
        }
    }

    public void OnClick_Panel(PointerEventData data)
    {
        GameManager.Instance().selectEmotion(null);
    }

    public void OnClick_Close(PointerEventData data)
    {
        EmotionManager.getEmotionManager().clearMyEmotionIconPanelsList();
        ClosePopup();
    }
}
