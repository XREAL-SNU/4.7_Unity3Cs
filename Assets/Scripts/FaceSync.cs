using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSync : MonoBehaviourPunCallbacks
{
    private Renderer faceRenderer;
    private PhotonView _view;
    private string currentEmotion = "happy";

    private void Awake()
    {
        faceRenderer = gameObject.transform.Find("Helmet_1").GetComponent<Renderer>();
        GameManager.Instance().AddFace(gameObject.GetComponent<FaceSync>());
        _view = gameObject.GetComponent<PhotonView>();
    }

    public void changeFaceSync(string emotionName) {
        if(currentEmotion != emotionName) {
            currentEmotion = emotionName;
            _view.RPC("changeFace", RpcTarget.All, emotionName);
        }
    }
    
    [PunRPC]
    public void changeFace(string emotionName)
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();

        block.SetTexture("_MainTex", Resources.Load<Texture>("Expressions_Avatar/" + emotionName));
        
        faceRenderer.SetPropertyBlock(block);
    }
}
