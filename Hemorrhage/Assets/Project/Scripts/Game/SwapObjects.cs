/**
 * Created DD/MM/YYYY
 * By: Aswad Mirza
 * Last Modified DD/MM/YYYY
 * By: [Insert Name]
 * 
 * Contributors: Aswad Mirza
 */

using UnityEngine;

/*
 * SwapObjecs switches between this game object to another game object, and destroys the original
 * Code based on Brackeys - SHATTER/DESTRUCTION In Unity (Tutorial) https://www.youtube.com/watch?time_continue=69&v=Dn_BUIVdAPg&feature=emb_title
 */
public class SwapObjects : MonoBehaviour
{
    // What we are replacing this gameobject with
    private GameObject _replacement_prefab;

    // The parent of this game object 
    public GameObject Replacement_Object_prefab;

    // Start is called before the first frame update    
    void Start()
    {
        _replacement_prefab = Replacement_Object_prefab;
    }

    // Update is called once per frame
    void Update()
    {
        Swap();
    }

    // Swap will swap the GameObjects
    public void Swap()
    {
        Debug.Log("SWAPPED");
        GameObject replacement_instance = GameObject.Instantiate(_replacement_prefab);

        // sets the replaced gameobjects parent to the parent of the object this script is attached to
        replacement_instance.transform.SetParent(transform.parent);
        Destroy(gameObject);
    }
}