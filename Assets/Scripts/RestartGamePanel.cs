using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGamePanel : MonoBehaviour
{
    private GameManager gameManager;
    private int times = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        if (times > 0 && gameManager.IsWatchRewardAd() == false)
        {
            gameManager.isGameOver = false;
            gameManager.gameOverText.gameObject.SetActive(false);
            gameManager.restartButton.gameObject.SetActive(false);
            gameManager.rewardsButton.gameObject.SetActive(false);
            gameManager.fruitChances = 3;
            gameManager.bombChances = 3;
            gameManager.score = 0;
            gameManager.scoreText.text = "Score: " + gameManager.score;
            gameManager.fruitChancesText.text = "Fruits: " + gameManager.fruitChances;
            gameManager.bombChancesText.text = "Bomb: " + gameManager.bombChances;
        }

        else if(times > 0 && gameManager.IsWatchRewardAd() == true)
        {
            gameManager.isGameOver = false;
            gameManager.gameOverText.gameObject.SetActive(false);
            gameManager.restartButton.gameObject.SetActive(false);
            gameManager.rewardsButton.gameObject.SetActive(false);
            gameManager.fruitChances = 5;
            gameManager.bombChances = 5;
            gameManager.score = 0;
            gameManager.scoreText.text = "Score: " + gameManager.score;
            gameManager.fruitChancesText.text = "Fruits: " + gameManager.fruitChances;
            gameManager.bombChancesText.text = "Bomb: " + gameManager.bombChances;
            AdsManager.Instance.isWatchRewardAds = false;
        }

        times++;
    }
}
