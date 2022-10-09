using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField, Description("Between 0 and 1")] private float _smoothstepPerSecond = 2.75f;
    [SerializeField] private Vector2 _offset = new Vector2(0.0f, 2.0f);

    private Camera _cam;
    private Vector3 _currentVelocity = Vector3.zero;
    
    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        var position = _cam.transform.position;
        var targetPosition = target.transform.position;
        targetPosition.z = position.z;

        _cam.transform.position = Vector3.Lerp(position, targetPosition + new Vector3(_offset.x, _offset.y, 0.0f), Time.fixedDeltaTime * _smoothstepPerSecond);
    }
}