using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacmanCollision : MonoBehaviour
{
    public AudioSource chime;
    public AudioSource eatGhost;

    //Ship crashed
    void OnTriggerEnter(Collider collision)
    {

        //if (collision.gameObject.tag == "bigCoin")
        //{
        //    chime.Play();
        //    GameObject.Destroy(collision.gameObject);
        //    pacmanGameController.score += 50;
        //    Debug.Log("Coin Collision with " + collision.gameObject.name + "score = " + pacmanGameController.score);

        //} else if (collision.gameObject.tag == "smallCoin")
        //{
        //    chime.Play();
        //    GameObject.Destroy(collision.gameObject);
        //    pacmanGameController.score += 10;
        //    Debug.Log("Coin Collision with " + collision.gameObject.name);
        //} else if (collision.gameObject.tag == "Ghost")
        //{
        //    //eatGhost.Play();
        //    //GameObject.Destroy(collision.gameObject);
        //}

        ////Update Score
        //GameObject textObject1 = GameObject.Find("score");
        ////textObject1.GetComponent<TextMesh>().text = "Score: " + pacmanGameController.score;
        //textObject1.GetComponent<TextMesh>().text = "Score: 100";

        // Lose life
        //loseLife();
    }
}
