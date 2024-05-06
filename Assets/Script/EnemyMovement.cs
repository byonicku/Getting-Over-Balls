using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private NavMeshAgent agent;

    // Chase Radius
    public float chaseRadius = 20.0f;
    private Vector3 initialPosition;

    // Speed
    public float baseSpeed = 0f;
    private float chaseSpeed = 5.0f;
    public ScoreController scoreController;
    public PauseController pauseController;
    

    void Start()    
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        InitializeController();
        initialPosition = transform.position;
        baseSpeed = chaseSpeed;
    }

    void InitializeController()
    {
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

    // Update is called once per frame
    void Update()
    {
        SpeedUpdate();

        if (Vector3.Distance(player.position, transform.position) <= chaseRadius)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(initialPosition);
        }
    }

    public void SpeedUpdate()
    {
        int score = scoreController.GetScore();
        switch (score)
        {
            case 2:
                baseSpeed = 6; break;
            case 4:
                baseSpeed = 6.5f; break;
            case 6:
                baseSpeed = 7; break;            
        }
    }
}
