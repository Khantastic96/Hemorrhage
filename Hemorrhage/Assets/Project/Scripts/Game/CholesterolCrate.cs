/**
 * Created 04/10/2020
 * By: Sharek Khan
 * Last Modified 19/11/2020
 * By: Sharek Khan
 *
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * CholesterolCrate handles all the logic with the CholesterolCrate GameObject and how it applies within the game
 */
public class CholesterolCrate : MonoBehaviour
{
    private int m_crateHealth;
    private int m_rngChance;
    private bool m_isBroken;
    private AudioSource m_audioSource;

    // Broken Cholesterol Crate Model
    public GameObject brokenCholestrolCrate;
    // VitaCapsule GameObject
    public GameObject vitaCapsule;
    // CholesterolCrate base health
    public int crateHealth = 100;
    // CholesterolCrate chance to generate a VitaCapsule
    public int rngChance = 25;

    // Start is called before the first frame update
    void Start()
    {
        // Initializes the crates starting health
        m_crateHealth = crateHealth;
        // Initializes the crates chance to generate a VitaCapsule
        m_rngChance = rngChance;
        // Initializes the crates dexterity status
        m_isBroken = false;

        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks health of crate to verify if its broken
        if(m_crateHealth <= 0)
        {
            m_isBroken = true;
        }
        // Checks if the crate has been broken
        if(m_isBroken != false)
        {
            // Breaks the crate by subbing in the destroyed model
            Instantiate(brokenCholestrolCrate, transform.position, transform.rotation);
            // Checks if a random number generated is in bounds of RNG Chance
            if(RNG(1, 100) <= m_rngChance)
            {
                // Instantiates a VitaCapsule on destruction for Player pickup
                Instantiate(vitaCapsule, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), vitaCapsule.gameObject.transform.rotation);
            }
            // Destroys the base object
            Destroy(gameObject);
        }
    }

    // RNG receives two integers and returns a random number between their interval
    int RNG(int minValue, int maxValue)
    {
        System.Random rng = new System.Random();
        return rng.Next(minValue, maxValue);
    }

    // OnTriggerEnter detects all trigger collisions on entry
    private void OnTriggerEnter(Collider other)
    {
        // Checks for collision with projectiles that are instantiated through the Player
        if (other.gameObject.GetComponent<Bullet>() != null && other.gameObject.GetComponent<Bullet>().ShotByPlayer != false) {
            m_crateHealth -= 25;
            if(!m_audioSource.isPlaying)
            {
                m_audioSource.Play();
            }
        }
    }
}