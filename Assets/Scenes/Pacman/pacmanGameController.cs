using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pacmanGameController : MonoBehaviour
{
    public Camera cam;
    public AudioSource gameSound;
    public AudioSource gameStart;
    public AudioSource gameEnd;

    public NavMeshAgent agent;
    public static int score = 0;
    public static int lives = 3;
    public static bool gameActive = false;
    public bool waiting = false;
    public static bool gameOver = false;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
        gameStart.Play();

        Physics.IgnoreLayerCollision(9, 11);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }

        // Move player and camera right
        if (Input.GetKey("w") && player.transform.position.z < 10 && gameActive)
        {
            moveForward();
            if (!gameSound.isPlaying)
            {
                gameSound.Play();
            }
        }
        // Move player and camera left
        if (Input.GetKey("x") && player.transform.position.z > -10 && gameActive)
        {
            moveBackward();
            if (!gameSound.isPlaying)
            {
                gameSound.Play();
            }
        }

        if (Input.GetKey("d") && player.transform.position.x < 10 && gameActive)
        {
            moveRight();
            if (!gameSound.isPlaying)
            {
                gameSound.Play();
            }
        }
        // Move player and camera left
        if (Input.GetKey("a") && player.transform.position.x > -10 && gameActive)
        {
            moveLeft();
            if (!gameSound.isPlaying)
            {
                gameSound.Play();
            }
        }
        if (Input.GetKey("r") && gameActive)
        {
            rotate90();
        }

        //Continue Running Game
        if (gameActive && lives != 0 && waiting == false)
        {
            runGame();
        }

        if (gameOver == true)
        {
            gameEnd.Play();
        }

    }

    IEnumerator Wait(float duration)
    {
        //This is a coroutine
        waiting = true;
        yield return new WaitForSeconds(duration);   //Wait
        waiting = false;
    }

    void runGame()
    {
        Debug.Log("Game Running");
        //Wait amount of time (Multiplier)
        float time = (float)(5);
        StartCoroutine(Wait(time));

        //Pick random player
    }
    void moveLeft()
    {
        //Move Camera/Player
        Vector3 position = agent.transform.position;
        position = agent.transform.position;
        position.x -= 0.2f;
        agent.transform.position = position;
    }

    void moveRight()
    {
        Vector3 position = agent.transform.position;
        position = agent.transform.position;
        position.y += 0.2f;
        agent.transform.position = position;
    }

    void moveForward()
    {
        Vector3 position = agent.transform.position;
        position = agent.transform.position;
        position.z += 0.2f;
        agent.transform.position = position;
    }

    void moveBackward()
    {
        Vector3 position = agent.transform.position;
        position = agent.transform.position;
        position.z -= 0.2f;
        agent.transform.position = position;

    }
    void rotate90()
    {
        //Move Camera/Player
        player.transform.Rotate(new Vector3(0, 90, 0));
    }


    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision with Player " + collision.gameObject.name);
    }

}
