using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;
    public int score, level, layersRemoved;
    float fallSpeed;

    bool gameIsOver;

    void Awake()
    {
        gmInstance = this;
    }
    void Start()
    {
        SetScore(score);
    }
    public void SetScore(int amount) // amount is score
    {
        score += amount;
        CalculateLevel();
        UIManager.uimnInstance.UpdateUI(score , level, layersRemoved);
    }  
    public float ReadFallSpeed()
    {
        return fallSpeed;
    }

    public void LayersCleared(int amount) // amount is the rows to cleared amount -- then set a rewarding score system in case 2 or more layers are cleared
    {
        switch(amount)
        {
            case 1:
                SetScore(400);
                break;
            case 2:
                SetScore(800);
                break;
            case 3:
                SetScore(1600);
                break;
            case 4:
                SetScore(3200);
                break;
        }

        layersRemoved += amount;
        UIManager.uimnInstance.UpdateUI(score, level, layersRemoved);
    }
    void CalculateLevel()  // Level is dependant on SCORE and will increase the fall speed
    {
        if(score <= 1000)
        {
            level = 1;
            fallSpeed = 3f;
        }
        else if (score > 1000 && score<2000)
        {
            level = 2;
            fallSpeed = 2.75f;
        }
        else if (score > 2000 && score < 3000)
        {
            level = 3;
            fallSpeed = 2.5f;
        }
        else if (score > 3000 && score < 4000)
        {
            level = 4;
            fallSpeed = 2.25f;
        }
        else if (score > 4000 && score < 5000)
        {
            level = 5;
            fallSpeed = 2f;
        }
        else if (score > 5000 && score < 6000)
        {
            level = 6;
            fallSpeed = 1.75f;
        }
        else if (score > 6000 && score < 7000)
        {
            level = 7;
            fallSpeed = 1.5f;
        }
        else if (score > 7000 && score < 8000)
        {
            level = 8;
            fallSpeed = 1.25f;
        }
        else if (score > 8000 && score < 9000)
        {
            level = 9;
            fallSpeed = 1f;
        }
        else if (score > 9000)
        {
            level = 10;
            fallSpeed = 0.8f;
        }
    }
    public bool ReadGameOver()
    {
        return gameIsOver;
    }
    public void SetGameIsOver()
    {
        gameIsOver = true;
        Highscore.hScore.SetHighestScore();
        UIManager.uimnInstance.ActivateGameOverwindow();
    }
    

}
