using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> fruits;
    public Text scoreText;
    public Text highestScoreText;
    public List<TextMeshProUGUI> highScores;
    public Text gameOverText;
    public Text fruitChancesText;
    public Text bombChancesText;
    public GameObject circle;
    public Button restartButton;
    public Button rewardsButton;
    public Button startButton;

    public int score = 0;
    public int fruitChances = 3;
    public int bombChances = 3;

    private float t1 = 2.0f;
    private float t2;
    private float t3 = 0.5f;
    private float t4;
    private float t5 = 1.5f;
    private float t6;
    private float t7 = 1.0f;
    private float t8;
    private float t9 = 0.5f;
    private float t10;
    private float spawnRate;

    public bool isGameOver;

    public AudioSource audioSource;
    public AudioClip bombAudioClip;
    public AudioClip fruitAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        scoreText.text = "Score: " + score;
        highestScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore", 0).ToString();
        fruitChancesText.text = "Fruits: " + fruitChances;
        bombChancesText.text = "Bomb: " + bombChances;
        isGameOver = false;

        AdsManager.Instance.PlayBannerAd();
    }

    public void StartScoreShow()
    {
        highScores[0].text = "1. " + PlayerPrefs.GetInt("HighestScore1", 0).ToString();
        highScores[1].text = "2. " + PlayerPrefs.GetInt("HighestScore2", 0).ToString();
        highScores[2].text = "3. " + PlayerPrefs.GetInt("HighestScore3", 0).ToString();
        highScores[3].text = "4. " + PlayerPrefs.GetInt("HighestScore4", 0).ToString();
        highScores[4].text = "5. " + PlayerPrefs.GetInt("HighestScore5", 0).ToString();
    }

    IEnumerator SpawnFruits()
    {
        if (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, fruits.Count);
            Instantiate(fruits[index]);
        }
    }

    IEnumerator RestartButtonAppear()
    {
        yield return new WaitForSeconds(5.0f);
        restartButton.gameObject.SetActive(true);
        rewardsButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(score <= 10)
        {
            if (Time.time - t2 > 0)
            {
                t2 = Time.time + t1;
                StartCoroutine(SpawnFruits());
            }
        }

        else if(score > 10 && score <= 20)
        {
            if (Time.time - t6 > 0)
            {
                t6 = Time.time + t5;
                StartCoroutine(SpawnFruits());
            }
        }

        else if (score > 20 && score <= 30)
        {
            if (Time.time - t8 > 0)
            {
                t8 = Time.time + t7;
                StartCoroutine(SpawnFruits());
            }
        }

        else
        {
            if (Time.time - t10 > 0)
            {
                t10 = Time.time + t9;
                StartCoroutine(SpawnFruits());
            }
        }

        if (Time.time - t4 > 0)
        {
            t4 = Time.time + t3;
            circle.transform.position = new Vector3(1080.0f, 1920.0f, 0.0f);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateFruitChances()
    {
        fruitChances--;
        fruitChancesText.text = "Fruits: " + fruitChances;
    }

    public void UpdateBombChances()
    {
        bombChances--;
        bombChancesText.text = "Bomb: " + bombChances;
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(RestartButtonAppear());
        SaveHighestScore();
        UpdateScoreCheck();
        AdsManager.Instance.PlayInterstitial();
    }

    public void SaveHighestScore()
    {
        if (score > PlayerPrefs.GetInt("HighestScore", 0))
        {
            PlayerPrefs.SetInt("HighestScore", score);
            highestScoreText.text = "Highest Score: " + score.ToString();
        }
    }

    public void UpdateScoreCheck()
    {
        if (score >= PlayerPrefs.GetInt("HighestScore1", 0))
        {
            int score1 = PlayerPrefs.GetInt("HighestScore1");
            int score2 = PlayerPrefs.GetInt("HighestScore2");
            int score3 = PlayerPrefs.GetInt("HighestScore3");
            int score4 = PlayerPrefs.GetInt("HighestScore4");
            PlayerPrefs.SetInt("HighestScore2", score1);
            PlayerPrefs.SetInt("HighestScore3", score2);
            PlayerPrefs.SetInt("HighestScore4", score3);
            PlayerPrefs.SetInt("HighestScore5", score4);
            highScores[1].text = "2. " + score1.ToString();
            highScores[2].text = "3. " + score2.ToString();
            highScores[3].text = "4. " + score3.ToString();
            highScores[4].text = "5. " + score4.ToString();
            PlayerPrefs.SetInt("HighestScore1", score);
            highScores[0].text = "1. " + score.ToString();
        }

        else if(score >= PlayerPrefs.GetInt("HighestScore2", 0) && score < PlayerPrefs.GetInt("HighestScore1", 0))
        {
            int score2 = PlayerPrefs.GetInt("HighestScore2");
            int score3 = PlayerPrefs.GetInt("HighestScore3");
            int score4 = PlayerPrefs.GetInt("HighestScore4");
            PlayerPrefs.SetInt("HighestScore3", score2);
            PlayerPrefs.SetInt("HighestScore4", score3);
            PlayerPrefs.SetInt("HighestScore5", score4);
            highScores[2].text = "3. " + score2.ToString();
            highScores[3].text = "4. " + score3.ToString();
            highScores[4].text = "5. " + score4.ToString();
            PlayerPrefs.SetInt("HighestScore2", score);
            highScores[1].text = "2. " + score.ToString();
        }

        else if (score >= PlayerPrefs.GetInt("HighestScore3", 0) && score < PlayerPrefs.GetInt("HighestScore2", 0))
        {
            int score3 = PlayerPrefs.GetInt("HighestScore3");
            int score4 = PlayerPrefs.GetInt("HighestScore4");
            PlayerPrefs.SetInt("HighestScore4", score3);
            PlayerPrefs.SetInt("HighestScore5", score4);
            highScores[3].text = "4. " + score3.ToString();
            highScores[4].text = "5. " + score4.ToString();
            PlayerPrefs.SetInt("HighestScore3", score);
            highScores[2].text = "3. " + score.ToString();
        }

        else if (score >= PlayerPrefs.GetInt("HighestScore4", 0) && score < PlayerPrefs.GetInt("HighestScore3", 0))
        {
            int score4 = PlayerPrefs.GetInt("HighestScore4");
            PlayerPrefs.SetInt("HighestScore5", score4);
            highScores[4].text = "5. " + score4.ToString();
            PlayerPrefs.SetInt("HighestScore4", score);
            highScores[3].text = "4. " + score.ToString();
        }

        else if (score >= PlayerPrefs.GetInt("HighestScore5", 0) && score < PlayerPrefs.GetInt("HighestScore4", 0))
        {
            PlayerPrefs.SetInt("HighestScore5", score);
            highScores[4].text = "5. " + score.ToString();
        }
    }

    public void PauseButton()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }

        startButton.gameObject.SetActive(true);
    }

    public void StartButton()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        startButton.gameObject.SetActive(false);
    }

    public void WatchRewardAd()
    {
        AdsManager.Instance.PlayRewardAd();
    }

    public bool IsWatchRewardAd()
    {
        if(AdsManager.Instance.IsPlayRewardAd() == true)
        {
            return true;
        }
        return false;
    }
}
