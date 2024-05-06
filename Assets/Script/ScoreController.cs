using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private int score;

    void Start()
    {
        GameObject text = GameObject.Find("Score Counter");
        if (text != null )
        {
            countText = text.GetComponent<TextMeshProUGUI>();
        }

        this.score = 0;
        countText.text = "Score: " + score.ToString();
    }

    public void AddScore()
    {
        this.score++;
        countText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    { return this.score; }
}
