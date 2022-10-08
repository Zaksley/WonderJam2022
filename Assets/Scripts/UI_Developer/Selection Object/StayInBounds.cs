using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StayInBounds : MonoBehaviour
{
    [SerializeField] private Vector2 _bounds;
    [SerializeField] private Color _gizmoColor = new Color(1.0f, 0.0f, 0.0f, 0.25f);

    private Vector2 _initialPosition;
    private Vector2 _spriteSize;

    private void Start()
    {
        _initialPosition = gameObject.transform.position;
        _spriteSize = transform.GetComponent<Collider2D>().bounds.size / 2.0f;
    }

    private void Awake()
    {
        _initialPosition = gameObject.transform.position;
    }

    private void LateUpdate()
    {
        var gameObjectPos = gameObject.transform.position;

        gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObjectPos.x,  _initialPosition.x - _bounds.x + _spriteSize.x, _initialPosition.x + _bounds.x - _spriteSize.x),
            Mathf.Clamp(gameObjectPos.y, _initialPosition.y - _bounds.y + _spriteSize.y, _initialPosition.y + _bounds.y - _spriteSize.y),
            gameObjectPos.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube(Application.isPlaying ?  _initialPosition : gameObject.transform.position, _bounds * 2.0f);
    }
}