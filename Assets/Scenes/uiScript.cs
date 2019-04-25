using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class uiScript : MonoBehaviour
{
    public static SortedList leaderboard = new SortedList();
    public bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait(float duration)
    {
        //This is a coroutine
        waiting = true;
        yield return new WaitForSeconds(duration);   //Wait
        waiting = false;
    }

    // Start game
    public static void startGame(bool newGame)
    {
        Debug.Log("Game Started");
        //Do countdown

        // Set game as active
        galagaMoveShip.gameActive = true;

        // Spawn Ships
        //galagaMoveShip.spawnWave();

        // Run game
        //galagaMoveShip.runGame();
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
                // Restart Game
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
        // Load Galaga Leaderboard
        if (File.Exists(Application.persistentDataPath + "/galaga.dat"))
        {
            BinaryFormatter bf1 = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/galaga.dat");
            SortedList newLeaderboard = (SortedList)bf1.Deserialize(fs);
            fs.Close();

            leaderboard = newLeaderboard;
        }

        // Set game as inactive
        galagaMoveShip.gameActive = false;

        // Write high score to leaderboard and save
        int finalScore = galagaMoveShip.score;
        Debug.Log("Score being Recorded: " + finalScore);
        leaderboard.Add(finalScore, System.DateTime.Now.ToString("MM/dd/yyyy"));
        //save();

        // Save Galaga Leaderboard
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs1 = File.Open(Application.persistentDataPath + "/galaga.dat", FileMode.OpenOrCreate);
        bf.Serialize(fs1, leaderboard);
        fs1.Close();

        // Reset globals
        //galagaMoveShip.score = 0;
        //galagaMoveShip.lives = 3;
        //galagaMoveShip.gameSpeed = 1.0;

        // Change scenes
        SceneManager.LoadScene("GameSelection", LoadSceneMode.Single);
    }


    // Load leaderboard
    void load()
    {
        // Load Galaga Leaderboard
        //if (File.Exists(Application.persistentDataPath + "/galaga.dat"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream fs = File.OpenRead(Application.persistentDataPath + "/galaga.dat");
        //    SortedList newLeaderboard = (SortedList)bf.Deserialize(fs);
        //    fs.Close();

        //    leaderboard = newLeaderboard;
        //}
    }

    // Save leaderboard
    void save()
    {
        // Save Galaga Leaderboard
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs1 = File.Open(Application.persistentDataPath + "/galaga.dat", FileMode.OpenOrCreate);
        bf.Serialize(fs1, leaderboard);
        fs1.Close();
    }


}
