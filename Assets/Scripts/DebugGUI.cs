using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGUI : MonoBehaviour
{
    static string myLog = "";
    private string output;
    private string stack;

    void OnEnable() {
        Application.logMessageReceived += Log;
    }

    void OnDisable() {
        Application.logMessageReceived -= Log;
    }

    public void Log(string logString, string stackTrace, LogType type) {
        output = logString;
        stack = stackTrace;
        myLog = output + "\n" + myLog;
        if (myLog.Length > 5000) {
            myLog = myLog.Substring(0, 50000);
        }
    }

    void OnGUI() {
        if(!Application.isEditor) {
            myLog = GUI.TextArea(new Rect(10, 1000, Screen.width - 1500, Screen.height - 10), myLog);
        }
    }
}
