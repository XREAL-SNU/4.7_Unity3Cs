using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SuitColorSync : MonoBehaviourPunCallbacks
{
    private bool inputR;
    private bool inputG;
    private bool inputB;

    public void Start() {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable{ {"SuitColor", "White"}});
    }

    public void Update() {
        inputR = Input.GetKey(KeyCode.R);
        inputG = Input.GetKey(KeyCode.G);
        inputB = Input.GetKey(KeyCode.B);
        if(inputR) {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable{ {"SuitColor", "Red"}});
        }
        else if(inputG) {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable{ {"SuitColor", "Green"}});
        }
        else if(inputB) {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable{ {"SuitColor", "Blue"}});
        }
    }

    private Material suit;

    private Color TransformColor(string colorText) {
        switch(colorText) {
            case "Red":
            return Color.red;
            case "Green":
            return Color.green;
            case "Blue":
            return Color.blue;
            default:
            return Color.white;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
        Debug.Log("A");
        Debug.Log(targetPlayer);
        Debug.Log("B");
        GameObject playerChange = (GameObject)targetPlayer.TagObject;
        Debug.Log(playerChange + "@");
        Debug.Log("C");
        GameObject suitG = playerChange.transform.Find("Space_Suit/Tpose_").gameObject;
        Debug.Log(suitG + "#");
        Debug.Log("D");

        suit.SetColor("_Color", TransformColor(changedProps["SuitColor"].ToString()));
    }
}
