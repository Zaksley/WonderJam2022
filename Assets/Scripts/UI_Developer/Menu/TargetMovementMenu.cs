using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovementMenu : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        speed = -speed;
    }
}
