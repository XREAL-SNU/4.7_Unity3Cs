using System;
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

    private Dictionary<string, Door> Doors = new Dictionary<string, Door>();
    private EmotionIconPanel selectedEmotion = null;
    private AvatarFaceControl avatarFaceControl = null;
    public void AddDoor(string doorName, Door door)
    {
        Doors.Add(doorName, door);
    }
    public void AddFace(AvatarFaceControl _avatarFaceControl)
    {
        avatarFaceControl = _avatarFaceControl;
    }
    public Door getDoor(string name)
    {
        Door doorOfName = null;
        if (Doors.ContainsKey(name))
        {
            doorOfName = Doors[name];
        }
        return doorOfName;
    }
    public AvatarFaceControl getAvatarFaceControl()
    {
        return avatarFaceControl;
    }

    public void selectEmotion(EmotionIconPanel _selectedEmotion)
    {
        if (selectedEmotion != null)
        {
            selectedEmotion.unClicked();
        }
        selectedEmotion = _selectedEmotion;
        if (selectedEmotion != null)
        {
            selectedEmotion.Clicked();
        }
    }

    public void changeEmotion(int index, MyEmotionIconPanel _currentEmotion)
    {
        if (selectedEmotion != null)
        {
            EmotionManager.getEmotionManager().changeCustomEmotions(index, selectedEmotion.getName());
            selectedEmotion.unClicked();
            selectedEmotion = null;
        }
    }

}