using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class AlienFleet : MonoBehaviour
{
    [Header("Inscribed")]

    public GameObject alienPrefab;

    public AlienCounter alienCounter;

    public List<GameObject> aliens = new List<GameObject> { };
    private int initialAlienCount;

    float speed = .1f;
    float leftBound = -16;
    float rightBound = 16;
    float lowerBound = 4.8f;
    float downTime = .1f;
    bool movingDown = false;
    float gameOverY = -5.5f;
    bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject alien = Instantiate<GameObject>(alienPrefab);
                alien.transform.position = new Vector3(2 * i - 12, 10 - 2 * j, 0);
                aliens.Add(alien);
                alien.GetComponent<Alien>().fleet = this;
            }
        }
        initialAlienCount = aliens.Count;
    }

    // Update is called once per frame
    void Update()
    {
        alienCounter.score = aliens.Count;
         // Check whether all aliens have been destroyed
        if (initialAlienCount > 0 && aliens.Count == 0 && !gameOver)
        {
            gameOver = true;

            // Tell GameManager the player won
            GameManager.Instance.EndGame(true);
        }
    }

    void FixedUpdate()
    {
        // Determine the leftmost and rightmost aliens
        float left = float.PositiveInfinity;
        float right = float.NegativeInfinity;

        // Adjust speed based on the number of destroyed aliens
        int destroyedAliens = initialAlienCount - aliens.Count;
        float newSpeed = 0.1f + (destroyedAliens * 0.005f);
        speed = Mathf.Sign(speed) * newSpeed;

        //move all aliens
        foreach (GameObject alien in aliens)
        {
                        Vector3 position = alien.transform.position;
            if (!movingDown)
            {
                position.x += speed;
            }
            else
            {
                position.y += -Mathf.Abs(speed);
                /*if (position.y <= lowerBound)
                {
                    movingDown = false;
                    lowerBound = position.y - 1;
                }*/
            }
            alien.transform.position = position;
            left = Mathf.Min(left, position.x);
            right = Mathf.Max(right, position.x);
            if (position.y <= gameOverY && !gameOver)
            {
                gameOver = true;

                // Tell GameManager that the player lost
                GameManager.Instance.EndGame(false);

                return;
            }
        }

        if (speed > 0 && right >= rightBound || speed < 0 && left <= leftBound)
        {
            speed = -speed;
            movingDown = true;
            Invoke("startSidewaysMotion", downTime);
        }
    }

    void startSidewaysMotion()
    {
        movingDown = false;
    }
}
