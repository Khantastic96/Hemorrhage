/**
 * Created 26/10/YYYY
 * By: Sharek Khan
 * Last Modified 03/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using System.Collections;
using UnityEngine;

/*
 * PlayerHitBox handles all the collision logic for the Player, with interactables, environment, enemies...
 */
public class PlayerHitBox : MonoBehaviour
{
    private GameObject m_playerObject;
    private Player m_player;

    public float knockbackForce = 20f;
    public float hurtDuration = 0.5f;

    AudioSource m_audio;

    public AudioClip vitaCapsuleClip;
    public AudioClip armorClip;
    float volume;

    // Start is called before the first frame update
    void Start()
    {
        m_playerObject = GameObject.FindGameObjectWithTag("Player");
        m_player = m_playerObject.GetComponent<Player>();
        m_audio = m_player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check for trigger collisions on enter
    void OnTriggerEnter(Collider collider)
    {
        volume = Random.Range(5,10);
        // Interactables
        // If Player collides with a VitaCapsule
        if (collider.GetComponent<VitaCapsule>() != null)
        {
            ActivityLog.AddActivity("PICKED UP A VITA CAPSULE!");
            // Collect health from VitaCapsule
            VitaCapsule vitaCapsule = collider.gameObject.GetComponent<VitaCapsule>();
            m_player.Health += vitaCapsule.health;

            if (m_audio != null && vitaCapsuleClip!=null) {
                m_audio.PlayOneShot(vitaCapsuleClip, volume);
            }
            Destroy(vitaCapsule.gameObject);
        }
        // If Player collides with a CellAmour Object
        if (collider.GetComponent<CellArmour>() != null)
        {
            ActivityLog.AddActivity("PICKED UP A CELL ARMOUR!");
            // Player is now equipped with armour
            m_player.IsArmourEquipped = true;
            // Collect armour
            CellArmour cellArmour = collider.gameObject.GetComponent<CellArmour>();
            m_player.Armour += cellArmour.armourPoint;
            if (m_audio != null && armorClip != null)
            {
                m_audio.PlayOneShot(armorClip, volume);
            }
            Destroy(cellArmour.gameObject);
        }

        // Environment
        // Lets the Player die if they fall to the ground ---- 2020/07/28
        // *NOTE WILL BE ALTERED
        if (collider.GetComponent<DeathZone>()!= null)
        {
            Debug.LogError("Logic is in PlayerHitBox.cs Line:76.");
            m_player.Health = 0;
        }

        // changes the scene based on the goals scene index
        /*
        if (collider.GetComponent<Goal>() != null) {
            SceneManager.LoadScene(collider.GetComponent<Goal>().SceneIndex);
        }
        */
        // Enemies
        // Checks if Player hasn't already taken damage
        if (m_player.IsHurt == false)
        {
            GameObject hazard = null;
            // Checks if Player was damaged by melee attack
            if (collider.GetComponent<Enemy>() != null)
            {
                // Touching enemies
                Enemy enemy = collider.GetComponent<Enemy>();
                // Checks if Enemy isn't already dead
                if (enemy.IsKilled == false)
                {
                    hazard = enemy.gameObject;
                    // Checks if Player is equipped with CellArmour
                    if (m_player.IsArmourEquipped != false)
                    {
                        // Decrements Armour
                        m_player.Armour -= enemy.Damage;
                        // Decrements Health by half because of armour padding
                        m_player.Health -= enemy.Damage / 2;
                    }
                    // Player isn't equipped with CellArmour
                    else
                    {
                        // Decrements Health
                        m_player.Health -= enemy.Damage;
                    }
                }
            }
            // Checks if Player was damaged by an Enemy through projectile
            else if (collider.GetComponent<Bullet>() != null)
            {
                // Shot by enemies
                Bullet bullet = collider.GetComponent<Bullet>();
                // Checks if Enemy isn't already dead
                if (bullet.ShotByPlayer == false)
                {
                    hazard = bullet.gameObject;
                    // Checks if Player is equipped with CellArmour
                    if (m_player.IsArmourEquipped != false)
                    {
                        // Decrements Armour
                        m_player.Armour -= bullet.damage;
                        // Decrements Health by half because of armour padding
                        m_player.Health -= bullet.damage / 2;
                    }
                    // Player isn't equipped with CellArmour
                    else
                    {// Decrements Health
                        m_player.Health -= bullet.damage;
                    }
                    bullet.gameObject.SetActive(false);
                }
            }

            // Knockback
            // Checks if knockback is required on attack
            if (hazard != null)
            {
                m_player.IsHurt = true;
                // Perform the knockback effect
                Vector3 hurtDirection = (m_playerObject.transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                m_playerObject.GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);
                StartCoroutine(HurtRoutine());
            } 
        }
    }

    // HurtRoutine schedules a duration for how long a Player should be damaged
    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);
        m_player.IsHurt = false;
    }
}