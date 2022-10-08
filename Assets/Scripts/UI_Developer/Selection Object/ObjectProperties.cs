using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class ObjectProperties : MonoBehaviour
{

    [SerializeField] private Toggle _colliderToggle;
    private Collider2D _collider;
    private bool _isActive = true;

    private int _withoutPlayerLayer;
    private int _platformLayer;

    private bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            
            if (_isActive)
                SyncUI();
        }
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _platformLayer = LayerMask.NameToLayer("Platform");
        _withoutPlayerLayer = LayerMask.NameToLayer("WithoutPlayer");

        SyncUI();
        _colliderToggle.onValueChanged.AddListener(SetColliderEnabled);
    }

    private void Update()
    {
        IsActive = (gameObject == GlobalVariable.ObjectSelected);
    }

    private void SyncUI()
    {
        _colliderToggle.isOn = (_collider.gameObject.layer == _platformLayer);
    }
        
    private void SetColliderEnabled(bool value)
    {
        if (IsActive)
            _collider.gameObject.layer = value ? _platformLayer : _withoutPlayerLayer;
    }
}
