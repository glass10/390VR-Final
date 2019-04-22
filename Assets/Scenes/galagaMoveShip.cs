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

    // Start is called before the first frame update
    void Start()
    {
        //Temp
        gameActive = true;
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

        //Temp -> Change to collision 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loseLife();
        }

    }

    void loseLife()
    {
        uiScript.pauseGame(true);
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
        Debug.Log("Shoot");
        //Create Laser
        GameObject go = GameObject.Instantiate(m_shotPrefab, ship.transform.position, ship.transform.rotation) as GameObject;
        GameObject.Destroy(go, 3f);

        //Do Raycast Things
        Vector3 fwd = ship.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(ship.transform.position, fwd * 500, Color.green);
        if (Physics.Raycast(ship.transform.position, fwd * 500, out hit, 200))
        {
            Debug.Log("Hit " + hit.transform.name);
            if (hit.transform.name != "Backdrop Stars")
            {
                // Get gameobj from ship hit
                GameObject obj = GameObject.Find(hit.transform.name);
                Debug.Log(hit.transform.name + " destroyed");

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
                Debug.Log("Score: " + score);
                GameObject textObject1 = GameObject.Find("Score");
                textObject1.GetComponent<TextMesh>().text = "Score: " + score;

                // Destroy game object
                GameObject.Destroy(obj);
            }
        }
    }
}
