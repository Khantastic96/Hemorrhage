/**
 * Created 04/10/2020
 * By: Sharek Khan
 * Last Modified 04/10/2020
 * By: Sharek Khan
 * 
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * PlasmaAmmo moves the Plasma prefab fired every frame by a set amount, and after it is active for x time it will be set as inactive
 * it also has a bool property that can be used to determine if it was fired by the Player or not for hit detection purposes
 */
public class PlasmaAmmo : MonoBehaviour
{
    public float speed = 20f;
    public float duration = 10f;
    public int damage = 5;

    private float m_lifespan;

    private bool m_shotByPlayer;
    public bool ShotByPlayer { get { return m_shotByPlayer; } set { m_shotByPlayer = value; } }
    
    // Start is called before the first frame update
    void Start()
    {
        m_lifespan = duration;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the PlasmaAmmo should be destroyed
        m_lifespan -= Time.deltaTime;
        if(m_lifespan <= 0f)
        {
            // Destroys the PlasmaAmmo
            Destroy(gameObject);
        }
    }
}