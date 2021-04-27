/** 
 * Created 21/10/2020
 * By: Sharek Khan
 * Last modified 03/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using UnityEngine;

/*
 * CancerCell handles all the logic, AI and behaviour patterns of the CancerCell GameObject
 */
public class CancerCell : Enemy
{
    private GameObject m_player;
    private Vector3 m_initialPos;
    private Quaternion m_initialRot;
    private int m_speed;
    private int m_chaseSpeed;
    private int m_rotSpeed = 180;
    private string m_enemyState;

    public int InitialHealth = 10;
    public float shootingDelta = 1f;
    public float shootingDist = 10f;
    public GameObject weapon01;
    public GameObject weapon02;
    public int speed = 2;
    public int chaseSpeed = 3;

    public GameObject cancerCellKilled;
    
    private AudioSource m_source;
    // Fire rate
    public float m_fireRate = 1f;
    private float m_initialFireRate = 0f;
    // Grab variables
    public float grabDistance = 3f;
    public float grabSpeed = 12;
    private Vector3 m_lastPlayerLocation = Vector3.zero;

    Rigidbody m_rigidbody;
    bool originalKinematicSetting = false;

    // Start is called before the first frame update
    void Start()
    {
        m_initialPos = transform.position;
        m_initialRot = transform.rotation;
        m_speed = speed;
        m_chaseSpeed = chaseSpeed;
        m_enemyState = "IDLE";
        // m_enemyState = "ATTACK";

        this.Health = InitialHealth;
        m_initialFireRate = m_fireRate;
        // Retrieves the Player through the active Player GameObject
        m_player = GameObject.FindGameObjectWithTag("Player");

        m_source = GetComponent<AudioSource>();

        m_rigidbody = GetComponent<Rigidbody>();
        if (m_rigidbody != null) {
            originalKinematicSetting = m_rigidbody.isKinematic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Grabbed();
        // Check Enemy behaviour state
        if (m_enemyState == "IDLE")
        {
            OnIdle();
        }
        else if (m_enemyState == "ATTACK")
        {
            LookAtPlayer();
            m_fireRate -= Time.deltaTime;

            if (m_fireRate <= 0) {
                OnAttack();
            }
        }
        if (m_fireRate <= 0) {
            m_fireRate = m_initialFireRate;
        }
    }

    // OnTriggerEnter detects all trigger collisions on entry
    void OnTriggerEnter(Collider collider)
    {
        // Checks if Enemy has entered the Players vicinity bubble
        if (collider.gameObject.tag == "AIDetection")
        {
            Debug.Log("CancerCell is inside Player vicinty.");
            // Switch enemy state to attack
            m_enemyState = "ATTACK";
        }

        // Because we are using OnTriggerEnter in this class, it needs to be updated
        if (collider.GetComponent<Bullet>() != null)
        {
            // Calls an instance of the PlasmaAmmo script component
            Bullet bullet = collider.GetComponent<Bullet>();
            // Checks if PlasmaAmmo was shot by Player
            if (bullet.ShotByPlayer == true)
            {
                // Enemy takes damage
                TakeDamage(bullet.damage);
                // PlasmaAmmo will be deactivated once contact is made
                bullet.gameObject.SetActive(false);

            }
        }
    }

    // OnTriggerExit detects all trigger collisions on exit
    void OnTriggerExit(Collider collider)
    {
        // Checks if Enemy has exited the Players vicinity bubble
        if (collider.gameObject.tag == "AIDetection")
        {
            Debug.Log("CancerCell is outside Player vicinty.");
            // Reset enemy position
            // transform.position = m_initialPos;
            transform.rotation = m_initialRot;
            // Switch enemy state to idle
            m_enemyState = "IDLE";
        }
    }

    // OnIdle determines CancerCell behaviour patterns when not near Player
    void OnIdle()
    {
        // Translate enemy back and forth, while rotating around
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
        if (transform.position.z >= (m_initialPos.z + 10))
        {
            // Rotate Enemy
            transform.Rotate(Vector3.up, m_rotSpeed * Time.deltaTime);
        }
        else if (transform.position.z <= (m_initialPos.z - 10))
        {
            // Rotate Enemy
            transform.Rotate(Vector3.up, m_rotSpeed * Time.deltaTime);
        }
    }

    // LookAtPlayer seperates the function to look at the player and function to attack [Added by: Aswad Mirza]
    void LookAtPlayer() {
        // Rotate in Player direction
        Vector3 playerDir = m_player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(playerDir);
        // Chase Player
        transform.position = CalculateDirection(transform.position, m_player.transform.position, m_chaseSpeed);
    }

    // OnAttack determines CancerCell behaviour patterns when entering Player vicinity
    void OnAttack()
    {
        //LookAtPlayer();
        // StartCoroutine(fireBullet(m_fireRate));
        // Attack Player by shooting projectiles at their direction
        // Instantiate an Ammo type GameObject
        GameObject plasmaAmmo = ObjectPoolingManager.Instance.GetSmgBullet(false);
        // Set the initial position of the Ammo object to be from PlasmaRifle
        plasmaAmmo.transform.position = weapon01.transform.position;
        // Translate the Ammo object towards the Player
        plasmaAmmo.transform.forward = (m_player.transform.position - weapon01.transform.position).normalized;
    }

    /*
    IEnumerator fireBullet(float firedelay) {
        yield return new WaitForSeconds(firedelay);
        // Attack Player by shooting projectiles at their direction
        // Instantiate an Ammo type GameObject
        GameObject plasmaAmmo = ObjectPoolingManager.Instance.GetSmgBullet(false);
        // Set the initial position of the Ammo object to be from PlasmaRifle
        plasmaAmmo.transform.position = weapon.transform.position;
        // Translate the Ammo object towards the Player
        plasmaAmmo.transform.forward = (m_player.transform.position - weapon.transform.position).normalized;
    }
    */

    // OnKill handles the logic for when the CancerCell dies, in this case it calls the base class function and instantiates 
    // the killed version of this prefab and destroys this game object
    protected override void OnKill()
    {
        base.OnKill();
        // Kill State
        Debug.Log("CancerCell is killed.");
        Instantiate(cancerCellKilled, transform.position, transform.rotation);
        Destroy(this.gameObject);
        
        // gameObject.GetComponent<Rigidbody>().isKinematic = false;

        // if (!m_source.isPlaying) {
        //     m_source.Play();
        //}

        //this.enabled = false;
        //Destroy(this.gameObject);
        //transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    // Grabbed handles the behaviour of the CancerCell reacts when grabbed by the Player through MeleeWeapon [Added By: Aswad Mirza]
    protected override void Grabbed()
    {
        if (IsGrabbed)
        {
            Debug.Log("Grabbed");
            //disable the navmesh agent
            // Condition checks if CancerCell RigidBody component is active
            if (m_rigidbody != null)
            {

                m_rigidbody.isKinematic = true;
            }
            // Condition checks if Players last position is relative to stationary movement
            if (m_lastPlayerLocation.Equals(Vector3.zero))
            {
                Debug.Log("Going to the last Player location");
                m_lastPlayerLocation = m_player.transform.position;
            }
            // Condition checks the delta between the CancerCell and Player distance is greater than the grab distance
            if (Vector3.Distance(transform.position, m_lastPlayerLocation) > grabDistance)
            {

                transform.position = CalculateDirection(transform.position, m_lastPlayerLocation, grabSpeed);
            }
            // Condition checks the delta between the CanerCell and Player distance is less than or equal to the grab distance
            else
            {
                // CancerCell is no longer grabbed and returns to it's previous state (position and physics)
                IsGrabbed = false;
                m_lastPlayerLocation = Vector3.zero;
                if (m_rigidbody != null)
                {
                    m_rigidbody.isKinematic = originalKinematicSetting;
                }
                Debug.Log("Not Grabbed Anymore");
            }
        }
    }
}