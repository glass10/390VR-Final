using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class pacmanGameController : MonoBehaviour
{
    public static SortedList leaderboard = new SortedList();
    public bool waiting = false;
    public bool eat = false;

    public Camera cam;
    public AudioSource gameSound;
    public AudioSource gameStart;
    public AudioSource gameEnd;
    public AudioSource chime;
    public AudioSource eatGhost;

    public NavMeshAgent agent;
    public static int score = 0;
    public static int lives = 3;
    public static bool gameActive = false;

    GameObject player;
    public Material blue;
    public Renderer ghost1;
    public Renderer ghost2;
    public Renderer ghost3;
    public Renderer ghost4;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
        gameStart.Play();

        Physics.IgnoreLayerCollision(9, 11);
        player = GameObject.Find("Player");
        StartCoroutine(pacmanEats(10));
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
        if (Input.GetKey("i") && player.transform.position.z < 10 && gameActive)
        {
            moveForward();
            if (!gameSound.isPlaying)
            {
                gameSound.Play();
            }
        }
        // Move player and camera left
        if (Input.GetKey("k") && player.transform.position.z > -10 && gameActive)
        {
            moveBackward();
            if (!gameSound.isPlaying)
            {
                gameSound.Play();
            }
        }

        if (Input.GetKey("l") && gameActive)
        {
            moveRight();
            if (!gameSound.isPlaying)
            {
                gameSound.Play();
            }
        }
        // Move player and camera left
        if (Input.GetKey("j") && player.transform.position.x > -10 && gameActive)
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
        position.x += 0.2f;
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
        if (collision.gameObject.tag == "bigCoin")
        {
            chime.Play();
            GameObject.Destroy(collision.gameObject);
            score += 50;
            Debug.Log("Player Collision with " + collision.gameObject.name);
            StartCoroutine(pacmanEats(10));
        }
        else if (collision.gameObject.tag == "smallCoin")
        {
            chime.Play();
            GameObject.Destroy(collision.gameObject);
            score += 10;
            Debug.Log("Player Collision with " + collision.gameObject.name);
        }
        else if (collision.gameObject.tag == "Ghost")
        {
            Debug.Log("Player Collision with " + collision.gameObject.name);
            eatGhost.Play();
            GameObject.Destroy(collision.gameObject);
            if (eat == true)
            {
                score += 200;
            }
            else
            {
                score -= 200;
                pauseGame(true); // lose a life
            }
        }
    

        //Update Score
        GameObject textObject1 = GameObject.Find("score");
        textObject1.GetComponent<TextMesh>().text = "Score: " + score;
        GameObject textObject2 = GameObject.Find("score2");
        textObject2.GetComponent<TextMesh>().text = "Score: " + score;

    }

    IEnumerator pacmanEats(float duration)
    {
        //This is a coroutine
        Debug.Log("Pacman Eats");
        Material g1m = ghost1.material;
        Material g2m = ghost2.material;
        Material g3m = ghost3.material;
        Material g4m = ghost4.material;
        eat = true;
        if (ghost1 && ghost2 && ghost3 && ghost4)
        {
            ghost1.material = blue;
            ghost2.material = blue;
            ghost3.material = blue;
            ghost4.material = blue;
        }
        yield return new WaitForSeconds(duration);   //Wait

        ghost1.material = g1m;
        ghost2.material = g2m;
        ghost3.material = g3m;
        ghost4.material = g4m;
        eat = false;
    }

    // Start game
    public static void startGame(bool newGame)
    {
        Debug.Log("Game Started");

        // Set game as active
        gameActive = true;

    }

    // Pause game
    public void pauseGame(bool liveDecrement)
    {
        Debug.Log("Game Paused");
        // Set game as inactive
        gameActive = false;

        //Player lost life
        if (liveDecrement)
        {
            // Update Lives
            lives--;
            GameObject textObject1 = GameObject.Find("lives");
            textObject1.GetComponent<TextMesh>().text = "Lives = " + lives;

            // Restart Game or stop
            if (lives > 0)
            {
                // Restart Game
                startGame(false);
            }
            else
            {
                gameEnd.Play();
                StartCoroutine(Wait(3));
                stopGame();
               
            }

        }
        else
        {
            pacmanGameController.lives++;
            GameObject textObject1 = GameObject.Find("lives");
            textObject1.GetComponent<TextMesh>().text = "Lives = " + pacmanGameController.lives;

        }

        // Load UI to make further selections
    }

    // Quit game
    public void stopGame()
    {
        Debug.Log("Game Stopped");

        // Load pacman Leaderboard
        if (File.Exists(Application.persistentDataPath + "/pacman.dat"))
        {
            BinaryFormatter bf1 = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/pacman.dat");
            SortedList newLeaderboard = (SortedList)bf1.Deserialize(fs);
            fs.Close();

            leaderboard = newLeaderboard;
        }

        // Set game as inactive
        gameActive = false;

        // Write high score to leaderboard and save
        int finalScore = score;
        Debug.Log("Score being Recorded: " + finalScore);
        if (finalScore != 0)
        {
            leaderboard.Add(finalScore, System.DateTime.Now.ToString("MM/dd/yyyy"));

            // Save pacman Leaderboard
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs1 = File.Open(Application.persistentDataPath + "/pacman.dat", FileMode.OpenOrCreate);
            bf.Serialize(fs1, leaderboard);
            fs1.Close();
        }

        // Change scenes
        SceneManager.LoadScene("GameSelection", LoadSceneMode.Single);
    }


}
