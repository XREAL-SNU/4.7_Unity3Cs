using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 
namespace DebugStuff
{
    public class DebuggingLog : MonoBehaviour
    {
    //#if !UNIITY EDITOR
        static string myLog="";
        private string output;
        private string stack;

        void OnEnable() 
        {
            Application.logMessageReceived += Log;
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnDisable() 
        {
            Application.logMessageReceived -= Log;
            //SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        public void Log(string logString, string stackTrace, LogType type)
        {
            output=logString;
            stack=stackTrace;
            myLog=output + "\n"+myLog;
            if(myLog.Length>5000)
            {
                myLog=myLog.Substring(0,4000);
            }
        }

        void OnGUI() 
        {
            //if (!Application.isEditor)//Do not display in editor
            myLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), myLog);
        }
    }
}

