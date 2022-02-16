using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu mainMenuInstance;
    public GameObject howToPlayPanel;

    

    void Start()
    {
        
    }
    /*public GameObject[] BlockList;
    public float blastX, blastY, blastZ;
    public float vecX, vecY, vecZ;
    float force;
    public Rigidbody rb;*/

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void HowToPlayPanel()
    {
        howToPlayPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        howToPlayPanel.SetActive(false);
    }
}
