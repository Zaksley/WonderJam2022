using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Properties
    [SerializeField] private float _jumpSpeed = 7.0f;
    [SerializeField] private float _moveSpeed = 150.0f;
    [SerializeField] private float _acceleration = 5.0f;
    [SerializeField] private float _deceleration = 50.0f;

    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private float _raycastLength = 1.0f;

    // Components
    private Rigidbody2D _body;
    
    // Fields
    private float _horizontalDirection = 0.0f;
    private bool _shouldJump = false;
    private bool _isGrounded = false;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        // Check mode
        _body.simulated = GameManager.State == GameManager.PlayerState.PLATEFORMER;

        // Input
        _horizontalDirection = Input.GetAxisRaw("Horizontal");
        
        if (!_shouldJump && _isGrounded)
            _shouldJump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _isGrounded = CheckIfGrounded();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }

    private void MovePlayer()
    {
        // Horizontal movement
        // Check if we start moving (accelerating) or stop moving (decelerating)
        var deltaAcceleration = Mathf.Abs(_horizontalDirection) > 0.0f ? _acceleration : _deceleration; 

        var horizontalVelocity = Mathf.Lerp(
            _body.velocity.x, 
            _horizontalDirection * _moveSpeed * Time.fixedDeltaTime,
            deltaAcceleration * Time.fixedDeltaTime);
        
        // Vertical jump
        var verticalVelocity = _shouldJump ? _jumpSpeed : _body.velocity.y;  
        
        // Final velocity
        _body.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        
        // We have jump now
        _shouldJump = false;
    }
    
    private bool CheckIfGrounded()
    {
        // We raycast down to check for a collider, if a collider was hit, we are grounded
        var result = Physics2D.Raycast(_raycastOrigin.position, new Vector2 (0.0f, -1.0f), _raycastLength, ~LayerMask.NameToLayer("Platform"));

        return result.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_raycastOrigin.position, new Vector3(0.0f, -_raycastLength, 0.0f));
    }
}
