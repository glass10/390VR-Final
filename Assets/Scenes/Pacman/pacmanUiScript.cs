using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class pacmanUiScript : MonoBehaviour
{
    public static SortedList leaderboard = new SortedList();
    public bool waiting = false;

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

        // Set game as active
        pacmanGameController.gameActive = true;

    }

    // Pause game
    public static void pauseGame(bool liveDecrement)
    {
        Debug.Log("Game Paused");
        // Set game as inactive
        pacmanGameController.gameActive = false;

        //Player lost life
        if (liveDecrement)
        {
            // Update Lives
            pacmanGameController.lives--;
            GameObject textObject1 = GameObject.Find("lives");
            textObject1.GetComponent<TextMesh>().text = "Lives = " + pacmanGameController.lives;

            // Restart Game or stop
            if (pacmanGameController.lives > 0)
            {
                // Restart Game
                startGame(false);
            }
            else
            {
                stopGame();
                pacmanGameController.gameOver = true;
            }

        }

        // Load UI to make further selections
    }

    // Quit game
    public static void stopGame()
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
        pacmanGameController.gameActive = false;

        // Write high score to leaderboard and save
        int finalScore = pacmanGameController.score;
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
