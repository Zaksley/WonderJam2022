using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerSimulate : MonoBehaviour
{
    public float WantedGravityScale { get; set; }
    
    private Rigidbody2D _rigidbody;
    private GameManager.PlayerState _lastState;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        WantedGravityScale = _rigidbody.gravityScale;
    }

    private void Update()
    {
        if (GameManager.State == GameManager.PlayerState.DEVELOPER)
            _rigidbody.gravityScale = 0.0f;

        if (GameManager.State != _lastState && _lastState == GameManager.PlayerState.DEVELOPER)
            _rigidbody.gravityScale = WantedGravityScale;

        _lastState = GameManager.State;
    }
}