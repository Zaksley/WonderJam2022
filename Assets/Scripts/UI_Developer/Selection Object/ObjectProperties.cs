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
    
    public bool IsActive
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

        SyncUI();
        _colliderToggle.onValueChanged.AddListener(SetColliderEnabled);
    }

    private void SyncUI()
    {
        _colliderToggle.isOn = _collider.enabled;
    }
        
    private void SetColliderEnabled(bool value)
    {
        if (IsActive && value != _collider.enabled)
            _collider.enabled = value;
    }
}
