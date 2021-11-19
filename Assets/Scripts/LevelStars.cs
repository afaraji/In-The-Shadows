using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStars : MonoBehaviour
{
    public Sprite[] stars;
    public Image image;

    public void SetStars(int starsAmount)
    {
        if (starsAmount > 3 || starsAmount < 0)
            starsAmount = 0;
        image.sprite = stars[starsAmount];
    }
    
}
