using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public GameObject retryBtn;
    public GameObject mainMenuBtn;
    public GameObject backBtn;
    public TextMeshProUGUI dashText;
    public GameObject middleTextObj;
    public TextMeshProUGUI middleText;
    void Start()
    {
        panel = GameObject.Find("Panel");
        panel.SetActive(false);

        retryBtn = GameObject.Find("Retry");
        mainMenuBtn = GameObject.Find("Main Menu");
        backBtn = GameObject.Find ("Back");

        retryBtn.SetActive(false);
        mainMenuBtn.SetActive(false);
        backBtn.SetActive(false);

        GameObject dashTextObject = GameObject.Find("Dash");
        if (dashTextObject != null)
        {
            dashText = dashTextObject.GetComponent<TextMeshProUGUI>();
            if (dashText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on 'Dash' object.");
            }
        }
        else
        {
            Debug.LogError("'Dash' object not found.");
        }

        GameObject middleTextObject = GameObject.Find("Middle");
        if (middleTextObject != null)
        {
            middleTextObj = middleTextObject;
            middleTextObj.SetActive(false);
            middleText = middleTextObj.GetComponent<TextMeshProUGUI>();
            if (middleText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on 'Middle' object.");
            }
        }
    }

    public void WinUI()
    {
        panel.SetActive(true);
        retryBtn.SetActive(true);
        mainMenuBtn.SetActive(true);
        middleTextObj.SetActive(true);
        middleText.text = "You Win!";
        middleText.color = Color.green;
    }
    public void LoseUI()
    {
        panel.SetActive(true);
        retryBtn.SetActive(true);
        mainMenuBtn.SetActive(true);
        middleTextObj.SetActive(true);
        middleText.text = "You Lose!";
        middleText.color = Color.red;
    }

    public void PauseUI(bool isPaused)
    {
        if (isPaused)
        {
            panel.SetActive(true);
            retryBtn.SetActive(true);
            mainMenuBtn.SetActive(true);
            backBtn.SetActive(true);
            middleTextObj.SetActive(true);
            middleText.text = "Paused";
            middleText.color = Color.red;
        }
        else
        {
            panel.SetActive(false);
            retryBtn.SetActive(false);
            mainMenuBtn.SetActive(false);
            backBtn.SetActive(false);
            middleTextObj.SetActive(false);
            middleText.text = string.Empty;
            middleText.color = Color.black;
        }
    }

    public void DashUI(float dashCD)
    {
        if (dashCD > 0)
        {
            dashText.text = "Dash CD : " + ((int)dashCD).ToString() + " seconds";
        }
        else
        {
            dashText.text = "Dash is Ready!";
        }
    }
}
