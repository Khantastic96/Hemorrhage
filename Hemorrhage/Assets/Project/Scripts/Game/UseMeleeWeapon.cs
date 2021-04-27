/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified 25/11/2020
 * By: Aswad Mirza
 * 
 * Contributors: Aswad Mirza
 */

using UnityEngine;

/*
 * UseMeleeWeapon for creating a melee weapon instance and launching it forward and making sounds, has an adujustable fire rate and force]
 */
public class UseMeleeWeapon : MonoBehaviour
{
    public GameObject meleeWeapon;
    public AudioClip meleeSounds;

    private AudioSource source;

    // Logic for fireRate
    public float initialFireRate = 0f;
    public float delayFireRate = 1f;
    public Vector3 MeleeDirection =new Vector3(0,100,5000);
    private float m_fireRate;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        m_fireRate = initialFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Reducing the firerate by delta time
        if (m_fireRate >= 0) {
            m_fireRate -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(1) && m_fireRate <=0) {
            float volume = Random.Range(5,10);
            source.PlayOneShot(meleeSounds,volume);

           // Vector3 weaponRotation = transform.rotation.eulerAngles;
            //weaponRotation.x = 90;

            GameObject weapon = Instantiate(meleeWeapon, transform.position, Quaternion.Euler(transform.rotation.eulerAngles));
            /*
            GameObject weapon =ObjectPoolingManager.Instance.GetMeleeWeapon();
            weapon.transform.position = transform.position;
            weapon.transform.rotation = Quaternion.Euler(weaponRotation);
            */
            Rigidbody rb = weapon.GetComponent<Rigidbody>();

            //Launch that wep with the might of zeus
            //rb.AddRelativeForce(0, 2000, 0);
            rb.AddRelativeForce(MeleeDirection);
            //fireRate is equal to the delay fireRate
            m_fireRate = delayFireRate;
        }
    }
}