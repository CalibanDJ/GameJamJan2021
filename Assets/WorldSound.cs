using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSound : MonoBehaviour
{


    public AudioClip airplane;
    public AudioClip crow;
    public AudioClip music;

    private AudioSource sourceAirplane { get { return GetComponent<AudioSource>(); } }
    private AudioSource sourceCrow { get { return GetComponent<AudioSource>(); } }
    private AudioSource sourceMusic { get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        sourceAirplane.clip = airplane;
        sourceAirplane.playOnAwake = false;

        gameObject.AddComponent<AudioSource>();
        sourceCrow.clip = crow;
        sourceCrow.playOnAwake = true;

        gameObject.AddComponent<AudioSource>();
        sourceMusic.clip = music;
        sourceMusic.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ClientGenerator.Instance.getRemainingTimeOfGame() % 40 == 0)
        {
            sourceAirplane.PlayOneShot(airplane);
        }
    }
}
