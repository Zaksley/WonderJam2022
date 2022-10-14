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
    [Space]
    [SerializeField] FloaterDoor floatingDisk;
    Vector2 initialPos;
    Vector2 initialDoorPos;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>(); 

        GameManager.OnPlayerGotKey += UpdateSprite; 
        GameManager.OnPlayerDie += RestartSprite;
        initialPos = transform.position;
        initialDoorPos = floatingDisk.doorFrame.position;
    }

    [ContextMenu("Float Toggle")]
    void ToggleFloater()
    {
        SetFloater(!floatingDisk.run);
        
    }

    void SetFloater(bool state)
    {
        floatingDisk.run = state;
        transform.position = initialPos;
        floatingDisk.doorFrame.position = initialDoorPos;
    }

    private void UpdateSprite(object sender, EventArgs args)
    {
        _animator.SetTrigger("GotKey");
        _spriteRenderer.sprite = _spriteEndLevel;
        SetFloater(true);
    }


    
    private void RestartSprite(object sender, EventArgs args)
    {
        _animator.Play("WinCorrupted");
        _spriteRenderer.sprite = _spriteStartLevel;
        SetFloater(false);
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
