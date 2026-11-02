using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Spaceship : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject dartPrefab;

    public static float shotCooldown = .05f;
    public float lastShot = 0;
    public float speed = 10f;
    public int lives = 3;
    public TextMeshProUGUI livesText;
    private float lastHitTime = -100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       UpdateLivesDisplay(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        // Move right
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            position.x += speed * Time.deltaTime;
        }

        // Move left
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            position.x -= speed * Time.deltaTime;
        }

        // Calculate the camera's horizontal boundaries
        float halfScreenWidth = Camera.main.orthographicSize
                                * Camera.main.aspect;

        float shipMargin = 1.5f;

        // Prevent the spaceship from moving offscreen
        position.x = Mathf.Clamp(
            position.x,
            -halfScreenWidth + shipMargin,
            halfScreenWidth - shipMargin
        );

        // Apply the updated position
        transform.position = position;

        // Shoot
        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            Time.time - lastShot > shotCooldown)
        {
            GameObject bullet = Instantiate<GameObject>(dartPrefab);
            bullet.transform.position = position;
            lastShot = Time.time;
        }
    }
    
    public void TakeDamage()
    {
        // Prevent multiple hits within one second
        if (lives <= 0 || Time.time - lastHitTime < 1f)
        {
            return;
        }

        lastHitTime = Time.time;
        lives--;
        UpdateLivesDisplay();

        Debug.Log("Lives remaining: " + lives);

        if (lives <= 0)
        {
            GameManager.Instance.EndGame(false);
        }
    }
    
    void UpdateLivesDisplay()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }



}


