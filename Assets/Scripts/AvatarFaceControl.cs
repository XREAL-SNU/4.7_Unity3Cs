using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarFaceControl : MonoBehaviour
{
    private Material face;

    private void Start()
    {
        face = gameObject.GetComponent<Renderer>().material;
        GameManager.Instance().AddFace(gameObject.GetComponent<AvatarFaceControl>());
    }

    public void changeFace(string emotionName)
    {
        face.SetTexture("_MainTex", Resources.Load<Texture>("Expressions_Avatar/" + emotionName));
    }
}
