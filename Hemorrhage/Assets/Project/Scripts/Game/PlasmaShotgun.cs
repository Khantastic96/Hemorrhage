/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified 02/12/2020
 * By: Aswad Mirza
 * 
 * Contributors: Aswad Mirza, Omer Farkhand, Sharek Khan
 */

using UnityEngine;

/*
 * PlasmaShotgun is one of the two main weapons of the game, fires a shotgun shell which fires several pellets which was meant to be good for single target]
 */
public class PlasmaShotgun : MonoBehaviour
{
    public GameObject playerObject;
    public float initialFireRate = 0f;
    public float delayFireRate = 2.5f;

    public ParticleSystem muzzleFlash1;
    public ParticleSystem muzzleFlash2;

    private Player m_player;
    private float m_fireRate;

    // Start is called before the first frame update
    void Start()
    {
        // Receives the Player component associated with the Player object
        m_player = playerObject.GetComponent<Player>();
        // Intializes the fire rate for Shotgun
        m_fireRate = initialFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Decrements fire rate value if above zero, every frame
        if (m_fireRate > 0f)
        {
            m_fireRate -= Time.deltaTime;
        }
        // Condition checks if player fired the gun
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    // Fire handles the functionality of when the PlasmaShotgun is activated by the Player and instantiates bullets
    void Fire()
    {
        // Condition checks if Ammo is present/Players not dead/Input not within fire rate
        if (m_player.Plasma > 0 && m_player.IsKilled != true && m_fireRate <= 0f)
        {
            // Ammo is decremented
            m_player.Plasma -= 9;
            // Creates a new instance of ShotgunShell through ObjectPoolingManager
            GameObject shotgunShellObject = ObjectPoolingManager.Instance.GetShotgunShell(true);
            // ShotgunShell takes the position of the parent objects position and angle
            shotgunShellObject.transform.position = transform.position + transform.forward;
            // ShotgunShell is shot forward with the direction of the parent object
            shotgunShellObject.transform.forward = transform.forward;

            muzzleFlash1.Play();
            muzzleFlash2.Play();
        }

        // If PlasmaShotgun fired, delay the next shot
        if (m_fireRate <= 0f)
        {
            m_fireRate = delayFireRate;
        }
    }
}