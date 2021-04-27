/**
 * Created DD/MM/2020
 * By: Sharek Khan
 * Last Modified 04/10/2020
 * By: Aswad Mirza
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using System.Collections.Generic;
using UnityEngine;

/*
 * ObjectPoolingManager pools objects so that instead of destroying and recreating objects they are instead either active or inactive in the scene
 * and save memory and performance for the game at an expense of a longer load time
 * handles the preloading of bullets, smgbullets, shotgun shells and some melee weapon instances
 * Code based on Zenva Studios -COMPLETE COURSE Create a Unity FPS Game in 3 hours - https://www.youtube.com/watch?v=UtlAqRyZrUw
 */
public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    //  Bullets
    public GameObject bulletPrefab;
    public int bulletAmount = 20;
    private List<GameObject> bullets;

    // SMGBullets
    public GameObject smgBulletPrefab;
    public int smgBulletAmount = 100;
    private List<GameObject> smgBullets;


    // ShotgunPellets
    public GameObject shotgunShellPrefab;
    public int shotgunShellAmount = 20;
    private List<GameObject> _shotgunShells;

    // MeleeWeapon
    public GameObject meleeWeaponPrefab;
    public int meleeWeaponAmount = 5;
    private List<GameObject> _meleeWeapons;
    
    // Awake intializes all class properties before Start is called
    void Awake()
    {
        instance = this;
        // Preload bullets

        bullets = new List<GameObject>(bulletAmount);
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            bullets.Add(prefabInstance);
        }

        // Preload smgBullets
        smgBullets = new List<GameObject>(smgBulletAmount);
        for (int i = 0; i < smgBulletAmount; i++)
        {
            GameObject smgPrefabInstance = Instantiate(smgBulletPrefab);
            smgPrefabInstance.transform.SetParent(transform);
            smgPrefabInstance.SetActive(false);
            smgBullets.Add(smgPrefabInstance);
        }

        // Preload ShotgunShells
        _shotgunShells = new List<GameObject>(shotgunShellAmount);
        for (int i = 0; i < smgBulletAmount; i++)
        {
            GameObject shotgunShellPrefabInstance = Instantiate(shotgunShellPrefab);
            shotgunShellPrefabInstance.transform.SetParent(transform);
            shotgunShellPrefabInstance.SetActive(false);
            _shotgunShells.Add(shotgunShellPrefabInstance);
        }

        // Preload MeleeWeapons
        _meleeWeapons = new List<GameObject>(meleeWeaponAmount);
        //fillBulletList(ref _meleeWeapons, meleeWeaponPrefab, meleeWeaponAmount);

        for (int i = 0; i < meleeWeaponAmount; i++)
        {
            GameObject meleeWeaponPrefabInstance = Instantiate(meleeWeaponPrefab);
            meleeWeaponPrefabInstance.transform.SetParent(transform);
            meleeWeaponPrefabInstance.SetActive(false);
            _meleeWeapons.Add(meleeWeaponPrefabInstance);
        }
    }

    // GetBullet object pools a Bullet and creates a new member in the List if there are not enough Bullets preloaded
    public GameObject GetBullet(bool shotByPlayer)
    {
        // Iterates through the List of Bullets
        foreach (GameObject bullet in bullets)
        {
            // Condition checks if indexed Bullet isn't already active in the game
            if (!bullet.activeInHierarchy)
            {
                // Activates the Bullet and states whether it was/wasn't fired from Player
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
                return bullet;
            }
        }
        // Instantiates a new Bullet prefab with transformation coming from the parent object
        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        bullets.Add(prefabInstance);
        return prefabInstance;
    }

    // GetSmgBullet object pools a SMGBullet and creates a new member in the List if there are not enough SMGBullets preloaded
    public GameObject GetSmgBullet(bool shotByPlayer)
    {
        // Iterates through the List of SMGBullets
        foreach (GameObject smgBullet in smgBullets)
        {
            // Condition checks if indexed SMGBullet isn't already active in the game
            if (!smgBullet.activeInHierarchy)
            {
                // Activates the SMGBullet and states whether it was/wasn't fired from Player
                smgBullet.SetActive(true);
                smgBullet.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
                return smgBullet;
            }
        }
        // Instantiates a new SMGBullet prefab with transformation coming from the parent object
        GameObject smgPrefabInstance = Instantiate(smgBulletPrefab);
        smgPrefabInstance.transform.SetParent(transform);
        smgBullets.Add(smgPrefabInstance);
        return smgPrefabInstance;
    }

    // GetShotgunShell object pools a ShotgunShell and creates a new member in the List if there are not enough Bullets preloaded
    // also goes through and sets that each pellet in the shell is shot by the Player
    public GameObject GetShotgunShell(bool shotByPlayer)
    {
        // Iterates through the List of ShotgunShells
        foreach (GameObject shotgunShell in _shotgunShells)
        {
            // Condition checks if indexed ShotgunShell isn't already active in the game
            if (!shotgunShell.activeInHierarchy)
            {
                // Activates the ShotgunShell
                shotgunShell.SetActive(true);
                // Iterates through all the Pellets from the ShotgunShell
                foreach (Transform pellet in shotgunShell.transform)
                {
                    // Grabs the Bullet component from each Pellet and states whether it was/wasn't fired from Player
                    Bullet childBullet = pellet.GetComponent<Bullet>();
                    childBullet.ShotByPlayer = shotByPlayer;
                }
                //shotgunShell.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
                return shotgunShell;
            }
        }
        // Instantiates a new ShotgunShell prefab with transformation coming from the parent object
        GameObject shotgunShellPrefabInstance = Instantiate(shotgunShellPrefab);
        shotgunShellPrefabInstance.transform.SetParent(transform);
        _shotgunShells.Add(shotgunShellPrefabInstance);
        return shotgunShellPrefabInstance;
    }

    // GetMeleeWeapon object pools a MeleeWeapon and creates a new member in the List if there are not enough MeleeWeapons preloaded
    public GameObject GetMeleeWeapon()
    {
        // Iterates through the List of MeleeWeapon projectiles
        foreach (GameObject melee in _meleeWeapons)
        {
            // Condition checks if indexed MeleeWeapon isn't already active in the game
            if (!melee.activeInHierarchy)
            {
                // Activates the MeleeWeapon
                melee.SetActive(true);
                return melee;
            }
        }
        // Instantiates a new MeleeWeapon prefab with transformation coming from the parent object
        GameObject meleeWeaponInstance = Instantiate(meleeWeaponPrefab);
        meleeWeaponPrefab.transform.SetParent(transform);
        _meleeWeapons.Add(meleeWeaponInstance);
        return meleeWeaponInstance;
    }

    // FillBulletList tries to make the job of preloading Bullets easier and to add more scalability to the project
    private void fillBulletList(ref List<GameObject> bulletList, GameObject prefab, int amount)
    {
        // Intializes the List of Bullets with new generic GameObjects
        bulletList = new List<GameObject>(amount);
        for (int i = 0; i < amount; i++)
        {
            // Instantiates a new GameObject prefab with transformation coming from the parent object
            GameObject prefabInstance = Instantiate(prefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            bulletList.Add(prefabInstance);
        }
    }
}