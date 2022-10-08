using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum PlayerState
    {
        DEVELOPER,
        PLATEFORMER
    };

    public static PlayerState State { get; private set; }
    [SerializeField] private List<GameObject> _objectsUI = new List<GameObject>();

    [SerializeField] private Texture2D _spriteCursor; 
    
    public static event EventHandler OnPlayerGotKey;
    public static event EventHandler OnPlayerDie;
    
    void Start()
    {
        State = PlayerState.DEVELOPER; 
        Cursor.SetCursor(_spriteCursor, Vector2.zero, CursorMode.Auto);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchMode();

            bool IsPlayerInDevMode = State == PlayerState.DEVELOPER;
            EnableUI(IsPlayerInDevMode);
            Cursor.visible = IsPlayerInDevMode;
        }
    }

    public static void UpdatePlayerKeyStatus()
    {
        OnPlayerGotKey?.Invoke(null, null);    
    }

    public static void UpdatePlayerAliveStatus()
    {
        OnPlayerDie?.Invoke(null, null);    
    }
    
    private void EnableUI(bool State)
    {
        for (int indexUI = 0; indexUI < _objectsUI.Count; indexUI++)
        {
            _objectsUI[indexUI].SetActive(State);
        }
    }
    
    private void SwitchMode()
    {
        if (State == PlayerState.DEVELOPER)
        {
            State = PlayerState.PLATEFORMER; 
        }
        else
        {
            State = PlayerState.DEVELOPER; 
        }
    }
}
