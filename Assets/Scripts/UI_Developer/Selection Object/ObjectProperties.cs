using System;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LineRenderer))]
public class ObjectProperties : MonoBehaviour
{

    public bool gravity;
    public bool collision;



    
    [SerializeField] private Toggle _colliderToggle;
    private Collider2D _collider;
    private LineRenderer _line;
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
        _line = GetComponent<LineRenderer>();
        _platformLayer = LayerMask.NameToLayer("Platform");
        _withoutPlayerLayer = LayerMask.NameToLayer("WithoutPlayer");

        SyncUI();
        _colliderToggle.onValueChanged.AddListener(SetColliderEnabled);
    }

    private void Update()
    {
        IsActive = (gameObject == GlobalVariable.ObjectSelected);
        _line.enabled = IsActive;
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
