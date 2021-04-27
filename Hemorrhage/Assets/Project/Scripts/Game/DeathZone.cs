/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified 03/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using UnityEngine;

/*
 * Deathzone handles the logic when the Player falls out of bounds or into a pit
 */
public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter detects all trigger collisions on entry
    private void OnTriggerEnter(Collider other)
    {
        // This code is redundant, Player's HitBox already checks for collision with DeathZone
        if (other.GetComponent<PlayerHitBox>() != false) {
            Debug.LogError("Logic is in DeathZone.cs Line:22.");
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.Health = 0;
            }  
        }
    }
}