using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NameTextViewer : MonoBehaviour
{
    public TextMeshPro nameText;
    private PhotonView photonView;
    void Start()
    {
        photonView=this.gameObject.GetComponent<PhotonView>();
        if(photonView.IsMine)
        {
            nameText.gameObject.SetActive(false);
            return;
        }
        SetName();
        Debug.Log("Player NameTage"+nameText.text + photonView.Owner.NickName);
    }

    private void SetName() => nameText.text=photonView.Owner.NickName;
}
