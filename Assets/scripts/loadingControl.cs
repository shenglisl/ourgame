using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingControl : MonoBehaviour
{
    public GameObject star;
    public SpriteRenderer starstar;
    public SpriteRenderer background;
    bool isEnter = false;
    static float time=6f;
    static float a = 0;
    static float b = 1 / (60 * time);
    void Update()
    {
        if (!isEnter)
        {
            fadeInAndOut.blackIn(starstar, 1);
            fadeInAndOut.blackIn(background, 1);
            if(starstar.color[1] >= 1)
            {
                isEnter = true;
            }
        }
        else
        {
            if (a < 1)
            {
                float zAngle = 0.3f;
                star.transform.Rotate(0, 0, zAngle, Space.Self);
                a = a + b;

            }
            else
            {
                fadeInAndOut.blackOut(starstar, 1);
                fadeInAndOut.blackOut(background, 1);
                if (starstar.color[1] <= 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

}
