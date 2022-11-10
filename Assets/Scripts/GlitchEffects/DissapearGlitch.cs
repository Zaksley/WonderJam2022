using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DissapearGlitch : MonoBehaviour
{
    [SerializeField] Transform targetObject;

    [SerializeField] [Range(0f, 2f)] float amplitude = 1;

    public bool triggerOnce = false;
    public bool triggered = false;

    public UnityEvent onDissapear;

    // Start is called before the first frame update
    void Start()
    {
        if (targetObject == null) targetObject = this.transform;
    }

    private void OnEnable()
    {
        if (targetObject == null) targetObject = this.transform;
    }

    [ContextMenu("TriggerShake")]
    public void ManualTriggerAway(bool state = true)
    {
        StartCoroutine(GlitchAway(state));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(triggered && triggerOnce)) StartCoroutine(GlitchAway(true));
    }

    void DisableTargetColliders()
    {
        Collider2D[] colls = targetObject.GetComponentsInChildren<Collider2D>();
        if(colls.Length>0)
        {
            foreach(Collider2D c in colls)
            {
                c.enabled = false;
            }
        }
    }

    IEnumerator GlitchAway(bool dissapearAtEnd)
    {
        if (dissapearAtEnd) DisableTargetColliders();

        Vector3 initialPos = targetObject.localPosition;

        int dir = 1;
        float delay = .1f;
        float offset = .05f;

        for (int i = 0; i < 15; i++)
        {
            targetObject.localPosition = initialPos + (Vector3.right * offset * dir * amplitude);
            
            yield return new WaitForSeconds(delay);

            dir *= -1;
            delay *= .75f;
            offset += .05f;
        }

        if(dissapearAtEnd)
        {
            targetObject.gameObject.SetActive(false);
            gameObject.gameObject.SetActive(false);
            onDissapear.Invoke();
        }
        
        targetObject.localPosition = initialPos;
        triggered = true;

        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
