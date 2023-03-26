using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class menuControl : MonoBehaviour
{
    public RawImage image1;
    public RawImage image2;
    public RawImage image3;
    public RawImage image4;
    public RawImage image5;
    public RawImage button1;
    public RawImage button2;
    public RawImage button3;
    public RawImage button4;
    int buttonBehaviour=1;
    public Texture exitImg;
    public Texture storyModeImg;
    public Image blanket;
    bool isNext = false;
    bool isEnter = false;
    bool isPush = false;
    private const int switchLimit = 2;
    public GameObject left;
    public GameObject right;
    public GameObject middle;
    public GameObject pushstart;

    private bool twinkleHelper = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (!isEnter)
        {
            fadeInAndOut.blanketIn(blanket, 2f);
            if (blanket.color[3] <= 0)
            {
                isEnter = true;
            }
        }
        else
        {
            if (!isPush)
                twinkle();
            anim();
            buttonChange();
            keyboard();
            if (isNext)
                loadingNext();
        }
    }
    void keyboard()
    {
        if (isPush)
     
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("middle");
                middleButton();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("left");
                leftButton();
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("right");
                rightButton();
            }

        }
        else
        {
            
                if (Input.anyKeyDown)
                {
                    pushStart();
                    Input.GetKeyDown(KeyCode.X);
                }
          
        }

    }
    void twinkle()
    {
        if (!twinkleHelper)
        {
            fadeInAndOut.fadeOut(button4, 1f);
            if (button4.color[3] <= 0)
                twinkleHelper = true;
        }
        else
        {
            fadeInAndOut.fadeIn(button4, 1f);
            if (button4.color[3] >= 1)
                twinkleHelper = false;
        }
       
    }
    void anim()
    {
        if (image1.color[3] <= 1)
            fadeInAndOut.fadeIn(image1, 1f);
        if (image1.color[3] >= 1 && image2.color[3] <= 1)
            fadeInAndOut.fadeIn(image2, 1f);
        if (image2.color[3] >= 1 && image3.color[3] <= 1)
            fadeInAndOut.fadeIn(image3, 1f);
        if (image3.color[3] >= 1 && image4.color[3] <= 1)
        {
            fadeInAndOut.fadeIn(image4, 1f);
            fadeInAndOut.fadeIn(image5, 1f);
            fadeInAndOut.fadeIn(button1, 1f);
            fadeInAndOut.fadeIn(button2, 1f);
            fadeInAndOut.fadeIn(button3, 1f);
            fadeInAndOut.fadeIn(button4, 1f);
        }
    }
    void buttonChange()
    {
        if(buttonBehaviour == 1)
        {
            button1.texture = storyModeImg;
        }
        else if(buttonBehaviour == 2)
        {
            button1.texture = exitImg; 
        }
    }
    public void leftButton()
    {
        if (buttonBehaviour != 1)
        {
            buttonBehaviour -= 1;
        }
        else
        {
            buttonBehaviour = switchLimit;
        }
    }
    public void rightButton()
    {
        if(buttonBehaviour != switchLimit)
        {
            buttonBehaviour += 1;
        }
        else
        {
            buttonBehaviour = 1;
        }
    }
    public void middleButton()
    {
        if(buttonBehaviour == 1)
        {
            storyModeButton();
        }
        else if(buttonBehaviour == 2)
        {
            exitButton();
        }
    }
    public void pushStart()
    {
        pushstart.SetActive(false);
        left.SetActive(true);
        right.SetActive(true);
        middle.SetActive(true);
        isPush = true;
    }
    void exitButton()
    {
        Application.Quit();
    }
    void storyModeButton()
    {

        isNext = true;
    }
    void loadingNext()
    {
        fadeInAndOut.blanketOut(blanket, 2f);
        if (blanket.color[3] >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
