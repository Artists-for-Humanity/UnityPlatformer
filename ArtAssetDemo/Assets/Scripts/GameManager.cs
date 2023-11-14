using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //Reference to the scoreNumber object
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI curStateText;
    public GameObject pauseMenu;
    public GameObject resumeButton;
    public GameObject nextLevelButton;
    private GameObject player;

    //VARIABLES//
    public int score;
    public int scoreToReach;
    public bool paused = false; 
    public bool gameOver = false;
    public int time = 30;    
    private int sceneIndex; 
    // Start is called before the first frame update
    void Start()
    {
        score = 0; 
        time = 30;
        scoreText.text = "0";
        timerText.text = time.ToString();
        curStateText.text = "";
        paused = false;
        gameOver = false;
        player = GameObject.FindWithTag("Player");
        player.SetActive(true);
        pauseMenu.SetActive(false);
        nextLevelButton.SetActive(false);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(CountdownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        timerText.text = time.ToString();

        if(paused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            PauseGame();
        }

        if(score >= scoreToReach)
        {
            Debug.Log("GOT ALL COINS");
            YouWin();
        }

       // else if(paused == true && Input.GetKeyDown(KeyCode.Escape))
       // {
       //    paused = false;
       ////     UnPauseGame();
       // }
    }

    IEnumerator CountdownTimer()
    {
        for(int i = time; i > 0; i--)
        {
            if(gameOver == false)
            {
                yield return new WaitForSeconds(1f);
                time--;
            }
        }
        if(gameOver == true & time <= 0)
        {
                    curStateText.text = "TIME'S UP!";
                    GameOver();
        }


    }

    public void YouWin()
    {
        if(sceneIndex < 2)
        {
            gameOver = true;
            pauseMenu.SetActive(true);
            resumeButton.SetActive(false);
            player.SetActive(false);
            nextLevelButton.SetActive(true);
            curStateText.text = "YOU WIN!";
        }
        else if(sceneIndex >=2)
        {
            gameOver = true;
            player.SetActive(false);
            curStateText.text = "CONGRATULATIONS! \nYOU BEAT THE GAME!";
            StartCoroutine(LoadCredits());
        }
        
    }

    IEnumerator LoadCredits()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(3);
    }

    public void GameOver()
    {
        gameOver = true;
        pauseMenu.SetActive(true);
        resumeButton.SetActive(false);
        player.SetActive(false);
    }
    public void PauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        curStateText.text = "PAUSED";
    }

    public void UnPauseGame()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        curStateText.text = "";
    }

    public void RestartGame()
    {
        paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }


}
