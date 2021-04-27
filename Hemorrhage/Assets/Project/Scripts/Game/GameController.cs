/**
 * Created DD/MM/2020
 * By: Sharek Khan
 * Last Modified 03/12/2020
 * By: Aswad Mirza
 * 
 * Contributors: Aswad Mirza, Omer Farkhand, Sharek Khan
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * GameController controls the game logic for moving to the next scene, whether the player has won or lost and for clearing the acitivty log
 * after x time
 * Code based on Zenva Studios -COMPLETE COURSE Create a Unity FPS Game in 3 hours - https://www.youtube.com/watch?v=UtlAqRyZrUw
 */
public class GameController : MonoBehaviour
{
    private float m_resetTimer = 3f;
    private bool m_gameOver = false;

    private float resetActivityTimer = 5f;

    [Header("Game")]
    public Player player;
    public GameObject enemyContainer;

    //Removing UI in game controller due to redundancy
    /*
    [Header("UI")]
    public Text healthText;
    public Text ammoText;
    public Text armourText;
    public Text enemyText;
    public Text infoText;
    */

    public int nextLevelIndex = 1;
    public int currentLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
       // infoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       // healthText.text = "Health: " + player.Health;
       // ammoText.text = "Ammo: " + player.Plasma;
       // armourText.text = "Armour: " + player.Armour;
        int aliveEnemies = 0;
        foreach(Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>())
        {
            if(enemy.IsKilled == false)
            {
                aliveEnemies++;
            }
        }
      //  enemyText.text = "Enemies: " + aliveEnemies;
        if(aliveEnemies == 0)
        {
            m_gameOver = true;
            //   infoText.gameObject.SetActive(true);
            //  infoText.text = "You Win!";
            ActivityLog.AddActivity("You Win!");
        }
        if(player.IsKilled == true)
        {
            m_gameOver = true;
            ActivityLog.AddActivity("You Died");
        }
        if(m_gameOver == true)
        {
            m_resetTimer -= Time.deltaTime;
            if(m_resetTimer <= 0)
            {
                if (!player.IsKilled)
                {
                    SceneManager.LoadScene(nextLevelIndex);
                }
                else {
                    SceneManager.LoadScene(currentLevelIndex);
                }
                 
            }
        }

        // Clears the event read for the Player after a few seconds
        if (ActivityLog.ReadRecentActivity() != "") {
            resetActivityTimer -= Time.deltaTime;
            if (resetActivityTimer <= 0) {
                ActivityLog.ClearLog();
                resetActivityTimer = 5f;
            }    
        }
    }
}