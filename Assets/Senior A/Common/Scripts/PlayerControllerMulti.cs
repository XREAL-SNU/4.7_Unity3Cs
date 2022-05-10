using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerControllerMulti : CharacterMovement
{
    public PhotonView _view;
    const string SUIT_COLOR_KEY = "SuitColor";
    public enum ColorEnum
    {
        Red, Green, Blue
    }
    protected override void Awake()
    {
        _view = GetComponent<PhotonView>();
        if (_view.IsMine)
        {
            base.Awake();
        }
    }

    Color GetColor(ColorEnum col)
    {
        switch (col)
        {
            case ColorEnum.Red:
                return Color.red;
            case ColorEnum.Green:
                return Color.green;
            case ColorEnum.Blue:
                return Color.blue;
        }
        return Color.black;
    }
    Color GetColor_string(string col)
    {
        switch (col)
        {
            case "Red":
                return Color.red;
            case "Green":
                return Color.green;
            case "Blue":
                return Color.blue;
        }
        return Color.black;
    }
    MaterialPropertyBlock block;
    Renderer suitRenderer;
    void Start()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { SUIT_COLOR_KEY, ColorEnum.Blue } });
    }

    public override void OnPlayerPropertiesUpdate(Player player, Hashtable updatedProps)
    {
        
    }

    public void SetColorProperty(ColorEnum col)
    {
        // PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { SUIT_COLOR_KEY, col } });
        _view.RPC("changeColor", RpcTarget.AllBuffered, col.ToString());
    }
    [PunRPC] void changeColor(string _color)
    {
        if (block == null) block = new MaterialPropertyBlock();
       
        //block.SetColor("_Color", GetColor((ColorEnum)updatedProps[SUIT_COLOR_KEY]));
        block.SetColor("_Color", GetColor_string(_color));
        //GameObject playerGo = (GameObject)player.TagObject; //내 캐릭터를 찾는건가? isMine이걸로 대신가능한가 ->Update문에서
        suitRenderer = transform.Find("Space_Suit/Tpose_/Man_Suit/Body").GetComponent<Renderer>(); //playGo

        suitRenderer.SetPropertyBlock(block);
    }
    public override void Update()
    {
        if (_view.IsMine)
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.R)) SetColorProperty(ColorEnum.Red);
            if (Input.GetKeyDown(KeyCode.N)) SetColorProperty(ColorEnum.Green);
            if (Input.GetKeyDown(KeyCode.B)) SetColorProperty(ColorEnum.Blue);

            if (Input.GetKeyDown(KeyCode.J)) PhotonNetwork.Instantiate("Heart/Heart", transform.position + Vector3.up, Quaternion.Euler(-90f, 0, 0));
            if (Input.GetKeyDown(KeyCode.H)) PhotonNetwork.Instantiate("Heart/Heart", transform.position + Vector3.up, Quaternion.Euler(-90f, 0, 0));
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        info.Sender.TagObject = this.gameObject;
        Debug.Log($"Player {info.Sender.NickName}'s Avatar is instantiated/t={info.SentServerTime}");
    }
}