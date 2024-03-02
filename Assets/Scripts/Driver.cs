using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 300;
    [SerializeField] float moveSpeed = 20;
    [SerializeField] float slowSpeed = 15;
    [SerializeField] float boostSpeed = 30;

    [SerializeField]
    Color32 speedUpColor = new Color32(1, 1, 1, 1);

    [SerializeField]
    Color32 speedNormalColor = new Color32(1, 1, 1, 1);

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private AudioClip hornSFX;


    float timeRemaining = 100;
    float timeNotify = 5;
    public bool timerIsRunning = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float movedAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, movedAmount, 0);
        Debug.Log(movedAmount);

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay <= timeNotify)
        {
            timeText.color = new Color32(221, 70, 66, 255);
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<AudioSource>().PlayOneShot(hornSFX);
        if (moveSpeed == boostSpeed)
        {
            moveSpeed = slowSpeed;
            spriteRenderer.color = speedNormalColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedUp")
        {
            moveSpeed = boostSpeed;
            Debug.Log("SPEED UP");
            spriteRenderer.color = speedUpColor;
        }
    }
}
