/**
 * Created DD/MM/2020
 * By: Sharek Khan
 * Last Modified 25/11/2020
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using UnityEngine;

/*
 * Player handles all the logic, functionality and behaviour patterns of the Player GameObject
 * Code based on Zenva Studios -COMPLETE COURSE Create a Unity FPS Game in 3 hours - https://www.youtube.com/watch?v=UtlAqRyZrUw
 */
public class Player : MonoBehaviour
{
    // Plasma regeneration properties
    private float m_plasmaRegenTimer = 0f;
    private int m_plasmaRegenTrigger = 3;

    // Fields visible to Unity Inpsector for tuning
    [Header("Visuals")]
    public Camera playerCamera;
    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialArmour = 0;
    public int initialPlasma = 100;
    public float initialRegenTimer = 5f;
    // Having less Plasma then this amount causes the player to slowly restore Plasma/plasma
    public int initialRegenTrigger = 3;

    // Player gameplay properties
    private int m_health;
    public int Health { get { return m_health; } set { m_health = value; } }
    private int m_plasma;
    public int Plasma { get { return m_plasma; }  set { m_plasma = value; } }
    private int m_armour;
    public int Armour { get { return m_armour; } set { m_armour = value; } }
    // Player state properties
    private bool m_isKilled;
    public bool IsKilled { get { return m_isKilled; } }
    // Player collision properties
    private bool m_isHurt;
    public bool IsHurt { get { return m_isHurt; } set { m_isHurt = value; } }
    private bool m_isArmourEquipped;
    public bool IsArmourEquipped { get { return m_isArmourEquipped; } set { m_isArmourEquipped = value; } }

    // Start is called before the first frame update
    void Start()
    {
        m_health = initialHealth;
        m_plasma = initialPlasma;
        m_armour = initialArmour;
        m_isKilled = false;
        m_isHurt = false;
        m_isArmourEquipped = false; 
        m_plasmaRegenTimer = initialRegenTimer;
        m_plasmaRegenTrigger = initialRegenTrigger; 
    }

    // Update is called once per frame
    void Update()
    {
        CheckRegen();
        // Validates Health value from being negative
        if (m_health <= 0)
        {
            m_health = 0;
            // Checks if Player isn't already dead
            if (m_isKilled == false)
            {
                // Officially declares the Player dead
                m_isKilled = true;
                // Performs the procedure that'll happen at death
                OnKill();
            }
        }
        // Validates Plasma value from being negative
        if (m_plasma <= 0) {
            m_plasma = 0;
        }
        // Validates Armour value from being negative
        if (m_armour <= 0)
        {
            m_armour = 0;
        }
    }

    // [Describe the function of this method]
    private void CheckRegen() {
        if (m_plasma < m_plasmaRegenTrigger)
        {
            m_plasmaRegenTimer -= Time.deltaTime;
            if (m_plasmaRegenTimer <= 0) {
                m_plasma++;
                m_plasmaRegenTimer = initialRegenTimer;
            }
        }
        else
        {
            m_plasmaRegenTimer = initialRegenTimer;
        }

    }

    // [Describe the function of this method]
    private void OnKill()
    {
        // Disabled due to bricking the game through different GameStates
        // GetComponent<CharacterController>().enabled = false;
        // GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
    }
}