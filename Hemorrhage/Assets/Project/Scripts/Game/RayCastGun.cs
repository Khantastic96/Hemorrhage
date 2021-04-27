/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified 26/10/2020
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using UnityEngine;

/*
 * RayCastGun was unused logic created for testing on how to fire a gun through raycasting
 * Code is based on Brackeys tutorial https://www.youtube.com/watch?v=THnivyG0Mvo
 */
public class RayCastGun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public Camera PlayerCamera;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot();
        }
    }

    // Shoot is an unused method for firing a raycast from the gun
    void Shoot() {
        RaycastHit hit;

        // Fires a lazer beam from this point that for a set distance that return information about the object hit

        // With out, we are putting all the information of this ray cast into the hit variable

        // Raycast returns true if you actually hit something
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, range)) {
            
            Debug.Log(hit.transform.name);
            ShootingEnemy enemy = hit.transform.GetComponent<ShootingEnemy>();
            if (enemy != null) {
                Debug.Log("Test");
                enemy.Health -= damage;

            }
        }
    }
}