using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerGlitcher : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.right;
    [SerializeField] float speed = 4;
    float randomSpeed = 1;
    [SerializeField] bool move = true;
    bool toLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwitchDirection()
    {
        toLeft = !toLeft;
        direction *= -1;
        randomSpeed = Random.Range(0.25f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Vector3 movement = direction * speed * randomSpeed * Time.deltaTime;
            if (toLeft) movement *= 8;

            transform.position += movement;
        }
    }
}
