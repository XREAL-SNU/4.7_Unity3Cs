using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum PartsType1
{
    Backpack,
    Body,
    Boot,
    Glove,
    Helmet
}

public class TintingTest : MonoBehaviour
{
    private GameObject _bodyGO;
    private Material _bodyMaterial;
    private string _partsPathRoot = "Space_Suit/Tpose_/Man_Suit/";
    private Dictionary<string, GameObject> _PartsGameObjects = new Dictionary<string, GameObject>();

    private int _partsCount;
    private int _currentPartsIndex = 0;

    void Start()
    {

        _partsCount = Enum.GetValues(typeof(PartsType1)).Length;

        foreach (PartsType1 PartsType1 in Enum.GetValues(typeof(PartsType1)))
        {
            string partsName = PartsType1.ToString();
            GameObject partsGO = transform.Find(_partsPathRoot + partsName).gameObject;
            _PartsGameObjects.Add(partsName, partsGO);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RandomTintParts();
        }

        if (Input.GetMouseButtonDown(1))
        {
            MoveToNextParts();
        }
    }

    private void RandomTintParts()
    {
        string curentPartsName = ((PartsType1)_currentPartsIndex).ToString();
        GameObject curentPartsGO = _PartsGameObjects[curentPartsName];
        Material mat = curentPartsGO.GetComponent<Renderer>().material;
        mat.SetColor("_Color", UnityEngine.Random.ColorHSV());
    }

    private void MoveToNextParts()
    {
        _currentPartsIndex += 1;

        if (_currentPartsIndex >= _partsCount)
        {
            _currentPartsIndex -= _partsCount;
        }
    }
}