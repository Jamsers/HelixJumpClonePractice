using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScoreAndHealth : MonoBehaviour
{
    static public PlayerScoreAndHealth Instance;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] GameObject gameOverScreen;

    int playerScore = 0;

    bool isPlayerAlive = true;
    public bool isAlive {
        get => isPlayerAlive;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        highScoreText.text = PlayerPrefs.GetInt("High Score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame() {
        SceneManager.LoadScene(0);
    }

    public void Died() {
        isPlayerAlive = false;
        gameOverScreen.SetActive(true);
        if (PlayerPrefs.GetInt("High Score", 0) < playerScore) {
            PlayerPrefs.SetInt("High Score", playerScore);
        }
    }

    public void AddScore() {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
    }
}
