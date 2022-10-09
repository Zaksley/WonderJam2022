using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField] private float _respawnTime = 1.0f;
    
    private ParticleSystem _deathEffect;
    private Rigidbody2D _rigidbody;
    private PlatformerSimulate _simulate;
    private float _initialGravityScale;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Start()
    {
        _deathEffect = GetComponent<ParticleSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _simulate = GetComponent<PlatformerSimulate>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;

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
        transform.rotation = _startRotation;

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
        if (_deathEffect != null)
            _deathEffect.Play();
        yield return new WaitForSeconds(_respawnTime);
        ResetObject();
    }
}
