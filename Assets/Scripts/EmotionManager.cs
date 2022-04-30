using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EmotionNames
{
    afraid, amazed, angry, bored, calm, cool, disgust, excited, fear, funny, guilty, happy, nervous, playful, sad, shy, sleepy, surprised, thoughtful, tired, worried
}

public class EmotionManager : MonoBehaviour
{
    private static EmotionManager _EmotionManager;

    public static EmotionManager getEmotionManager()
    {
        return _EmotionManager;
    }
    void Awake()
    {
        if (_EmotionManager == null)
        {
            _EmotionManager = this;
        }
        else if (_EmotionManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private static int customEmotionNum = 4;
    private static string[] customEmotionNames = new string[customEmotionNum];
    private List<MyEmotionIconPanel> myEmotionIconPanelsList = new List<MyEmotionIconPanel>();

    public void Start()
    {
        for (int i = 0; i < customEmotionNum; i++)
        {
            EmotionNames emotionName = (EmotionNames)i;
            customEmotionNames[i] = emotionName.ToString();
        }
    }

    public void addMyEmotionIconPanelsList(MyEmotionIconPanel _myEmotionIconPanel)
    {
        myEmotionIconPanelsList.Add(_myEmotionIconPanel);
    }

    public void clearMyEmotionIconPanelsList()
    {
        myEmotionIconPanelsList.Clear();
    }

    public void changeCustomEmotions(int index, string _emotionName)
    {
        int alreadyHasEmotion = Array.IndexOf(customEmotionNames, _emotionName);
        if (alreadyHasEmotion >= 0)
        {
            customEmotionNames[alreadyHasEmotion] = customEmotionNames[index];
            changeCustomEmotionsUI(alreadyHasEmotion);
        }
        customEmotionNames[index] = _emotionName;
        changeCustomEmotionsUI(index);
    }

    public void changeCustomEmotionsUI(int index)
    {
        myEmotionIconPanelsList[index].changeEmotion(customEmotionNames[index]);
    }

    public int getCustomEmotionNum()
    {
        return customEmotionNum;
    }

    public string[] getCustomEmotionNames()
    {
        return customEmotionNames;
    }
    public string getEmotion(int index)
    {
        return customEmotionNames[index];
    }
}
