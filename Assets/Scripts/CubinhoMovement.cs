using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubinhoMovement : MonoBehaviour
{
    // Variables
    public Animator animator;
    [SerializeField] private float limMinX, limMaxX;
    public float slidingSpeed, turningSpeed;
    private float targetX;
    private bool isCollided;
    public bool isPlayPressed;
    private bool canSlide;

    // Start is called before the first frame update
    void Start()
    {
        isPlayPressed = false;
        canSlide = false;
        animator = GetComponentInChildren<Animator>();
        targetX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSlide)
            return;

        if (isCollided)
        {
            if (Input.anyKeyDown)
                GameManager.instance.RestartGame();
            return;
        }

        // Move forward non stop
        transform.position -= transform.forward * Time.deltaTime * slidingSpeed;

        // Calculate target position for left and right movement
        if (Input.GetKey(KeyCode.A))
            targetX += (transform.right * Time.deltaTime * turningSpeed).x;
        if (Input.GetKey(KeyCode.D))
            targetX -= (transform.right * Time.deltaTime * turningSpeed).x;

        if (targetX < limMinX) targetX = limMinX;
        if (targetX > limMaxX) targetX = limMaxX;

        // Smoothly move towards the target position
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), Time.deltaTime * turningSpeed);

        // Set the animator parameters
        int direction = 0;
        if (Input.GetKey(KeyCode.A)) direction = 1;
        if (Input.GetKey(KeyCode.D)) direction = 2;
        animator.SetInteger("direction", direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isCollided = true;
            GameManager.instance.ShowGameOverPopUp();

            // if is not infinite
            if (PlayerPrefsManager.lastLevelLoaded == 0 && ScoreCounter.instance.GetScore() > PlayerPrefsManager.highScore)
                PlayerPrefsManager.highScore = ScoreCounter.instance.GetScore();
        }
    }

    public void IncrementSpeed()
    {
        slidingSpeed += 0.5f;
        turningSpeed += 0.5f;
    }

    public void StartSliding()
    {
        canSlide = true;
        GameManager.instance.HideInitialMenu();
    }
}
