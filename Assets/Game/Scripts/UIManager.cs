using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public int score;
    public Text scoreText;
    public Text highScoreText;
    public GameObject _playerPrefab;
    public Image startGameImage;
    public bool hasGameStarted = false;

    private ScreenManager _sM;
    [SerializeField]
    private GameObject galaxyBG;

    public GameObject mainMenu;
    public Joystick joystick;


    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];

    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;

        if(score > PlayerPrefs.GetInt("HighScore", score))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score: " + score.ToString();
        }

    }

    public void Awake()
    {
        _sM = GameObject.Find("ScreenManager").GetComponent<ScreenManager>();
        galaxyBG.transform.localScale = new Vector2(_sM.getScreenWidth(), _sM.getScreenHeight());
    }

    public void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }


    public void Update()
    {

        if (hasGameStarted == false)
        {
            showTitleScreen();
            scoreText.gameObject.SetActive(false);
            highScoreText.gameObject.SetActive(false);
            livesImageDisplay.gameObject.SetActive(false);
        }
        else if (hasGameStarted == true)
        {
            startGameImage.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
            livesImageDisplay.gameObject.SetActive(true);
        }


    }



    public void showTitleScreen()
    {
        startGameImage.gameObject.SetActive(true);
        mainMenu.SetActive(true);

    }

    public void hideTitleScreen()
    {
        startGameImage.gameObject.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
    }


    


}
