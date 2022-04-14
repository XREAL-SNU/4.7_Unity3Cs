using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance()
    {
        return _instance;
    }
    private void Awake() 
    {
        _instance=this;
    }
    public GameObject emotionScrollPanel;
    public GameObject openBtn;
    private GameObject panel;
    private GameObject emotionPrefab;
    private GameObject quickSlotPanel;
    private GameObject[] quickEmotions = new GameObject[4];
    private Sprite[] images;
    public GameObject player;
    public GameObject selectedBtn;
    public Texture2D[] quickEmotionTextures=new Texture2D[4];
    string emotionName;
    // Start is called before the first frame update
    void Start()
    {
        Init();

    }

    private void Init()
    {
        emotionPrefab = Resources.Load<GameObject>("EmotionButton");
        images = Resources.LoadAll<Sprite>("SeniorA/UI/Expressions_UI");
        panel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        player=GameObject.FindWithTag("Player");
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
    }

    private void Update() 
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            UpdateFace(0);
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            UpdateFace(1);
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            UpdateFace(2);
        }
        if(Input.GetKey(KeyCode.Alpha4))
        {
            UpdateFace(3);
        }

    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        openBtn.SetActive(true);
    }
    public void OpenPanel()
    {
        panel.SetActive(true);
        openBtn.SetActive(false);
    }


    //Face material 미완성
    private void UpdateQuickEmotions()
    {
        quickSlotPanel = panel.transform.GetChild(0).gameObject;
        int temp = quickSlotPanel.transform.childCount;
        for (int i = 1; i < temp; i++)
        {
            quickEmotions[i - 1] = quickSlotPanel.transform.GetChild(i).gameObject;

            emotionName=quickEmotions[i-1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name;
            Debug.Log(emotionName);
            quickEmotionTextures[i-1]=Resources.Load<Texture2D>("Assets/Resources/SeniorA/UI/Expressions_Avatar/"+ emotionName);
        }
    }
    private void UpdateFace(int faceNum)
    {
        UpdateQuickEmotions();
        player.transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().material.SetTexture(emotionName, quickEmotionTextures[faceNum]);
    }
}
