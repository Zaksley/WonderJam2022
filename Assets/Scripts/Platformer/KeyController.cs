using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class KeyController : MonoBehaviour
{
    public float floatFrequency = .5f;
    public float floatAmplitude = .01f;
    
    private float _startHeight;

    private void Start()
    {
        _startHeight = transform.position.y;
        StartCoroutine(FloatAnimation());
        GameManager.OnPlayerDie += RestartKey; 
    }

    private void RestartKey(object sender, EventArgs args)
    {
        gameObject.SetActive(true);
        StartCoroutine(FloatAnimation());
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

    private IEnumerator FloatAnimation()
    {
        // Sine-like floating animation
        while (true)
        {
            var nextPos = transform.position;
            nextPos.y = _startHeight +
                        (float)Math.Sin(Time.fixedTime * Math.PI * floatFrequency) * floatAmplitude * .3f;
            transform.position = nextPos;

            yield return null;
        }
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDie -= RestartKey; 
    }
}
