using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{

    [SerializeField] List<Transform> layerSprites;
    [SerializeField] List<float> distancesX;
    [SerializeField] List<float> distancesY;
    [Space]
    [SerializeField] Transform objectReference;
    [SerializeField] Vector2 baseBack;
    [SerializeField] Vector2 baseObject;
    [SerializeField] bool moveBack = true;
    Vector2 prevObject;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < layerSprites.Count; i++)
        {
            Transform t = layerSprites[i];
            t.position = new Vector3(baseBack.x, baseBack.y, i);
        }
    }

    [ContextMenu("Update OffsetX")]
    public void UpdateBackgroundPosX()
    {
        float diffObject = objectReference.position.x - baseObject.x;
        for (int i = 0; i < layerSprites.Count; i++)
        {
            float offset = diffObject * distancesX[((i < distancesX.Count) ? i : distancesX.Count)];
            layerSprites[i].position = new Vector3(baseBack.x + offset, layerSprites[i].position.y, i);
        }
    }

    [ContextMenu("Update OffsetY")]
    public void UpdateBackgroundPosY()
    {
        float diffObject = objectReference.position.y - baseObject.y;
        for (int i = 0; i < layerSprites.Count; i++)
        {
            float offset = diffObject * distancesY[((i < distancesY.Count) ? i : distancesY.Count)];
            layerSprites[i].position = new Vector3(layerSprites[i].position.x, baseBack.y + offset,  i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(prevObject.x != objectReference.position.x && moveBack)
        {
            UpdateBackgroundPosX();
        }

        if (prevObject.y != objectReference.position.y && moveBack)
        {
            UpdateBackgroundPosY();
        }

        prevObject = (Vector2)objectReference.position;
    }
}
