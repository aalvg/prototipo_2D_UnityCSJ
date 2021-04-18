using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;
    public GameObject gameOver;
    public static GameController instance; //estaticas podem ser acessadas por outros scripts



    void Start()
    {
        instance = this; //estou atribuindo a minha variavel o meu proprio script
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    public void updateScoreText()
    {
        scoreText.text = totalScore.ToString(); //aqui eu converto o int para texto
    }

    public void showGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
    }

}
