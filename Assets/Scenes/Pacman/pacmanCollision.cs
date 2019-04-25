using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacmanCollision : MonoBehaviour
{
    public AudioSource chime;
    public AudioSource eatGhost;
    void loseLife()
    {
        pacmanUiScript.pauseGame(true);
    }

    //Ship crashed
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Coin Collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "bigCoin")
        {
            chime.Play();
            GameObject.Destroy(collision.gameObject);
            pacmanGameController.score += 50;
            
        } else if (collision.gameObject.tag == "smallCoin")
        {
            chime.Play();
            GameObject.Destroy(collision.gameObject);
            pacmanGameController.score += 10;
        } else if (collision.gameObject.tag == "Ghost")
        {
            //eatGhost.Play();
            //GameObject.Destroy(collision.gameObject);
        }

        //Update Score
        GameObject textObject1 = GameObject.Find("score");
        //textObject1.GetComponent<TextMesh>().text = "Score: " + pacmanGameController.score;
        textObject1.GetComponent<TextMesh>().text = "Score: 100";

        // Lose life
        //loseLife();
    }
}
