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
 * VitaCapsule handles all the logic with the VitaCapsule GameObject and how it applies within the game
 */
public class VitaCapsule : MonoBehaviour
{
    public GameObject vitaCapsule;
    public float rotationSpeed = 80f;
    public int health = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vitaCapsule.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
