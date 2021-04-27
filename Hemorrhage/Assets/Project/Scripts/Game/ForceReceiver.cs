/**
 * Created DD/MM/2020
 * By: Sharek Khan
 * Last Modified DD/MM/2020
 * By: Sharek Khan
 * 
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * ForceReceiver applies a kickback force to a GameObject containing the CharacterController Component
 * Code based on Zenva Studios -COMPLETE COURSE Create a Unity FPS Game in 3 hours - https://www.youtube.com/watch?v=UtlAqRyZrUw
 */
public class ForceReceiver : MonoBehaviour
{
    public float deceleration = 5;
    public float mass = 3;

    private Vector3 intensity;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        intensity = Vector3.zero;
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Condition checks if magnitude of the forces intensity is below a float value
        if(intensity.magnitude > 0.2f)
        {
            // Translates the character in a specific vector with the force intensity
            character.Move(intensity * Time.deltaTime);
        }
        intensity = Vector3.Lerp(intensity, Vector3.zero, deceleration * Time.deltaTime);
    }

    // AddForce receives a vector and force amount and defines the intensity relative to mass
    public void AddForce(Vector3 direction, float force)
    {
        intensity += direction.normalized * force / mass;
    }
}
