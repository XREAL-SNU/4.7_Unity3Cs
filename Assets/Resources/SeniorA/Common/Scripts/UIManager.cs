using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{    
    public GameObject panel;
    public GameObject openBtn;
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
}
