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
    
    void Start()
    {
        State = PlayerState.DEVELOPER; 
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchMode();

            bool IsPlayerInDevMode = State == PlayerState.DEVELOPER; 
            EnableUI(IsPlayerInDevMode);

            Cursor.visible = !Cursor.visible; 
        }
        
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
