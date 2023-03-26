using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class switchControl : MonoBehaviour
{
    public Image blanket;

    public Image picture;
    public Image character;

    public Sprite picture1;
    public Sprite picture2;
    public Sprite character1;
    public Sprite character2;


    bool isEnter = false;
    bool isNext = false;
    bool isBack = false;

    private int switchNum = 1;
    private const int switchLimit = 2;
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
            sceneSwitch();
            keyboard();
            if (isNext)
            {
                loadingNext();
            }
            else if (isBack)
            {
                loadingBack();
            }
        }
    }
    void keyboard()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("middle");
            playGame();
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
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            backButton();
        }
    }
    void loadingNext()
    {
        fadeInAndOut.blanketOut(blanket, 2f);
        if (blanket.color[3] >= 1)
        {
            GameData.Instance.switchNum = switchNum;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void loadingBack()
    {
        fadeInAndOut.blanketOut(blanket, 2f);
        if (blanket.color[3] >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    void sceneSwitch()
    {
        if(switchNum == 1)
        {
            picture.sprite = picture1;
            character.sprite = character1;
        }
        if(switchNum == 2)
        {
            picture.sprite = picture2;
            character.sprite = character2;
        }
    }

    public void playGame()
    {
        isNext = true;
    }
    public void backButton()
    {
        isBack = true;
    }
    public void leftButton()
    {
        if(switchNum == 1)
        {
            switchNum = switchLimit;
        }
        else
        {
            switchNum --;
        }
    }
    public void rightButton()
    {
        if (switchNum == switchLimit)
        {
            switchNum = 1;
        }
        else
        {
            switchNum++;
        }
    }
}
