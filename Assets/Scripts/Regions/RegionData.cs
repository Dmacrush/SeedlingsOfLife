using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionData : MonoBehaviour
{
    public int maxAmountEnemies = 4;
    public string BattleScene;
    public List<GameObject> possibleEnemies = new List<GameObject>();

    public int maxAmountPlayers = 4;
    
    public List<GameObject> possiblePlayers = new List<GameObject>();
}
