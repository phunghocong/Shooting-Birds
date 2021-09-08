using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : Singleton<BackGroundController>
{
    //m?ng l?u các ?nh 
    public Sprite[] sprites;

    public  Image image;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        ChangeSprite();
    }
    public void ChangeSprite()
    {
        if(image != null && sprites != null && sprites.Length > 0)
        {
            int randomIdx = Random.Range(0, sprites.Length);

            if(sprites[randomIdx] != null)
            {
                image.sprite = sprites[randomIdx];
            }
        }
    }


}
