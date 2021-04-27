/** 
 * Created 25/11/YYYY
 * By: Sharek Khan
 * Last modified 03/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Sharek Khan
 */

using UnityEngine;
using UnityEngine.UI;

/*
 * PlayerUI handles all the logic and functionality for the UI for the core gameplay, displays game critical information
 */
public class PlayerUI : MonoBehaviour
{
    private Player player = null;
    private GunContainer gunContainer = null;
    // private float gameInfoTextTimer = 0.0f;

    [Header("UI")]
    public Text gameInfoText = null;
    public Text armourText = null;
    public Text healthText = null;
    public Image playerImage = null;
    public Text plasmaText = null;
    public Image weaponImage = null;

    [Header("Sprites")]
    public Sprite playerDefault = null;
    public Sprite playerMinor = null;
    public Sprite playerSome = null;
    public Sprite playerPlenty = null;
    public Sprite playerMajor = null;
    public Sprite playerDefeated = null;
    public Sprite plasmaRifle = null;
    public Sprite plasmaShotgun = null;

    // Start is called before the first frame update
    private void Start()
    {
        // Gets the Player script from the Player objects component
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // Gets the GunContainer scripts from the component of Player objects children
        gunContainer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GunContainer>();
        // gameInfoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Updates the Game Info Text on the UI
        gameInfoText.text = ActivityLog.ReadRecentActivity();

        // Updates the Player Armour on the UI
        armourText.text = player.Armour + "%";
        // Updates the Player Health on the UI
        healthText.text = player.Health + "%";
        
        // Updates the Player Image on the UI
        if (player.Health >= 100)
        {
            // Show Default Player Image
            playerImage.sprite = playerDefault;
        }
        else if(player.Health >= 75 && player.Health < 100)
        {
            // Show Minor Player Degradation
            playerImage.sprite = playerMinor;
        }
        else if(player.Health >= 50 && player.Health < 75)
        {
            // Show Some Player Degradation
            playerImage.sprite = playerSome;
        }
        else if(player.Health >= 25 && player.Health < 50)
        {
            // Show Plenty Player Degradation
            playerImage.sprite = playerPlenty;
        }
        else if(player.Health > 0 && player.Health < 25)
        {
            // Show Major Player Degradation
            playerImage.sprite = playerMajor;
        }
        else
        {
            // Show Defeated/Killed Player Image
            playerImage.sprite = playerDefeated;
        }
        
        // Updates the Weapon Plasma on the UI
        plasmaText.text = player.Plasma + "%";
        
        // Updates the Weapon sprite on the UI
        if(gunContainer.SelectedGun == 0)
        {
            // Display Plasma Rifle
            weaponImage.sprite = plasmaRifle;
        }
        else if (gunContainer.SelectedGun == 1)
        {
            // Display Plasma Shotgun
            weaponImage.sprite = plasmaShotgun;
        }
    }
}