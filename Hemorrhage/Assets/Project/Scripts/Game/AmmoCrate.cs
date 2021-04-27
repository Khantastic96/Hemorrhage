/** 
 * Created DD/MM/YYYY
 * By: Sharek Khan
 * Last modified 03/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * AmmoCrate handles the logic for the AmmoCrate GameObject
 * Code based on Zenva Studios -COMPLETE COURSE Create a Unity FPS Game in 3 hours - https://www.youtube.com/watch?v=UtlAqRyZrUw
 */
public class AmmoCrate : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject container;
    public float rotationSpeed = 80f;
    
    [Header("Gameplay")]
    public int ammo = 12;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        container.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
