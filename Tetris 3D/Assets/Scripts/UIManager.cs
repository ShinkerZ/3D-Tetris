using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uimnInstance;
    public Text scoreText, levelText, layerText;

    public GameObject gameOverWindow;

    private void Awake()
    {
        uimnInstance = this;
    }
    private void Start()
    {
        gameOverWindow.SetActive(false);
    }
    public void UpdateUI(int score, int level, int layers)
    {
        scoreText.text = "SCORE : " + scoreText.ToString();
        levelText.text = "LEVEL : " + levelText.ToString();
        layerText.text = "ROWS CLEARED : " + layerText.ToString();
    }
    public void ActivateGameOverwindow()
    {
        gameOverWindow.SetActive(true);
    }
}
