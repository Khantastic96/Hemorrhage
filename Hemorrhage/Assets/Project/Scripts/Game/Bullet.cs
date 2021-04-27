/**
 * Created DD/MM/YYYY
 * By: Sharek Khan
 * Last Modified DD/MM/YYYY
 * By: [Insert Name]
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using UnityEngine;

/*
 * Bullet moves the Bullet prefab fired every frame by a set amount, and after it is active for x time it will be set as inactive
 * it also has a bool property that can be used to determine if it was fired by the player or not for hit detection purposes
 * Code based on Zenva Studios -COMPLETE COURSE Create a Unity FPS Game in 3 hours - https://www.youtube.com/watch?v=UtlAqRyZrUw
 */
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeDuration = 10f;
    public int damage = 5;

    private float lifeTimer;

    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }

    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the Bullet move
        transform.position += transform.forward * speed * Time.deltaTime;

        // Check if the Bullet should be destroyed
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}