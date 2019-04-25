using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;

    public AudioSource eatGhost;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // end game
            Debug.Log("Colliding with Player " + collision.gameObject.name);
            eatGhost.Play();
            GameObject.Destroy(collision.gameObject);
            pacmanUiScript.pauseGame(true);
        }
        //if (collision.gameObject.tag == "bigCoin" || collision.gameObject.tag == "smallCoin")
        //{
        //    Physics.IgnoreCollision(collision.gameObject<Collider>, collider1);
        //}
    }
}

