using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static GameObject go;
    void Start()
    {
        go = Instantiate(Resources.Load<GameObject>("Prefabs/FaceUI"));
    }

    public void ShowFaceUI()
    {
        go.SetActive(true);
    }
}
