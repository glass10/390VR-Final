using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galagaMoveShip : MonoBehaviour
{
    GameObject ship;
    private RaycastHit hit;
    public GameObject m_shotPrefab;

    // Shared Globals 
    public static int score = 0;
    public static int lives = 3;
    public static bool gameActive = false;
    public static double gameSpeed = 1.0;
    public static int stage = 1;
    public static int numShips = 0;

    // Defined Globals
    public GameObject boss;
    public GameObject butterfly;
    public GameObject bee;
    public GameObject explosion;

    public bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        //Temp
        spawnWave();
        gameActive = true;
        //runGame();
        //uiScript.startGame(false);
    }

    IEnumerator Wait(float duration)
    {
        //This is a coroutine
        waiting = true;
        yield return new WaitForSeconds(duration);   //Wait
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        ship = GameObject.Find("MainShip");

        // Move ship and camera right
        if (Input.GetKey(KeyCode.Period) && ship.transform.position.x < 300 && gameActive)
        {
            moveRight();
        }
        // Move ship and camera left
        if (Input.GetKey(KeyCode.Comma) && ship.transform.position.x > -300 && gameActive)
        {
            moveLeft();
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Space) && gameActive)
        {
            shoot();
        }

        //Continue Running Game
        if (gameActive && numShips != 0 && waiting == false)
        {
            runGame();
        }

        //End of Stage
        if (numShips == 0)
        {
            completeStage();
        }

        // Move Diving Ships
        GameObject[] divers = GameObject.FindGameObjectsWithTag("Diving");
        // Get player position
        Transform target = GameObject.Find("MainShip").transform;
        if (gameActive && divers.Length != 0)
        {
            for(int i = 0; i < divers.Length; i++)
            {
                // Move and rotate towards player
                float step = (float)((15 * gameSpeed) * Time.deltaTime); // calculate distance to move
                Transform diveT = divers[i].transform;
                diveT.position = Vector3.MoveTowards(diveT.position, target.position, step);
                diveT.LookAt(target);

                //Shoot at ship
                int randomNum = Random.Range(0, 250);
                if(randomNum == 9)
                {
                    Debug.Log("Shot Fired");
                    //Raycast
                    Vector3 fwd = diveT.TransformDirection(Vector3.forward);
                    if (Physics.Raycast(diveT.position, fwd * 500, out hit, 200))
                    {
                        if (hit.transform.name == "MainShip")
                        {
                            //Spawn laser
                            GameObject go = GameObject.Instantiate(m_shotPrefab, diveT.position, diveT.rotation) as GameObject;
                            GameObject.Destroy(go, 0.5f);

                            // Destroy Other Ship
                            GameObject.Destroy(diveT.gameObject);
                            numShips--;

                            // Animate mainShip smoke
                            GameObject explode = Instantiate(explosion, target.position, Quaternion.identity);
                            //explode.GetComponent<ParticleSystem>().Play();
                            GameObject.Destroy(explode);

                            // Lose life
                            uiScript.pauseGame(true);
                        }
                    }
                   
                }
            }
        }

    }

    void runGame()
    {
            Debug.Log("Game Running");
            //Wait amount of time (Multiplier)
            float time = (float)(5);
            StartCoroutine(Wait(time));

            //Pick random ship
            GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
            int size = ships.Length;
            if(size > 0)
            {
                int randomNum = Random.Range(0, size - 1);
                while (!ships[randomNum].name.Contains("Clone"))
                {
                    randomNum = Random.Range(0, size - 1);
                }
                if(ships[randomNum] && ships[randomNum].name.Contains("(Clone)"))
                {
                    //Set to diving
                    ships[randomNum].tag = "Diving";
                }
            }
    }

    void completeStage()
    {
        Debug.Log("Stage Complete");

        //Pause Game
        uiScript.pauseGame(false);

        // Update Stage #
        stage += 1;

        //Write stage #
        GameObject textObject1 = GameObject.Find("Stage");
        textObject1.GetComponent<TextMesh>().text = "Stage: " + stage;

        // Update Speed
        gameSpeed *= 1.125;

        //Spawn new wave
        spawnWave();

        //Pause for a few seconds
        StartCoroutine(Wait(3));

        //Restart game
        uiScript.startGame(false);
    }


    void spawnWave()
    {

        //Spawn Bosses
        for (int i = -24; i <= 24; i += 12)
        {
            Instantiate(boss, new Vector3(i, 3, 100), Quaternion.identity);
            numShips++;
        }

        // Spawn Butterflies
        for (int i = -24; i <= 24; i += 12)
        {
            Instantiate(butterfly, new Vector3(i, 3, 75), Quaternion.identity);
            numShips++;
        }

        // Spawn Bees
        for (int i = -24; i <= 24; i += 12)
        {
            Instantiate(bee, new Vector3(i, 3, 50), Quaternion.identity);
            numShips++;
        }
    }

    void dive(GameObject obj)
    {
        if(obj)
        {
            Debug.Log("Ship Diving");
            //obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 5, obj.transform.position.z);

            // Destroy
            if(obj)
            {
                GameObject.Destroy(obj, 3.0f);
            }
            numShips--;
        }
    }

    void moveLeft()
    {
        //Move Camera/Player
        Vector3 position = this.transform.position;
        position.x -= 2;
        this.transform.position = position;

        //Move Ship
        position = ship.transform.position;
        position.x -= 2;
        ship.transform.position = position;
    }

    void moveRight()
    {
        //Move Camera/Player
        Vector3 position = this.transform.position;
        position.x += 2;
        this.transform.position = position;

        //Move Ship
        position = ship.transform.position;
        position.x += 2;
        ship.transform.position = position;
    }

    void shoot()
    {
        //Create Laser
        GameObject go = GameObject.Instantiate(m_shotPrefab, ship.transform.position, ship.transform.rotation) as GameObject;
        GameObject.Destroy(go, 0.5f);

        //Do Raycast Things
        Vector3 fwd = ship.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(ship.transform.position, fwd * 500, Color.green);
        if (Physics.Raycast(ship.transform.position, fwd * 500, out hit, 200))
        {
            Debug.Log("Hit " + hit.transform.name);
            if (hit.transform.name != "Backdrop Stars")
            {
                // Get gameobj from ship hit
                GameObject obj = hit.transform.gameObject;

                // Update Score
                /* Bee: In formation    50
                    Bee: Diving 100
                    Butterfly: In formation 80
                    Butterfly: Diving   160 
                    Boss Galaga: In formation   150
                    Boss Galaga: Diving alone   400                      
                    */
                if (obj.name.Contains("Bee")) //Bee
                {
                    if (obj.tag == "Diving")
                    {
                        score += 100;
                    }
                    else
                    {
                        score += 50;
                    }
                }
                else if (obj.name.Contains("Butterfly")) //Butterfly
                {
                    if (obj.tag == "Diving")
                    {
                        score += 160;
                    }
                    else
                    {
                        score += 80;
                    }
                }
                else if (obj.name.Contains("Boss")) //Boss Galaga
                {
                    if (obj.tag == "Diving")
                    {
                        score += 400;
                    }
                    else
                    {
                        score += 150;
                    }
                }

                //Update Score
                GameObject textObject1 = GameObject.Find("Score");
                textObject1.GetComponent<TextMesh>().text = "Score: " + score;

                // Destroy game object
                if(obj)
                {
                    GameObject.Destroy(obj);
                }
                numShips--;
            }
        } 
    }
}
