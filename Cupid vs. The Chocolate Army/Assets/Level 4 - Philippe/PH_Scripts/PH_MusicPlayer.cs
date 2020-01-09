using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// The code that the music is attached to to play music and preserve music through scenes
/// </summary>

public class PH_MusicPlayer : MonoBehaviour
{
    [SerializeField] string[] relatedScenes;

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    void Update() { if(!relatedScenes.Contains(SceneManager.GetActiveScene().name)) { Destroy(gameObject); } }//destroy if it's not in the right scene
}
