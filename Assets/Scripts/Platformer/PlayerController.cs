using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Properties
    [SerializeField] private float _jumpSpeed = 7.0f;
    [SerializeField] private float _moveSpeed = 150.0f;
    [SerializeField] private float _acceleration = 5.0f;
    [SerializeField] private float _deceleration = 50.0f;

    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private float _raycastLength = 1.0f;


    [SerializeField] private Transform _startTransform;
    [SerializeField] private float _respawnTime = 1.0f;

    public bool HasKey { get; private set; } = false;

    // Components
    private Rigidbody2D _body;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private ParticleSystem _deathEffect;

    // Fields
    private float _lastHorizontalDirection = 0.0f;
    private float _horizontalDirection = 0.0f;
    private bool _shouldJump = false;
    private bool _wasSimulated = false;
    private bool _wasGrounded = false;
    private bool _isGrounded = false;
    private bool _isAlive = true;

    // Cache
    private static readonly int HorizontalVelocityHash = Animator.StringToHash("horizontalVelocity");
    private static readonly int VerticalVelocityHash = Animator.StringToHash("verticalVelocity");
    private static readonly int JumpingHash = Animator.StringToHash("jumping");

    // Audio
    private AudioSource _playerAudioSource;
    private AudioSource _walkAudioSource;

    [SerializeField] private AudioClip _keyPickupClip;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private AudioClip _walkClip;
    [SerializeField] private AudioClip _jumpClip;

    // Unity events

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _deathEffect = GetComponent<ParticleSystem>();
        _playerAudioSource = GetComponent<AudioSource>();

        _walkAudioSource = gameObject.AddComponent<AudioSource>();
        _walkAudioSource.loop = true;
        _walkAudioSource.clip = _walkClip;

        gameObject.transform.position = _startTransform.position;
    }

    private void Update()
    {
        var simulate = ((GameManager.State == GameManager.PlayerState.PLATEFORMER) && _isAlive);

        // Check mode
        _wasSimulated = _body.simulated;
        _body.simulated = simulate;
        _animator.enabled = simulate;

        if (!simulate)
        {
            _walkAudioSource.Stop();
            return;
        }

        // Input
        ProcessInputs();

        // Animations
        Animate();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void PlayerGotKey()
    {
        HasKey = true;
        _playerAudioSource.PlayOneShot(_keyPickupClip, 2f);
        GameManager.UpdatePlayerKeyStatus();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _wasGrounded = _isGrounded;
        _isGrounded = CheckIfGrounded();
        
        if (!_wasGrounded && _isGrounded)
            StartPlayJump();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_raycastOrigin.position, new Vector3(0.0f, -_raycastLength, 0.0f));
    }

    // Member functions

    // Make the player die and respawn at start position
    public void Die()
    {
        Debug.Log("Player start dying");
        StartCoroutine(nameof(WaitAndRespawn));
    }

    // Wait for respawn time and set object active at start position
    private IEnumerator WaitAndRespawn()
    {
        _isAlive = false;
        _sprite.enabled = false;
        _deathEffect.Play();
        _playerAudioSource.PlayOneShot(_deathClip, 4f);
        yield return new WaitForSeconds(_respawnTime);
        HasKey = false;
        GameManager.UpdatePlayerAliveStatus();
        gameObject.transform.position = _startTransform.position;
        _isAlive = true;
        _sprite.enabled = true;
        Debug.Log("Player finished dying");
    }

    // Move the rigidbody of the player according to inputs
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

    // Raycast down to check for a collider, if a collider was hit, we are grounded
    private bool CheckIfGrounded()
    {
        var result = Physics2D.Raycast(_raycastOrigin.position, new Vector2(0.0f, -1.0f), _raycastLength,
            ~LayerMask.NameToLayer("Platform"));
        return result.collider != null;
    }

    // Update variables from inputs
    private void ProcessInputs()
    {
        if (_horizontalDirection != 0.0f)
            _lastHorizontalDirection = _horizontalDirection;

        _horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (!_shouldJump && _isGrounded)
            _shouldJump = Input.GetButtonDown("Jump");
    }

    // Animate the player
    private void Animate()
    {
        _animator.SetFloat(HorizontalVelocityHash, Mathf.Abs(_body.velocity.x));
        _animator.SetFloat(VerticalVelocityHash, _body.velocity.y);
        _animator.SetBool(JumpingHash, !_isGrounded);
        _sprite.flipX = _lastHorizontalDirection < 0.5f; // _body.velocity.x < 0.0f;
    }

    private void StartPlayWalk()
    {
        _walkAudioSource.Play();
    }

    private void StopPlayWalk()
    {
        _walkAudioSource.Stop();
    }

    private void StartPlayJump()
    {
        _playerAudioSource.PlayOneShot(_jumpClip, 2.0f);
    }
}
