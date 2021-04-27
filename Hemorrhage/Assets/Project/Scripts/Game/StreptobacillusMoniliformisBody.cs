/**
 * Created 26/10/2020
 * By: Omer Farkhand
 * Last Modified 07/12/2020
 * By: Omer Farkhand
 * 
 * Contributors: Omer Farkhand
 */

using UnityEngine;

/*
 * StreptobacillusMoniliformisBody handles the logic, AI and behaviour patterns of the body parts of the StreptobacillusMoniliformis 
 */
public class StreptobacillusMoniliformisBody : Enemy
{
    public int InitialHealth = 100;
    public int damage = 25;
    public float m_speed;
    public float Speed { get { return m_speed; } set { m_speed = value; } }
    public float m_rotation = 0;
    public float RotationSpeed { get { return m_rotation; } set { m_rotation = value; } }
    float distance = 1;
    
    public GameObject body;


    // Start is called before the first frame update
    void Start()
    {
        this.Health = InitialHealth;
        this.Damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        //m_speed = 16f;
        Vector3 v1, v2;
        v1 = this.gameObject.transform.position;
        v2 = body.transform.position;
        distance = Vector3.Distance(v1, v2);

        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(body.transform.position -
           this.gameObject.transform.position), m_rotation * Time.deltaTime);

        Move();
    }

    // Move moves this GameBbject forward
    void Move()
    {
        // Move towards the Player
        this.gameObject.transform.position += this.gameObject.transform.forward * Time.deltaTime * m_speed;
    }

    // GetAttach retrieves what this GameObject is attached to
    public GameObject getAttach()
    {
        return body;
    }
}
