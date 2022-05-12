using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class ChangeFace : MonoBehaviour,IPointerClickHandler
{
    
    private static ChangeFace _instance;
    public static ChangeFace Instance()
    {
        return _instance;
    }
    private void Awake() 
    {
        _instance=this;
    }
    
    private GameObject emotionScrollPanel;
    private GameObject openBtn;
    private GameObject panel;
    private GameObject emotionPrefab;
    private GameObject quickSlotPanel;
    public GameObject[] quickEmotions = new GameObject[4];
    private Sprite[] images;
    public GameObject player;
    public GameObject selectedBtn;
    private Texture2D[] quickEmotionTextures=new Texture2D[5];
    string emotionName;
    // Start is called before the first frame update
    PhotonView photonView;
    void Start()
    {
        Init();
    }

    private void Init()
    {
        photonView=this.gameObject.GetComponent<PhotonView>();

        GameObject canvas=GameObject.FindGameObjectWithTag("Canvas");
        emotionScrollPanel=canvas.transform.Find("Panel/EmotionScrollRect/ScrollPanel").gameObject;
        openBtn=canvas.transform.Find("OpenBtn").gameObject;
        emotionPrefab = Resources.Load<GameObject>("EmotionButton");
        images = Resources.LoadAll<Sprite>("SeniorA/UI/Expressions_UI");
        panel = canvas.transform.GetChild(0).gameObject;
        UpdateQuickEmotions();

        for (int i = 0; i < images.Length; i++)
        {
            GameObject emotionInstance = Instantiate(emotionPrefab);
            emotionInstance.transform.SetParent(emotionScrollPanel.transform);
            var image = emotionInstance.transform.Find("Image");
            image.GetComponent<Image>().sprite = images[i];
            var text = emotionInstance.transform.Find("Text");
            text.GetComponent<Text>().text = images[i].name;
        }
        quickEmotionTextures[4]=Resources.Load<Texture2D>("SeniorA/UI/Expressions_Avatar/happy");
    }

    private void Update() 
    {
        if(photonView.IsMine)

        {
            if(Input.GetKey(KeyCode.Alpha1))
                PhotonView.Get(this).RPC("UpdateFace", RpcTarget.All, (int)0);
            if(Input.GetKey(KeyCode.Alpha2))
                PhotonView.Get(this).RPC("UpdateFace", RpcTarget.All, (int)1);
            if(Input.GetKey(KeyCode.Alpha3))
                PhotonView.Get(this).RPC("UpdateFace", RpcTarget.All, (int)2);
            if(Input.GetKey(KeyCode.Alpha4))
                PhotonView.Get(this).RPC("UpdateFace", RpcTarget.All, (int)3);
        }
    }

    [PunRPC]
    //중복체크함수
    public int CheckIsSameFace()
    {
        UpdateQuickEmotions();
        for (int i = 0; i < 4; i++)
        {
            if(selectedBtn.transform.GetChild(0).gameObject.GetComponent<Image>().sprite==quickEmotions[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
            {
                return i;
            }
        }
        return 5;
    }
    //Face material 
    [PunRPC]
    public void UpdateQuickEmotions()
    {
        quickSlotPanel = panel.transform.GetChild(0).gameObject;
        int temp = quickSlotPanel.transform.childCount;
        for (int i = 1; i < temp; i++)
        {
            quickEmotions[i - 1] = quickSlotPanel.transform.GetChild(i).gameObject;

            emotionName=quickEmotions[i-1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name;
            if(emotionName != "UIMask")
                quickEmotionTextures[i-1]=Resources.Load<Texture2D>("SeniorA/UI/Expressions_Avatar/"+ emotionName);
        }
    }


    [PunRPC]
    private void UpdateFace(int faceNum)
    {
        player=GameObject.FindWithTag("Player");
        UpdateQuickEmotions();
        StartCoroutine(UpdateFaceCoroutine(faceNum));
    }

    [PunRPC]
    IEnumerator UpdateFaceCoroutine(int faceNum)
    {
        if(quickEmotionTextures[faceNum] != null)
            player.transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().material.mainTexture= quickEmotionTextures[faceNum];

        yield return new WaitForSeconds(10.0f);

        player.transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().material.mainTexture= quickEmotionTextures[4];
    }

    [PunRPC]
    public void OnPointerClick(PointerEventData eventData)
    {
        selectedBtn=null;
    }

    
}
