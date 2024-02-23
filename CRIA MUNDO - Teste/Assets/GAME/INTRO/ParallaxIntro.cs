using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxIntro : MonoBehaviour
{
    public SpriteRenderer parallax;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        parallax.material.mainTextureOffset = new Vector2(parallax.material.mainTextureOffset.x + speed * Time.deltaTime, 0);
    }
}
