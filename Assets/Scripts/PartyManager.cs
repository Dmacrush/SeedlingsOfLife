using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<GameObject> seedlingsInParty = new List<GameObject>();

    public int maxPartyMembers;

    private void Awake()
    {
        if(seedlingsInParty.Count < maxPartyMembers)
        {
            //AddSeedling(gameObject);
        }
        else
        {
            return;
        }

        maxPartyMembers = seedlingsInParty.Count;
        
        
    }

    public void AddSeedling (GameObject seedling)
    {
        seedlingsInParty.Add(seedling);
    }

}
