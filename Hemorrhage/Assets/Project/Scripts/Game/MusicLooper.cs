/**
 * Created 12/12/2020
 * By: Omer Farkhand
 * Last Modified 12/12/2020
 * By: Omer Farkhand
 * 
 * Contributors: Omer Farkhand
 */

using UnityEngine;

/*
 * MusicLooper loops the part of music that is to be looped, ends once the trigger is passed through
 */
public class MusicLooper : MonoBehaviour
{
    public AudioClip[] audioClips;

    public GameObject backgroundMusic;
    public AudioSource introAudioSource;
    public AudioSource loopAudioSource;
    public AudioSource endAudioSource;

    // Update is called once per frame
    void Update()
    {
        // Checks if background music is not false, then checks whether the intro of the song is playing, if not switches the clip to loop
        if(backgroundMusic.activeInHierarchy != false)
        {
            if (!introAudioSource.isPlaying)
            {
                introAudioSource.Stop();
                introAudioSource.clip = loopAudioSource.clip;
                introAudioSource.loop = true;
                introAudioSource.Play();
            }
        }
    }

    // OnTriggerEnter detects all trigger collisions on entry
    private void OnTriggerEnter(Collider other)
    {
        // Condition checks if colliding object has a MusicEnd tag
        if(other.tag == "MusicEnd")
        {
            // Plays the End of the Song
            introAudioSource.Stop();
            introAudioSource.loop = false;
            introAudioSource.clip = endAudioSource.clip;
            introAudioSource.Play();
            Destroy(GameObject.FindGameObjectWithTag("MusicEnd"));
        }
    }
}