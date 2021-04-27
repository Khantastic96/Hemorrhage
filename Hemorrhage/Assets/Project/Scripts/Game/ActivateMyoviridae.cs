/**
 * Created DD/MM/YYYY
 * By: [Insert Name]
 * Last Modified DD/MM/YYYY
 * By: [Insert Name]
 * 
 * Contributors:
 */

using UnityEngine;

/*
 * [Describe the function of the class]
 */
public class ActivateMyoviridae : MonoBehaviour
{
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter detects all trigger collisions on entry
    private void OnTriggerEnter(Collider other)
    {
        // Condition checks if the colliding object has a Player tag
        if(other.gameObject.tag == "Player")
        {
            Enemy.GetComponent<Myoviridae>().m_speed = 6;
            Enemy.GetComponent<Myoviridae>().m_rushSpeed = 9;
        }
    }
}
