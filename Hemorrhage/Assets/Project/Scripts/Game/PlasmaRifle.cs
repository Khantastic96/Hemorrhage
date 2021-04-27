/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified 01/12/2020
 * By: Omer Farkhand
 * 
 * Contributors: Aswad Mirza, Omer Farkhand, Sharek Khan
 */

using UnityEngine;

/*
 * PlasmaRifle is one of the two main weapons in the game, can fire bullets in quick succession on mouse down or when the mouse is held down
 * also has adjustable firerates for fine tuning gameplay
 */
public class PlasmaRifle : MonoBehaviour
{
    // Player GameObject
    public GameObject playerObject;
    // Rate of fire that is set
    public float initialFireRate = 0.1f;

    public ParticleSystem muzzleFlash;
    // Player script
    private Player m_player;
    // Internal counter for checking if it is time to fire another bullet
    private float m_fireRate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Receives the Player component associated with the Player object
        m_player = playerObject.GetComponent<Player>();
        // Intializes the fire rate for PlasmaRifle
        m_fireRate = initialFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Fire rate is relative to frame time
        m_fireRate -= Time.deltaTime;

        // Condition checks if player fired the gun
        if (Input.GetMouseButton(0)||Input.GetMouseButtonDown(0))
        {
            Fire();
           // muzzleFlash.Play();
        }

        // Condition keeps fire rate from decrementing below 0
        if (m_fireRate <= 0)
        {
            m_fireRate = initialFireRate;
        }
    }

    // Fire handles the functionality of when the PlasmaRifle is activated by the Player and instantiates bullets
    void Fire()
    {
        // Condition checks if Ammo is present/Players not dead/Input within fire rate
        if (m_player.Plasma > 0 && m_player.IsKilled != true && m_fireRate <= 0)
        {
            // Ammo is decremented
            m_player.Plasma--;
            // Creates a new instance of SMGBullet through ObjectPoolingManager
            GameObject smgBulletObject = ObjectPoolingManager.Instance.GetSmgBullet(true);
            // Bullet takes the position of the parent objects position and angle
            smgBulletObject.transform.position = transform.position + transform.forward;
            // Bullet is shot forward with the direction of the parent object
            smgBulletObject.transform.forward = transform.forward;
            muzzleFlash.Play();
        }
    }
}