/**
 * Created 26/12/2020
 * By: Omer Farkhand
 * Last Modified 12/20/2020
 * By: Omer Farkhand
 * 
 * Contributors: Omer Farkhand
 */

using UnityEngine;

/*
 * StreptobacillusMoniliformisHead handles the logic, AI and behaviour patterns of head of the StreptobacillusMoniliformis 
 */
public class StreptobacillusMoniliformisHead : Enemy
{
    // Fields
    public int InitialHealth = 100;
    public int damage = 25;

    public AudioSource deathSound;
    public AudioSource effectSound;

    public float m_speed = 25;
    public float Speed { get { return m_speed; } set { m_speed = value; } }
    
    public float m_rotation = 0;
    public float RotationSpeed { get { return m_rotation; } set { m_rotation = value; } }

    public GameObject deathEffect;

    Vector3 dir1;
    Vector3 dir2;
    float distance;

    float timer;

    public GameObject player;
    public GameObject[] body = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        // Initializes the health and damage
        this.Health = InitialHealth;
        this.Damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        //m_speed = 15f;
        Vector3 v1, v2;
        v1 = this.gameObject.transform.position;
        v2 = player.transform.position;
        distance = Vector3.Distance(v1, v2);

        // Rotates the gameobject to look at the player
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(player.transform.position -
            this.gameObject.transform.position), m_rotation * Time.deltaTime);

        Move();

        /*
         * If the player steps out of the specific range, StreptobacillusMonoiliformis increases it's speed to ram the player. 
         */
        
        /*if (distance > 3)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer = " + timer);
            if (timer >= 5.0f)
            {
                Speed *= 0;
                Lunge(2);
                timer = 0;
                Speed = 25;
            }
        } else {
            Move();
            }*/
    }

    // OnKill overrides the OnKill method from the Enemy class to destroy all the parts of the boss's body 
    protected override void OnKill()
    {
        base.OnKill();
        deathSound.Play();
        effectSound.Play();
        GameObject effect = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);

        // agent.enabled = false;
        this.enabled = false;
        Destroy(this.gameObject);
        Destroy(body[0]);
        Destroy(body[1]);
        Destroy(body[2]);
        Destroy(body[3]);
        Destroy(body[4]);
        Destroy(body[5]);
    }

    // Move moves the head forward
    void Move()
    {
        // Move towards the Player
        this.gameObject.transform.position += this.gameObject.transform.forward * Time.deltaTime * m_speed;
    }

    // Lunge multiplies the speed of the boss to ram the Player 
    void Lunge(float multiplier)
    {
        Debug.Log("HeadLunge");

        Debug.Log("Speed = " + m_speed);
       
        m_speed = 15f;
        m_speed *= multiplier;

        //move towards the player
        this.gameObject.transform.position += this.gameObject.transform.forward * Time.deltaTime * m_speed;
    }
}