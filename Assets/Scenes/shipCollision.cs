using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipCollision : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void loseLife()
    {
        uiScript.pauseGame(true);
    }

    //Ship crashed
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision");
        //Explosion handling - Other Ship
        GameObject explode = Instantiate(explosion, collision.transform.position, Quaternion.identity);
        //explode.GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(explode, 1.0f);
        explosionSound.Play();

        //Explosion handling - Main Ship
        GameObject explode2 = Instantiate(explosion, transform.position, Quaternion.identity);
        //explode2.GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(explode2, 1.0f);

        // Destroy Ship
        GameObject.Destroy(collision.gameObject);
        galagaMoveShip.numShips--;

        // Lose life
        loseLife();
    }
}
