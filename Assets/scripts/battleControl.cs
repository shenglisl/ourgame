using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class battleControl : MonoBehaviour
{
    public GameObject pauseMenu;
    public Image blanket;
    bool isExit = false;
    bool isEnter = false;
    public Image enemyMask;
    public Image playerMask;
    private float originalEnemySize;
    private float updateEnemySize;
    private float originalPlayerSize;
    private float updatePlayerSize;
    bool enemyIsDead = false;
    bool playerIsDead = false;
    bool playerIsExhausted = false;
    bool isPause = false;
    public Sprite nullStar;
    public Sprite fullStar;
    private int updatePlayerBlood = 5;
    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    Image[] mystar = new Image[5];
    


    
    void Start()
    {
        originalEnemySize = enemyMask.rectTransform.rect.width;
        updateEnemySize = originalEnemySize;
        originalPlayerSize = playerMask.rectTransform.rect.height;
        updatePlayerSize = originalPlayerSize;

        mystar[0] = star1;
        mystar[1] = star2;
        mystar[2] = star3;
        mystar[3] = star4;
        mystar[4] = star5;
    }
    void Update()
    {
        if (!isEnter)
        {
            Time.timeScale = 1f;
            fadeInAndOut.blanketIn(blanket, 2f);
            if (blanket.color[3] <= 0)
            {
                isEnter = true;
            }
        }
        else
        {
            enemyBlood();
            playerBlood();
            playerEnergy();
            keyboard();
            //playerEnergyDebug();
            if (isExit)
            {
                fadeInAndOut.blanketOut(blanket, 2f);
                if (blanket.color[3] >= 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }
        }
    }
    void keyboard()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
                pauseOn();
            else
                pauseOff();
        }
    }
    void playerEnergyDebug()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerEnergyChange(0.2f);


        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            playerEnergyChange(-0.2f);
        }
    }
    void playerBlood()
    {

        if (updatePlayerBlood < 0)
        {
            updatePlayerBlood = 0;
            playerIsDead = true;
        }
        if (updatePlayerBlood > 5)
            updatePlayerBlood = 5;
        for(int i=0; i <5-updatePlayerBlood; i++)
        {
            mystar[i].sprite = nullStar;
        }
        for (int i = 5-updatePlayerBlood; i < 5; i++)
        {
            mystar[i].sprite = fullStar;
        }
    }
    public int playerBloodCheck()
    {
        return updatePlayerBlood;
    }
    public void playerBloodSet(int blood)
    {
        updatePlayerBlood = blood;
    }

    public void  playerBloodChange(int deltablood)
    {
        updatePlayerBlood += deltablood;
    }
    void enemyBlood()
    {
        if (updateEnemySize > originalEnemySize)
            updateEnemySize = originalEnemySize;
        if (updateEnemySize < 0)
        {
            updateEnemySize = 0;
            enemyIsDead = true;
        }
        enemyMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, updateEnemySize);
    }

    public void enemyBloodSet(float percent)
    {
        if (percent > 1f)
            percent = 1f;
        if (percent < 0f)
            percent = 0f;
        updateEnemySize = percent * originalEnemySize;
    }
    public void enemyBloodChange(float changePercent)
    {
        updateEnemySize += originalEnemySize*changePercent;
    }
    public bool checkEnemy()
    {
        return enemyIsDead;
    }
    void playerEnergy()
    {
        if (updatePlayerSize > originalPlayerSize)
        {
            updatePlayerSize = originalPlayerSize;
        }
        if (updatePlayerSize < 0)
        {
            updatePlayerSize = 0;
            playerIsExhausted = true;
        }
        else
        {
            playerIsExhausted = false;
        }
        playerMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, updatePlayerSize);
    }
    public void playerEnergySet(float percent)
    {
        if (percent > 1f)
            percent = 1f;
        if (percent < 0f)
            percent = 0f;
       updatePlayerSize = percent * originalPlayerSize;
    }
    public bool playerExhaustedCheck()
    {
        return playerIsExhausted;
    }
    public void playerEnergyChange(float changePercent)
    {
        updatePlayerSize += originalPlayerSize * changePercent;
    }
    public void pauseOn()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void pauseOff()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    public void exit()
    {
        isExit = true;
    }
}
