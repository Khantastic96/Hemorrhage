/** 
 * Created 19/11/YYYY
 * By: Sharek Khan
 * Last modified 02/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * Destructible handles all the logic and behaviour around the destructible environment objects
 */
public class Destructible : MonoBehaviour
{
    private AudioSource m_audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.name == "CholesterolCrate(Broken)")
        {
            m_audioSource = GameObject.FindGameObjectWithTag("CholesterolCrate").GetComponent<AudioSource>();
        }
        else if(this.gameObject.name == "Myoviridae(Killed)")
        {
            m_audioSource = GameObject.FindGameObjectWithTag("Myoviridae").GetComponent<AudioSource>();
        }
        else if(this.gameObject.name == "CancerCell(Killed)")
        {
            m_audioSource = GameObject.FindGameObjectWithTag("CancerCell").GetComponent<AudioSource>();
        }
        //if(!m_audioSource.isPlaying)
        //{
            // m_audioSource.Play();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerExit detects all trigger collisions on exit
    void OnTriggerExit(Collider collider)
    {
        // Deletes object one it's outside of Players proximity
        if (collider.gameObject.tag == "AIDetection")
        {
            Debug.Log(this.gameObject.name + " Destroyed.");
            Destroy(this.gameObject);
        }
    }
}
