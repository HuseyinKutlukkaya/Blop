using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkaPlanOlusturucu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Texture2D texture = new Texture2D(Camera.main.pixelWidth+50, Camera.main.pixelHeight+50);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        GetComponent<SpriteRenderer>().sprite = sprite;
        
        int KareKenar = texture.width / 25;
        
  
        for (int x = 0; x < texture.width; x++) //Goes through each pixel
        {
            for (int y = 0; y < texture.height; y++)
            {

                Color pixelColour;
                if ((x / KareKenar) % 2 == 0) //50/50 chance it will be black or white
                {
                    if ((y / KareKenar) % 2 == 0)
                    {
                        pixelColour = Color.green;

                    }
                    else
                        pixelColour = Color.yellow;
                }
                else
                {
                    if ((y / KareKenar) % 2 == 0)
                    {
                        pixelColour = Color.yellow;


                    }
                    else
                        pixelColour = Color.green;

                }
                texture.SetPixel(x, y, pixelColour);
            }

        }
        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
