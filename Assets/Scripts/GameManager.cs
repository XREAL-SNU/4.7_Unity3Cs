using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private Dictionary<string, EmotionIconPanel> totalEmotions = new Dictionary<string, EmotionIconPanel>();
    private string[] myEmotionsName = {"afraid", "amazed", "angry", "bored"};
    private Dictionary<string, MyEmotionIconPanel> myEmotions = new Dictionary<string, MyEmotionIconPanel>();
    private Dictionary<string, Door> Doors = new Dictionary<string, Door>();
    private EmotionIconPanel selectedEmotion = null;

    public void AddEmotions(string emotionName, EmotionIconPanel emotionIconPanel)
    {
        totalEmotions.Add(emotionName, emotionIconPanel);
    }
    public void AddMyEmotions(string emotionName, MyEmotionIconPanel myEmotionIconPanel)
    {
        myEmotions.Add(emotionName, myEmotionIconPanel);
    }
    public void AddDoor(string doorName, Door door) {
        Doors.Add(doorName, door);
    }
    public Door getDoor(string name) {
        Door doorOfName = null;
        if(Doors.ContainsKey(name)) {
            doorOfName = Doors[name];
        }
        return doorOfName;
    }

    public EmotionIconPanel getEmotionIconPanel(string name)
    {
        EmotionIconPanel emotionOfName = null;
        if(totalEmotions.ContainsKey(name)) {
            emotionOfName = totalEmotions[name];
        }
        return emotionOfName;
    }

    public string getMyEmtionIconPanel(int index) {
        return myEmotionsName[index];
    }

    public void selectEmotion(EmotionIconPanel _selectedEmotion) {
        if(selectedEmotion != null) {
            selectedEmotion.unClicked();
        }
        selectedEmotion = _selectedEmotion;
        if(selectedEmotion != null) {
            selectedEmotion.Clicked();
        }
    }

    public void changeEmotion(int index, MyEmotionIconPanel _currentEmotion) {
        if(selectedEmotion != null) {
            myEmotionsName[index] = selectedEmotion.getName();
            _currentEmotion.changeEmotion(selectedEmotion.getName());
            selectedEmotion.unClicked();
            selectedEmotion = null;
        }
    }
    
}