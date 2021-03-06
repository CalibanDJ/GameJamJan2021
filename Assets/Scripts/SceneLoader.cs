using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string scene;


    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        
    }

    public void PlaySound()
    {
        source.PlayOneShot(sound);
    }

    public void loadScene()
    {
        PlaySound();
        SceneManager.LoadScene(scene);        
    }
}
