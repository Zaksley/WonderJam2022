using System;
using UnityEngine;

public class MoveObjectByCircle : MonoBehaviour
{
    [SerializeField] private Transform _parentToRotate;
    [SerializeField] private float _speed = 5.0f;

    private float _previousRotation;

    private void OnMouseDrag()
    {
        _parentToRotate.rotation = Quaternion.AngleAxis( _previousRotation + (MouseWorldPosition().y - _parentToRotate.position.y) * _speed, Vector3.forward);
    }

    private void OnMouseDown()
    {
        _previousRotation = _parentToRotate.rotation.eulerAngles.z;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(_parentToRotate.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos); 
    }
}