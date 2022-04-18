using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum PartsType
{
    Backpack,
    Body,
    Boot,
    Glove,
    Helmet
}

public class CharacterAppearance
{
    public Dictionary<string, GameObject> CustomParts = new Dictionary<string, GameObject>();
    private string _partsPathRoot = "Space_Suit/Tpose_/Man_Suit/";
    private int _partsCount;

    public CharacterAppearance(GameObject target)
    {
        _partsCount = Enum.GetValues(typeof(PartsType)).Length;

        foreach (PartsType partsType in Enum.GetValues(typeof(PartsType)))
        {
            string partsName = partsType.ToString();
            GameObject partsGO = target.transform.Find(_partsPathRoot + partsName).gameObject;
            CustomParts.Add(partsName, partsGO);
        }
    }

    public void ApplyColor(GameObject obj, Color color)
    {
        Material material = obj.GetComponent<Renderer>().material;
        material.SetColor("_Color", color);
    }
}
