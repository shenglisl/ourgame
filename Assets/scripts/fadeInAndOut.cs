using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeInAndOut : MonoBehaviour
{


    public static void fadeIn(RawImage picture, float time)
    {
        float a = picture.color[3];
            if (a < 1)
            {
                float b = 1 / (60 * time);
                a = a + b;
                picture.color = new Color(1, 1, 1, a);
            }
        
    }
    public static void fadeIn(Image picture, float time)
    {
        float a = picture.color[3];
        if (a < 1)
        {
            float b = 1 / (60 * time);
            a = a + b;
            picture.color = new Color(1, 1, 1, a);
        }
    }
    public static void fadeOut(Image picture, float time)
    {
        float a = picture.color[3];
        if (a > 0)
        {
            float b = 1 / (60 * time);
            a = a - b;
            picture.color = new Color(1, 1, 1, a);
        }
    }
    public static void fadeOut(RawImage picture, float time)
    {
        float a = picture.color[3];
        if (a > 0)
        {
            float b = 1 / (60 * time);
            a = a - b;
            picture.color = new Color(1, 1, 1, a);
        }
    }

    public static void blackIn(SpriteRenderer picture, float time)
    {
        float a = picture.color[0];
        if (a < 1)
        {
            float b = 1 / (60 * time);
            a = a + b;
            picture.color = new Color(a, a, a, 1);
        }

    }
    public static void blackOut(SpriteRenderer picture, float time)
    {
        float a = picture.color[0];
        if (a > 0)
        {
            float b = 1 / (60 * time);
            a = a - b;
            picture.color = new Color(a, a, a, 1);
        }

    }
    public static void blanketOut(Image picture, float time)
    {
        float a = picture.color[3];
        if (a < 1)
        {
            float b = 1 / (60 * time);
            a = a + b;
            picture.color = new Color(0, 0, 0, a);
        }
    }
    public static void blanketIn(Image picture, float time)
    {
        float a = picture.color[3];
        if (a > 0)
        {
            float b = 1 / (60 * time);
            a = a - b;
            picture.color = new Color(0, 0, 0, a);
        }
    }
}
