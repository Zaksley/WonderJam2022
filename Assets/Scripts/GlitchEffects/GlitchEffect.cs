using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    [Range(0.01f, 5f)] public float intensity = 1;
    public bool doGlitch = false;
    public bool doColor = false;
    bool glitchRunning = false;
    [Space]
    [SerializeField] List<Options> options_;
    [Space]
    SpriteRenderer render;
    Collider2D coll;
    [SerializeField] List<Sprite> glitchedTextures;
    Color redGlitch;
    Color blueGlitch;

    enum Options
    {
        Flicker,
        Shake
    }

    // Start is called before the first frame update
    void Start()
    {
        redGlitch = new Color32(252, 54, 53, 255);
        blueGlitch = new Color32(21, 205, 233, 255);
        render = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    IEnumerator Glitch()
    {
        Debug.Log("GlitchStart");
        glitchRunning = true;
        while(doGlitch)
        {
            Sprite texture = render.sprite;
            DoEffect(options_[Random.Range(0,options_.Count)]);
            yield return new WaitForSeconds(Random.Range(.5f, 1+ 5f/intensity));
        }
        glitchRunning = false;
        Debug.Log("GlitchEnd");
        yield break;
    }

    void DoEffect(Options type)
    {
        int mAmount = Mathf.CeilToInt(intensity);
        switch (type)
        {
            case Options.Flicker:
                StartCoroutine(Flicker(mAmount));
                break;

            case Options.Shake:
                StartCoroutine(Shake(mAmount));
                break;
        }
    }


    void ColorSwitch(bool reset = false)
    {
        if(doColor)
        {
            Color color_;
            if (reset)
            {
                color_ = Color.white;
            }
            else
            {
                color_ = RandomBool() ? Color.white : (RandomBool() ? redGlitch : blueGlitch);
            }
            render.color = color_;
        }
    }

    bool RandomBool()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0) return true;
        else return false;
    }

    IEnumerator Flicker(int maxAmount)
    {
        //Debug.Log("GlitchFlicker");
        int amount = Random.Range(1, maxAmount + 1);
        Sprite texture = render.sprite;
        for(int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, glitchedTextures.Count);
            TextureSwitch(glitchedTextures[index]);
            render.flipX = RandomBool();
            render.flipY = RandomBool();
            ColorSwitch();
            yield return new WaitForSeconds(.1f);
            TextureSwitch(texture);
            render.flipX = false;
            render.flipY = false;
            ColorSwitch(true);
            yield return new WaitForSeconds(.1f);
        }
        TextureSwitch(texture);

        yield break;
    }

    void TextureSwitch(Sprite texture_)
    {
        render.sprite = texture_;
    }

    IEnumerator Shake(int maxAmount)
    {
        //Debug.Log("GlitchShake");
        int amount = Random.Range(1, maxAmount + 2);

        Vector2 collPos = (coll)? coll.offset : Vector2.zero;

        Vector3 initialPos = transform.position;

        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, glitchedTextures.Count);
            OffsetSet(Random.Range(.5f,(intensity+1)/10));
            ColorSwitch();
            yield return new WaitForSeconds(.1f);
            transform.position = initialPos;
            if(coll) coll.offset = collPos;
            ColorSwitch(true);
            yield return new WaitForSeconds(.1f);
        }
        if(coll) coll.offset = collPos;
        transform.position = initialPos;

        yield break;
    }

    void OffsetSet(float val)
    {
        Vector2 collPos = (coll)? coll.offset : Vector2.zero;
        int xFactor = Random.Range(0, 3) - 1;
        int yFactor = Random.Range(0, 3) - 1;
        if(coll) coll.offset = collPos - (new Vector2(xFactor * val / 2, yFactor * val / 2));
        transform.position = new Vector3(transform.position.x + xFactor * val /2, transform.position.y + yFactor * val/2, transform.position.z);
    }



    // Update is called once per frame
    void Update()
    {
     if(doGlitch && !glitchRunning)
        {
            StartCoroutine(Glitch());
        }
    }
}
