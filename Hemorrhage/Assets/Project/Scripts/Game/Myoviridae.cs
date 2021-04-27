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
 * Myoviridae handles all the logic, AI and behaviour patterns of the Myoviridae GameObject
 */
public class Myoviridae : Enemy
{
    private GameObject m_player;
    private Vector3 m_initialPos;
    private Quaternion m_initialRot;
    public int m_speed = 6;
    public int m_rushSpeed = 9;
    private int m_rotSpeed = 45;
    private string m_enemyState;
    private AudioSource m_source;

    public GameObject myoviridaeKilled;

    public float chargingDelta = 5f;
    public float chargingDistance = 5f;

    public float grabDistance = 3f;
    public float grabSpeed = 120f;
    private Vector3 m_lastPlayerLocation = Vector3.zero;

    //health
    public int initialHealth = 10;

    Rigidbody rigidbody;
    bool originalKinematicSetting =false;

    // Start is called before the first frame update
    void Start()
    {
        // Initializes the original position of Myoviridae
        m_initialPos = transform.position;
        // Initializes the original rotation of Myoviridae
        m_initialRot = transform.rotation;
        // Initializes the inital behaviour state of Myoviridae
        //m_enemyState = "IDLE";
        m_enemyState = "IDLE";

        m_source = GetComponent<AudioSource>();
        // Retrieves the Player script through the active Player GameObject
        m_player = GameObject.FindGameObjectWithTag("Player");

        this.Health = initialHealth;

        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null) {
            originalKinematicSetting = rigidbody.isKinematic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check Enemy behaviour state
        if(m_enemyState == "IDLE")
        {
            OnIdle();
        }
        else if(m_enemyState == "ATTACK")
        {
            OnAttack();
        }
        Grabbed();
    }

    // OnTriggerEnter detects all trigger collisions on entry
    void OnTriggerEnter(Collider collider)
    {
        
        // Checks if Enemy has entered the Players vicinity bubble
        if (collider.gameObject.tag == "AIDetection")
        {
            Debug.Log("Myoviridae is inside Player vicinty.");
            // Switch enemy state to attack
            m_enemyState = "ATTACK";
        }


        // Because we are using ontrigger enter in this class, it needs to be updated
        if (collider.GetComponent<Bullet>() != null)
        {
            // Calls an instance of the PlasmaAmmo script component
            Bullet bullet = collider.GetComponent<Bullet>();
            // Checks if PlasmaAmmo was shot by Player
            if (bullet.ShotByPlayer == true)
            {
                // Queue audio for enemy taking damage
                if (!m_source.isPlaying && this.Health > 0)
                {
                    m_source.Play();
                }
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
            Debug.Log("Myoviridae is outside Player vicinty.");
            // Reset enemy position
            // transform.position = m_initialPos;
            transform.rotation = m_initialRot;
            //  Switch enemy state to idle
            m_enemyState = "IDLE";
        }
    }

    // Enemies Behaviour Patterns when not near Player
    void OnIdle()
    {
        // Translate enemy back and forth, while rotating around
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
        if(transform.position.z >= (m_initialPos.z + 10))
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

    // Enemies Behaviour Patterns when enter Player vicinity
    void OnAttack()
    {
        // Rotate in Player direction
        Vector3 playerDir = m_player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(playerDir);
        // Attack Player by rushing them with melee damage
        transform.position = CalculateDirection(transform.position, m_player.transform.position, m_rushSpeed);
    }


    // OnKill handles the logic for when the Myoviridae dies, in this case it calls the base class function and instantiates 
    // the killed version of this prefab and destroys this game object
    protected override void OnKill()
    {
        base.OnKill();
        // Kill State
        Debug.Log("Myoviridae is killed.");
        Instantiate(myoviridaeKilled, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    // Grabbed handles the behaviour of the CancerCell reacts when grabbed by the Player through MeleeWeapon [Added By: Aswad Mirza]
    protected override void Grabbed()
    {
       
        if (IsGrabbed)
        {
            Debug.Log("Grabbed");
            // Get the initial Kinematic value
            if (rigidbody != null) {
                
                rigidbody.isKinematic = true;
            }

            if (m_lastPlayerLocation.Equals(Vector3.zero))
            {
                Debug.Log("Going to the last Player location");
                m_lastPlayerLocation = m_player.transform.position;
            }

            if (Vector3.Distance(transform.position, m_lastPlayerLocation) > grabDistance)
            {

                transform.position = CalculateDirection(transform.position, m_lastPlayerLocation, grabSpeed);
            }
            else
            {
                IsGrabbed = false;
                m_lastPlayerLocation = Vector3.zero;
                if (rigidbody != null) {
                    rigidbody.isKinematic = originalKinematicSetting;
                }
                Debug.Log("Not Grabbed Anymore");
            }
        }
    }
}