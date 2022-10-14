using System;
using UnityEngine;


public class PlatformerSimulate : MonoBehaviour
{
    public float WantedGravityScale { get; set; }
    private Vector2 _storeVelocity = Vector2.zero; 
    
    private Rigidbody2D _rigidbody;
    private GameManager.PlayerState _lastState;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        WantedGravityScale = _rigidbody.gravityScale;
    }

    private void Update()
    {
        _rigidbody.isKinematic = (GameManager.State == GameManager.PlayerState.DEVELOPER);
        
        if (GameManager.State != _lastState)
        {
            if (_lastState == GameManager.PlayerState.PLATEFORMER)
            {
                StorePreviousParameters();
                _rigidbody.velocity = Vector2.zero;
            }
            else if (_lastState == GameManager.PlayerState.DEVELOPER)
            {
                _rigidbody.gravityScale = WantedGravityScale;
                _rigidbody.velocity = _storeVelocity;  
            }
        }

        _lastState = GameManager.State;
    }

    private void StorePreviousParameters()
    {
        _storeVelocity = _rigidbody.velocity; 
    }
}
