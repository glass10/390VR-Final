using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    Vector3 g1pos; 
    Vector3 g2pos;
    Vector3 g3pos;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        g1pos = target.transform.position;
        g2pos = target.transform.position;
        g3pos = target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //int random = Random.Range(0, 250);

        agent.destination = target.transform.position;
        //if (agent.name == "Ghost1")
            //{
            //    agent.destination = target.transform.position;
            //}


            //else if (agent.name == "Ghost2" && agent.transform.position == g2pos)
            //{
            //    Vector3 position = agent.transform.position;
            //    position = agent.transform.position;
            //    int randomX = Random.Range(-10, 10);
            //    int randomY = Random.Range(-10, 10);
            //    position.x = randomX;
            //    position.y = randomY;
            //    agent.destination = position;
            //}
            //else if (agent.name == "Ghost3")
            //{
            //    Vector3 position = agent.transform.position;
            //    position = agent.transform.position;
            //    int randomX = Random.Range(-10, 10);
            //    int randomY = Random.Range(-10, 10);
            //    position.x = randomX;
            //    position.y = randomY;
            //    agent.destination = position;
            //}
            //else if (agent.name == "Ghost4")
            //{
            //    Vector3 position = agent.transform.position;
            //    position = agent.transform.position;
            //    int randomX = Random.Range(-10, 10);
            //    int randomY = Random.Range(-10, 10);
            //    position.x = randomX;
            //    position.y = randomY;
            //    agent.destination = position;
            //}
        
    }

}

