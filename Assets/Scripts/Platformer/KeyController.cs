using System;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class KeyController : MonoBehaviour
{
    private void Start()
    {
        GameManager.OnPlayerDie += RestartKey; 
    }

    private void RestartKey(object sender, EventArgs args)
    {
        gameObject.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.PlayerGotKey();
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDie -= RestartKey; 
    }
}
