/**
 * Created DD/MM/YYYY
 * By: Omer Farkhand
 * Last Modified DD/MM/YYYY
 * By: Omer Farkhand
 * 
 * Contributors: Omer Farkhand
 */

using UnityEngine;

/*
 * DoorScript activates trap doors hidden within the main level Level01
 */
public class DoorScript : MonoBehaviour
{
    public GameObject Door;

    // OnCollisionEnter detects all collisions on entry
    private void OnCollisionEnter(Collision collision)
    {
        // The Door Closes when the player enters this trigger
        Door.SetActive(true);
    }
}
