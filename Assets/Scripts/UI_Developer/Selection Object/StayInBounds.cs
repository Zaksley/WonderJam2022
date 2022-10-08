using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StayInBounds : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] private Color _gizmoColor = new Color(1.0f, 0.0f, 0.0f, 0.25f);

    private Vector2 _spriteSize;

    private void Start()
    {
        _spriteSize = transform.GetComponent<SpriteRenderer>().bounds.size / 2.0f;
    }

    private void LateUpdate()
    {
        var gameObjectPos = gameObject.transform.position;

        gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObjectPos.x, _bounds.min.x + _spriteSize.x, _bounds.max.x - _spriteSize.x),
            Mathf.Clamp(gameObjectPos.y, _bounds.min.y + _spriteSize.y, _bounds.max.y - _spriteSize.y),
            gameObjectPos.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube(_bounds.center, _bounds.size);
    }
}