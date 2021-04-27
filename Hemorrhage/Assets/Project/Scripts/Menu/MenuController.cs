/**
 * Created DD/MM/2020
 * By: Sharek Khan
 * Last modified 10/12/2020
 * By: Sharek Khan
 * 
 * Contributors: Sharek Khan
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * MenuController handles the logic behind the MainMenu scene
 * Code based on
 * Zenva Studios -COMPLETE COURSE Create a Unity FPS Game in 3 hours - https://www.youtube.com/watch?v=UtlAqRyZrUw
 */
public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // PlayGame transitions to Level01 scene
    public void PlayGame()
    {
        Debug.Log("Loading Level 1...");
        SceneManager.LoadScene("Level01");
    }

    // StartCredits transitioins to Credits scene
    public void StartCredits()
    {
        Debug.Log("Loading Credits...");
        SceneManager.LoadScene("Credits");
    }

    // QuitGame quits the game though either Editor/Application depending on how on instance running
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        // Quit the game through application...
        Application.Quit();
        // Quit the game through editor...
         UnityEditor.EditorApplication.isPlaying = false;
    }
}