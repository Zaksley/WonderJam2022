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

    [SerializeField] private AudioClip _devClip;
    [SerializeField] private AudioClip _platformerClip;
    private AudioSource _audioSource;
    
    public static event EventHandler OnPlayerGotKey;
    public static event EventHandler OnPlayerDie;
    
    void Start()
    {
        State = PlayerState.PLATEFORMER;
        Cursor.SetCursor(_spriteCursor, Vector2.zero, CursorMode.Auto);
        EnableUI(false);
        
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchMode();

            var isPlayerInDevMode = State == PlayerState.DEVELOPER;
            EnableUI(isPlayerInDevMode);
            _audioSource.PlayOneShot(isPlayerInDevMode ? _devClip : _platformerClip, 2.0f);
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
        
        Cursor.visible = State;
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
