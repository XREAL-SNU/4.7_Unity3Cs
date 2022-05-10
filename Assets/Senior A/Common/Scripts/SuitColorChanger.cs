using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SuitColorChanger : MonoBehaviourPunCallbacks  //, IInRoomCallbacks
{
    /*


    [SerializeField] // °¡´É?
    public enum ColorEnum{
        Red,
        Green,
        Blue
    }
  
    const string SUIT_COLOR_KEY = "SuitColor";
    public void SetSuitColor(Player player, ColorEnum col)
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetColor("_Color", Material.GetColor(col));

        // get the renderer of the player
        GameObject playerGo = (GameObject)player.TagObject;
        Renderer suitRenderer = playerGo.transform.Find("Space_Suit/Tpose_/Man_Suit/Body").GetComponent<Renderer>();

        suitRenderer.SetPropertyBlock(block);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SetColorProperty(ColorEnum.Red);
        if (Input.GetKeyDown(KeyCode.G)) SetColorProperty(ColorEnum.Green);
        if (Input.GetKeyDown(KeyCode.B)) SetColorProperty(ColorEnum.Blue);
    }
    public void SetColorProperty(ColorEnum col)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { SUIT_COLOR_KEY, col } });
    }
    public override void OnPlayerPropertiesUpdate(Player player, Hashtable updatedProps)
    {
        if (updatedProps.ContainsKey(SUIT_COLOR_KEY))
        {
            SetSuitColor(player, (ColorEnum)updatedProps[SUIT_COLOR_KEY]);
        }
    }

    */
}
