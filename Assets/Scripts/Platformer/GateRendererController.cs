using System;
using UnityEngine;


public class GateRendererController : MonoBehaviour
{
    // Components 
    private SpriteRenderer _spriteRenderer;

    // Sprite
    [SerializeField] private Sprite _spriteEndLevel; 

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.OnPlayerGotKey += UpdateSprite; 
    }

    private void UpdateSprite(object sender, EventArgs args)
    {
        _spriteRenderer.sprite = _spriteEndLevel; 
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerGotKey -= UpdateSprite; 
    }
}
