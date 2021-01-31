using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSound : MonoBehaviour
{
    public static WorldSound Instance { get;  private set; }

    public AudioSource correctItem;
    public AudioSource wrongItem;
    public AudioSource dragItem;

    public void Awake()
    {
        Instance = this;
    }

    public void playCorrectItem()
    {
        correctItem.Play();
    }

    public void playWrongItem()
    {
        wrongItem.Play();
    }

    public void playDragItem()
    {
        dragItem.Play();
    }



}
