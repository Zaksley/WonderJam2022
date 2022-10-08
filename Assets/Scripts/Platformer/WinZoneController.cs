using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WinZoneController : MonoBehaviour
{
    // Components 
    private SpriteRenderer _spriteRenderer;
    private Animator _animator; 
    
    // Sprite
    [SerializeField] private Sprite _spriteEndLevel; 
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>(); 

        GameManager.OnPlayerGotKey += UpdateSprite; 
    }

    private void UpdateSprite(object sender, EventArgs args)
    {
        _animator.SetTrigger("GotKey");
        _spriteRenderer.sprite = _spriteEndLevel; 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            if (player.HasKey)
            {
                Debug.Log("WINNER");
            }
        }
    }
}
