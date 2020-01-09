using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// The code that the music is attached to to play music and preserve music through scenes
/// </summary>

public class MusicController : MonoBehaviour
{ }/*
    [SerializeField] string[] relatedScenes;
    
        void Awake()
        {
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                foreach (GameObject i in FindObjectsOfType(GetType()))
                {
                    if (this.gameObject != i) { Destroy(gameObject); }
                }
            }
            else { DontDestroyOnLoad(gameObject); }
        }
        */
/*
    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1) { Destroy(this.gameObject); }
        else DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Debug.Log("more than one music object");
            foreach (GameObject i in FindObjectsOfType(GetType()))
            {
                if (this.gameObject == i) { Destroy(this.gameObject); }
            }
        }
    }
    void Start()
    {
        if (!relatedScenes.Contains(SceneManager.GetActiveScene().name)) { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update() { if (!relatedScenes.Contains(SceneManager.GetActiveScene().name)) { Destroy(gameObject); } }//destroy if it's not in the right scene
}*/
/*
 * 1. The music attached to the object will start on scene
 * 
 * 2. If there was 
 */