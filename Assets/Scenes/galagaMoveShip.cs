using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galagaMoveShip : MonoBehaviour
{
    GameObject ship;
    private RaycastHit hit;
    public GameObject m_shotPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ship = GameObject.Find("MainShip");

        // Move ship and camera right
        if (Input.GetKey(KeyCode.Period) && ship.transform.position.x < 300)
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
        // Move ship and camera left
        if (Input.GetKey(KeyCode.Comma) && ship.transform.position.x > -300)
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

        if (Input.GetKeyDown(KeyCode.Space))
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
            }
        }
    }
}
