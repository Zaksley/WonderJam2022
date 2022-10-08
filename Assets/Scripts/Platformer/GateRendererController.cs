using System;
using UnityEngine;


public class GateRendererController : MonoBehaviour
{
    // Components 
    private SpriteRenderer _spriteRenderer;

    // Sprite
    [SerializeField] private Sprite _spriteStartLevel; 
    [SerializeField] private Sprite _spriteEndLevel; 

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.OnPlayerGotKey += UpdateSprite;
        GameManager.OnPlayerDie += RestartSprite; 
    }

    private void UpdateSprite(object sender, EventArgs args)
    {
        _spriteRenderer.sprite = _spriteEndLevel; 
    }

    private void RestartSprite(object sender, EventArgs args)
    {
        _spriteRenderer.sprite = _spriteStartLevel; 
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerGotKey -= UpdateSprite; 
        GameManager.OnPlayerDie -= RestartSprite; 
    }
}
