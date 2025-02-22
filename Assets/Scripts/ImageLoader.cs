using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{

    public bool LoadImage(int questionIdx)
    {
        return LoadImage("Images/"+ (questionIdx+1));
    }
    public bool LoadImage(string imagename)
    {
        gameObject.SetActive(true);
        Debug.Log("Loading Sprite: " + imagename);
        Sprite img = Resources.Load<Sprite>(imagename);
        if (img)
        {
            GetComponent<Image>().sprite = img;
            return true;
        }
        Debug.LogError("Failed to load Sprite: "+imagename);
        return false;
    }
    public static Sprite LoadSprite(string spritename)
    {
        Debug.Log("Loading Sprite: " + spritename);
        Sprite img = Resources.Load<Sprite>(spritename);
        if (img)
        {
            return img;
        }
        Debug.LogError("Failed to load Sprite: " + spritename);
        return null;
    }
}
