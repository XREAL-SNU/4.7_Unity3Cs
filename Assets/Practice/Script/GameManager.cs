using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        PlayerManager.Player.PlayerGO = GameObject.FindGameObjectWithTag("Player");
    }
}