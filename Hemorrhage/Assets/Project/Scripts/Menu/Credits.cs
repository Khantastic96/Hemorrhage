/** 
 * Created 03/12/2020
 * By: Omer Farkhand
 * Last modified 12/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Omer Farkhand, Sharek Khan
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Credit script handles the logic behind Credit sequence
 */
public class Credits : MonoBehaviour
{
    public float scrollSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection(0, 10, 0);
        pos += localVectorUp * scrollSpeed * Time.deltaTime;
        transform.position = pos;

        // Credits stop if interrupted by User input
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            OnPress();
        }
    }

    // OnTriggerExit detects all trigger collisions on exit
    void OnTriggerExit(Collider collider)
    {
        // Condition check is the colliding object is a Canvas
        if (collider.gameObject.name == "Canvas")
        {
            // Stops the scroll sequence and sets off the timer
            scrollSpeed = 0;
            StartCoroutine(CreditsTimer());
        }
    }

    // OnPress transitions to the MainMenu scene
    void OnPress()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }

    // CreditsTimer sets up the timer for Credits
    IEnumerator CreditsTimer()
    {
        yield return new WaitForSeconds(5);
        OnPress();
    }
}