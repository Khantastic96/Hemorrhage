/**
 * Created 26/10/YYYY
 * By: Sharek Khan
 * Last Modified 26/10/2020
 * By: Sharek Khan
 *
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * CholesterolCrateBroken handles all the logic with the CholesterolCrate GameObject and how it applies within the game
 */
public class CholesterolCrateBroken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerExit detects all trigger collisions on exit
    void OnTriggerExit(Collider collider)
    {
        // Condition checks if the colliding object has a AIDetection tag
        if(collider.gameObject.tag == "AIDetection")
        {
            // Deletes object one it's outside of Players proximity
            Debug.Log("CholesterolCrate(Broken) Destroyed.");
            Destroy(this.gameObject);
        }
    }
}
