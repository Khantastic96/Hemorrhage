/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified 25/11/2020
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using UnityEngine;

/*
 * MeleeWeapon is the Key Feature of the game, is used to pull enemies closer to the player or have the player traverse to HookShot objects
 * damages enemies and restores plasma once the player kills an enemy with it, will destroy itself after x time
 */
public class MeleeWeapon : MonoBehaviour
{
    private GameObject m_playerObject;
    private Player m_player;
    private float m_timeAlive = 0f;
    private static bool m_isFired = false;
    private AudioSource m_source;

    public int heal = 10;
    public int damage = 5;
    public int ammoRestore = 10;
    public float speed = 20f;
    public float enemyDistance = 3f;
    public float initialTimeAlive = 0.25f;

    public GameObject collideEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        // isfired = false;
        m_timeAlive = initialTimeAlive;
        m_playerObject = GameObject.Find("Player");
        m_player = m_playerObject.GetComponent<Player>();
        m_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //MoveObject();

        // We let the meleeweaponStay active for x seconds and then destroy it
        if (m_timeAlive <= 0) {
            m_isFired = false;
            Destroy(this.gameObject);
            //gameObject.SetActive(false);
            m_timeAlive = initialTimeAlive;
        }
        m_timeAlive -= Time.deltaTime;
    }

    // OncollisionEnter detects all collisions on entry
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = null;
        // Condition checks if colliding object has a Enemy component and boolean isFired flag is not active
        if (collision.gameObject.GetComponent<Enemy>() != null &&!m_isFired) {
            Debug.Log("Melee grab successful.");
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.IsGrabbed = true;
            enemy.TakeDamage(damage);
            if (enemy.Health <= 0) {
                //m_player.Health += heal;
                m_player.Plasma += ammoRestore;
            }
            // This is to prevent the melee weapon from hitting the enemy for multiple frames
            if (!m_source.isPlaying) {
                // problem is that the object will be destroyed so this doesnt have time to play
                m_source.Play();
            }
            // Spawns the particle effect
            GameObject effect = Instantiate(collideEffect, transform.position, collideEffect.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    // MoveObject is a unused function to have the object move through a script rather then through its Rigidbody
    private void MoveObject()
    {
        // Make the weapon move
        transform.position += transform.forward * speed * Time.deltaTime; 
    }

    // PullObject is a unused script for moving an Enemy directly in front of the Player
    private void pullObject(GameObject enemy) {
        Vector3 distanceFromPlayer = m_playerObject.transform.forward;
       // distanceFromPlayer.z += m_enemyDistance;
        enemy.transform.position = distanceFromPlayer;
    }
}