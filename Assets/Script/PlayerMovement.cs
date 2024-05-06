using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Pergerakan Bola
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Kecepatan
    public float speed = 0;
    public float dashSpeed = 8f; // Speed of the dash
    public float dashDuration = 0.1f; // Duration of the dash in seconds
    public float dashCooldown = 13.0f; // Cooldown duration in seconds

    // Score
    public ScoreController scoreController;

    // Pause
    public PauseController pauseController;

    // Text UI
    public UIController UIController;

    // Dash state
    private bool isDashing = false;
    private float dashCooldownRemaining = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializeController();
        rb.mass = 1f;
        rb.drag = 1f;
        Time.timeScale = 1f;
    }

    // Method to automatically initialize UI elements
    void InitializeController()
    {
        GameObject uiControllerObj = GameObject.Find("UIController");
        if (uiControllerObj != null)
        {
            UIController = uiControllerObj.GetComponent<UIController>();
            if (UIController == null)
            {
                UIController = uiControllerObj.AddComponent<UIController>();
            }
        }
        else
        {
            Debug.LogError("GameObject 'UIController' not found.");
        }

        GameObject scoreControllerObj = GameObject.Find("ScoreController");
        if (scoreControllerObj != null)
        {
            scoreController = scoreControllerObj.GetComponent<ScoreController>();
            if (scoreController == null)
            {
                scoreController = scoreControllerObj.AddComponent<ScoreController>();
            }
        }
        else
        {
            Debug.LogError("GameObject 'ScoreController' not found.");
        }

        GameObject pauseControllerObj = GameObject.Find("PauseController");
        if (pauseControllerObj != null)
        {
            pauseController = pauseControllerObj.GetComponent<PauseController>();
            if (pauseController == null)
            {
                pauseController = pauseControllerObj.AddComponent<PauseController>();
            }
        }
        else
        {
            Debug.LogError("GameObject 'PauseController' not found.");
        }
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && other.gameObject.activeSelf)
        {
            other.gameObject.SetActive(false);
            scoreController.AddScore();
            rb.mass += 0.03f;
            rb.drag += 0.08f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && scoreController.GetScore() != 8)
        {
            UIController.LoseUI();
            Time.timeScale = 0f;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started && (scoreController.GetScore() != 8 || Time.timeScale == 0))
        {
            pauseController.TogglePause();
            UIController.PauseUI(pauseController.GetIsPaused());
        }
    }

    public void TogglePause()
    {
        pauseController.TogglePause();
        UIController.PauseUI(pauseController.GetIsPaused());
    }

    void FixedUpdate()
    {
        if (scoreController.GetScore() >= 8)
        {
            rb.freezeRotation = true;
            UIController.WinUI();
            Time.timeScale = 0f;
        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        if (isDashing)
        {
            movement *= dashSpeed;
        }

        if (dashCooldownRemaining > 0)
        {
            dashCooldownRemaining -= Time.deltaTime;
            UIController.DashUI(dashCooldownRemaining);
        }

        rb.AddForce(movement * speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && dashCooldownRemaining <= 0 && !isDashing && !rb.freezeRotation)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        dashCooldownRemaining = dashCooldown;
    }
}
