using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _followDistance = 0.5f;
    [SerializeField] private Camera _cam;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (target == null)
            return;

        var position = transform.position;
        var targetPosition = target.transform.position;
        targetPosition.z = position.z;
        var targetVector = targetPosition - position;

        if (targetVector.magnitude > _followDistance)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                Time.deltaTime * _followSpeed
            );
        }
    }
}