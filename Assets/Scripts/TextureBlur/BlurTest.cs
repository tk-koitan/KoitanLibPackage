using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurTest : MonoBehaviour
{
    [SerializeField]
    float radius;
    // Start is called before the first frame update
    void Start()
    {
        var spr = GetComponent<SpriteRenderer>();
        var tex = BuildTexture.CreateBlurTexture(spr.sprite.texture, radius);
        spr.sprite = Sprite.Create(tex, spr.sprite.rect, new Vector2(0.5f, 0.5f), 100.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
