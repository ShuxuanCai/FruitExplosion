using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    private Rigidbody2D fruitsRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    private int scoreToAdd = 1;
    private int collisionWithBound = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        fruitsRb = GetComponent<Rigidbody2D>();
        fruitsRb.AddForce(Vector3.up * Random.Range(500.0f, 700.0f));
        fruitsRb.AddTorque(Random.Range(-10.0f, 10.0f));
        transform.position = new Vector2(Random.Range(-2.5f, 2.5f), -6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameManager.isGameOver)
        {

            if (collision.gameObject.CompareTag("Circle"))
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                gameManager.audioSource.PlayOneShot(gameManager.fruitAudioClip, 1.0f);
                collision.transform.position = new Vector3(1080.0f, 1920.0f, 0.0f);
                gameManager.UpdateScore(scoreToAdd);
                if (gameManager.GetScore() < 0)
                    gameManager.GameOver();
            }

            if (collision.gameObject.CompareTag("OutOfBound"))
            {
                gameManager.UpdateFruitChances();
            }

            if (gameManager.fruitChances <= 0)
            {
                gameManager.GameOver();
            }

        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
