/**
 * Created 01/12/2020
 * By: Omer Farkhand
 * Last Modified DD/MM/YYYY
 * By: Omer Farkhand
 * 
 * Contributors: Omer Farkhand
 */

using UnityEngine;

/*
 * This script handles triggering the background music
 */
public class Background_Music : MonoBehaviour
{
    //Fields
    public GameObject backgroundMusic;
    public AudioSource source;

    // OnTriggerEnter detects all trigger collisions on entry
    private void OnTriggerEnter(Collider other)
    {
        // Condition checks if the colliding object has a Player tag 
        if(other.gameObject.tag == "Player")
        {
            // Starts the background music
            backgroundMusic.SetActive(true);
            source.Play();
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
