using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField] private float _respawnTime = 1.0f;

    private SpriteRenderer _sprite;
    private ParticleSystem _deathEffect;
    private Rigidbody2D _rigidbody;
    private PlatformerSimulate _simulate;
    private float _initialGravityScale;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _deathEffect = GetComponent<ParticleSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _simulate = GetComponent<PlatformerSimulate>();
        _startPosition = transform.position;
        _startRotation = transform.parent.rotation;

        if (_simulate != null && _rigidbody != null)
        {
            _initialGravityScale = _rigidbody.gravityScale;
        }

        ResetObject();
        
        GameManager.OnPlayerDie += OnPlayerDie;
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDie -= OnPlayerDie;
    }

    private void ResetObject()
    {
        transform.position = _startPosition;
        transform.parent.rotation = _startRotation;

        if (_simulate != null && _rigidbody != null)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.gravityScale = _initialGravityScale;
            _simulate.WantedGravityScale = _initialGravityScale;
        }
    }

    private void OnPlayerDie(object sender, EventArgs e)
    {
        ResetObject();
    }

    public void Explode()
    {
        StartCoroutine(nameof(WaitAndRespawn));
    }
    
    private IEnumerator WaitAndRespawn()
    {
        _sprite.enabled = false;
        if (_deathEffect != null)
            _deathEffect.Play();
        yield return new WaitForSeconds(_respawnTime);
        ResetObject();
        _sprite.enabled = true;
    }
}
