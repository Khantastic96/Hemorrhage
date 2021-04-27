/**
 * Created 02/10/2020
 * By: Sharek Khan
 * Last Modified 02/10/2020
 * By: Sharek Khan
 *
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * CellArmour handles all the logic with the CellArmour GameObject and how it applies within the game
 */
public class CellArmour : MonoBehaviour
{
    public GameObject cellArmour;
    public float rotationSpeed = 80f;
    public int armourPoint = 100;

    private AudioSource m_audio;

    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotates the Cell Armour throught every frame
        cellArmour.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    // OnTriggerEnter detects all trigger collisions on entry
    private void OnTriggerEnter(Collider other)
    {
        // Condition checks if AudioSource is initialized
        if (m_audio != null) {
            // Condition checks if AudioSource isn't already playing
            if (!m_audio.isPlaying) {
                // Plays the audio cue for the CellArmour
                m_audio.Play();
            }
        }
    }
}