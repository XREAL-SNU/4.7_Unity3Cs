using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventoryCanvas;

    public void OpenInventory()
    {
        inventoryCanvas.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
