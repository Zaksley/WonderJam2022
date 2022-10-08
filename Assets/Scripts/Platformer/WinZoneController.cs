using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class WinZoneController : MonoBehaviour
{
    // Components 
    private SpriteRenderer _spriteRenderer;
    private Animator _animator; 
    
    // Sprite
    [SerializeField] private Sprite _spriteStartLevel;
    [SerializeField] private Sprite _spriteEndLevel;
    private float _timeJumpNextScene = 0.5f; 
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>(); 

        GameManager.OnPlayerGotKey += UpdateSprite; 
        GameManager.OnPlayerDie += RestartSprite; 
    }

    private void UpdateSprite(object sender, EventArgs args)
    {
        _animator.SetTrigger("GotKey");
        _spriteRenderer.sprite = _spriteEndLevel; 
    }
    
    private void RestartSprite(object sender, EventArgs args)
    {
        _animator.Play("WinCorrupted");
        _spriteRenderer.sprite = _spriteStartLevel; 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            if (player.HasKey)
            {
                StartCoroutine(JumpNextScene()); 
            }
        }
    }
    
    private void OnDestroy()
    {
        GameManager.OnPlayerGotKey -= UpdateSprite; 
        GameManager.OnPlayerDie -= RestartSprite; 
    }
    
    private IEnumerator JumpNextScene()
    {
        yield return new WaitForSeconds(_timeJumpNextScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
