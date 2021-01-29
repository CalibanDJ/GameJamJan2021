using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemGen;

public class ItemGenerator : MonoBehaviour
{
    private GrowingRNG itemType;
    private GrowingRNG itemCaract;
    // Param pr le nbr de caractéristique

    public ItemGenerator(GrowingRNG typeGen, GrowingRNG caractGen) {
        itemType = typeGen;
        itemCaract = caractGen;
    }

    // WIP
    public void generate() {
        int type = itemType.generateNumber();
        int carac = caractGen.generateNumber();

        // Retourner l'obet correspondant
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
