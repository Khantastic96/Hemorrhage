/**
 * Created DD/MM/YYYY
 * By: Omer Farkhand
 * Last Modified 07/12/2020
 * By: Omer Farkhand
 * 
 * Contributors: Omer Farkhand
 */

using UnityEngine;

/*
 * BossLevel activates the trap door and enables the StreptobacillusMoniliformis scripts
 */
public class BossLevel : MonoBehaviour
{
    public GameObject door;

    // OnTriggerEnter detects all trigger collisions on entry
    private void OnTriggerEnter(Collider other)
    {
        // Condition checks if the colliding object has a Player tag
        if (other.gameObject.name == "Player")
        {
            // Enables the Scripts of the Boss and it's body parts
            door.SetActive(true);
            GameObject.Find("Tail").GetComponent<StreptobacillusMoniliformisBody>().enabled = true;
            GameObject.Find("Body4").GetComponent<StreptobacillusMoniliformisBody>().enabled = true;
            GameObject.Find("Body3").GetComponent<StreptobacillusMoniliformisBody>().enabled = true;
            GameObject.Find("Body2").GetComponent<StreptobacillusMoniliformisBody>().enabled = true;
            GameObject.Find("Body").GetComponent<StreptobacillusMoniliformisBody>().enabled = true;
            GameObject.Find("Head").GetComponent<StreptobacillusMoniliformisHead>().enabled = true;
        }
    }
}
