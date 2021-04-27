/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified 25/11/2020
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using UnityEngine;

/*
 * GunContainer purpose is to hold all the gun models and choose which is active
 * IDEA: Each gun has its own fire functionality, and different bullets that they fire.
 * When the player left clicks, they just call the selected gun's fire script
 * When the player scrolls up and down, they just select a different gun
 * Code based on Brackeys- Weapon Switching - Unity Tutorial https://www.youtube.com/watch?time_continue=69&v=Dn_BUIVdAPg&feature=emb_title
 */
public class GunContainer : MonoBehaviour
{
    private int m_maxWeapons;
    private int m_previousSelectedGun;
    private int m_selectedGun;
    public int SelectedGun { get { return m_selectedGun; } }

    // Selects the deafult weapon on start
    public int DefaultGun = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Starts the selected gun as the default value
        m_selectedGun = DefaultGun;
        // Keeps the max allocation of weapon inventory
        m_maxWeapons = transform.childCount - 1;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        // Temp variable that keeps reference of previously used weapon
        m_previousSelectedGun = m_selectedGun;

        // Changing weapon with the scroll wheel
        // If the user is scrolling up, axis is positive
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            // Condition checks to contain gun iteration within max allocated weapon inventory
            if (m_selectedGun >= m_maxWeapons)
            {
                m_selectedGun = 0;
            }
            // Increments through weapon wheel
            else {
                m_selectedGun++;
            }
        }
        // If the user is scrolling down, axis is negative
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // Condition check if current gun is a first point in inventory, moving back to last point
            if (m_selectedGun == 0)
            {
                m_selectedGun = m_maxWeapons;
            }
            // Decrements through weapon wheel
            else
            {
                m_selectedGun--;
            }
        }
        // Checks if change was made in weapon, selects a new gun
        if (m_previousSelectedGun != m_selectedGun) {
            SelectWeapon();
        }
    }

    // SelectWeapon manages the logic between cycling through and activating/deactivating a gun
    public void SelectWeapon() {
        // For each child transform in the transform in the gunContainer, we are looping through them
        int index = 0;
        foreach (Transform weapon in transform) {
            // Checks if current selected gun matches child element index
            if (m_selectedGun == index){
                // Gun model becomes active
                weapon.gameObject.SetActive(true);
            }
            else {
                // Gun model becomes de-active
                weapon.gameObject.SetActive(false);
            }
            index++;
        }
    }
}