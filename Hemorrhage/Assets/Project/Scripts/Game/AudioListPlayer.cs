/**
 * Created 02/12/2020
 * By: Aswad Mirza
 * Last Modified DD/MM/YYYY
 * By: [Insert Name]
 * 
 * Contributors: Aswad Mirza
 */

using UnityEngine;

/*
 * AudiolistPlayer basically takes a public array of audioclips and just plays through each clip one by one until it reaches the
 * end of the audioclips, and then the script deletes itself from the object
 */
public class AudioListPlayer : MonoBehaviour
{

    public AudioClip[] clips;
    AudioSource m_audioSource;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //while (counter < clips.Length) {
        // Condition checks if counter is less than the size of the AudioClips array
        if (counter < clips.Length)
        {
            // Condition checks if an audio source is playing/active
            if (m_audioSource.isPlaying)
            {

            }
            // Condition checks if an audio source is not playing/active
            else
            {
                // Increments the counter
                counter++;
                // Condition checks if counter is less than the size of the AudioClips array
                if (counter < clips.Length) {
                    // Plays the AudioClip at the index value equivalent to counter value
                    m_audioSource.clip = clips[counter];
                    m_audioSource.Play();
                }   
            }
        }
        // Condition checks if counter is greater than equal to the size of the AudioClips array
        else {
            // Destroy the GameObject attached with this instance of the script
            Destroy(this);
        }   
    }
}