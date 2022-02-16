using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public static Highscore hScore; 
    public Text higestScoreText;
    public int highScore;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetHighestScore()
    {
        if (GameManager.gmInstance.score > highScore)
        {
            highScore = GameManager.gmInstance.score;
            higestScoreText.text = highScore.ToString();
        }
    }
}
