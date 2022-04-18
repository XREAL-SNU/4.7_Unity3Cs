using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _playerManager;
    public static PlayerManager Player
    {
        get => _playerManager;
    }

    private void Awake()
    {
        if (_playerManager == null)
        {
            _playerManager = this;
        }
        else if (_playerManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private GameObject _playerGO;
    private CharacterAppearance _appearance;

    public GameObject PlayerGO
    {
        get => _playerGO;
        set
        {
            _playerGO = value;
            if (_appearance is null)
            {
                _appearance = new CharacterAppearance(_playerGO);
            }
        }
    }

    public CharacterAppearance Apperance
    {
        get => _appearance;
    }
}
