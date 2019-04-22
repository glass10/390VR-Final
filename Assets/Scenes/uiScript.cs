using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class uiScript : MonoBehaviour
{
    public static SortedList leaderboard = new SortedList();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Start game
    public static void startGame(bool newGame)
    {
        Debug.Log("Game Started");
        //Do countdown

        //Spawn first wave of ships
        if (newGame)
        {

        }
        else
        {
            //Start game with old configuration
        }

        // Set game as active
        galagaMoveShip.gameActive = true;
    }

    // Pause game
    public static void pauseGame(bool liveDecrement)
    {
        Debug.Log("Game Paused");
        // Set game as inactive
        galagaMoveShip.gameActive = false;

        //Player lost life
        if (liveDecrement)
        {
            // Update Lives
            galagaMoveShip.lives--;
            GameObject textObject1 = GameObject.Find("Lives");
            textObject1.GetComponent<TextMesh>().text = "Lives: " + galagaMoveShip.lives;

            // Restart Game or stop
            if(galagaMoveShip.lives > 0)
            {
                startGame(false);
            }
            else
            {
                stopGame();
            }

        }

        // Load UI to make further selections
    }

    // Quit game
    public static void stopGame()
    {
        Debug.Log("Game Stopped");
        // Set game as inactive
        galagaMoveShip.gameActive = false;

        // Write high score to leaderboard and save
        int finalScore = galagaMoveShip.score;
        leaderboard.Add(finalScore, System.DateTime.Now.ToString("MM/dd/yyyy/"));
        save();

        // Reset globals
        galagaMoveShip.score = 0;
        galagaMoveShip.lives = 3;
        galagaMoveShip.gameSpeed = 1.0;

        // Change scenes
    }

    // Load leaderboard
    static void load()
    {
        // Load Galaga Leaderboard
        if (File.Exists(Application.persistentDataPath + "/galaga.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/galaga.dat");
            SortedList newLeaderboard = (SortedList)bf.Deserialize(fs);
            fs.Close();

            leaderboard = newLeaderboard;


        }
    }

    // Save leaderboard
    static void save()
    {
        // Save Galaga Leaderboard
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs1 = File.Open(Application.persistentDataPath + "/galaga.dat", FileMode.OpenOrCreate);
        bf.Serialize(fs1, leaderboard);
        fs1.Close();
    }
}
