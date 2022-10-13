using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FloaterDoor : MonoBehaviour
{

    public float floatFrequency = .5f;
    public float floatAmplitude = .01f;
    public bool invertDirection = false;
    public Transform doorFrame;

    private float _startHeight;
    public bool run = false;
    private bool isRunning = false;
    
    private Vector2 initialDoorPos;
    
        // Start is called before the first frame update
    void Start()
    {
        initialDoorPos = doorFrame.position;// (Vector2)transform.position + Vector2.up * 0.4f;
        _startHeight = transform.position.y;
    }

    private IEnumerator FloatAnimation()
    {
        isRunning = true;
        // Sine-like floating animation
        while (run)
        {
            var nextPos = transform.position;
            nextPos.y = _startHeight +
                        (float)Math.Sin(Time.fixedTime * Math.PI * floatFrequency) * floatAmplitude * .3f * (invertDirection ? -1 : 1);
            transform.position = nextPos;
            doorFrame.position = initialDoorPos;

            

            yield return null;
        }
        isRunning = false;
    }

    private void Update()
    {
        if(run && ! isRunning)
        {
            StartCoroutine(FloatAnimation());
        }
        else if (!run && isRunning)
        {
            StopCoroutine(FloatAnimation());
            isRunning = false;
        }
    }
}
