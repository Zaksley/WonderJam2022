using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LineRenderer))]
public class StayInBounds : MonoBehaviour
{
    [SerializeField] private Vector2 _bounds;
    [SerializeField] private Color _gizmoColor = new Color(1.0f, 0.0f, 0.0f, 0.25f);

    private LineRenderer _lines;
    private Vector2 _initialPosition;
    private Vector2 _spriteSize;

    public bool ShowOutline
    {
        get => _lines.enabled;
        set => _lines.enabled = value;
    }
    
    private void Start()
    {
        _initialPosition = gameObject.transform.position;
        _spriteSize = transform.GetComponent<Collider2D>().bounds.size / 2.0f;

        var leftUpCorner = _initialPosition - _bounds;
        var rightDownCorner = _initialPosition + _bounds;
        _lines = GetComponent<LineRenderer>();
        _lines.SetPositions(new Vector3[]
        {
            new(leftUpCorner.x, leftUpCorner.y, 0.0f),
            new(rightDownCorner.x, leftUpCorner.y, 0.0f),
            new(rightDownCorner.x, rightDownCorner.y, 0.0f),
            new(leftUpCorner.x, rightDownCorner.y, 0.0f),
        });
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