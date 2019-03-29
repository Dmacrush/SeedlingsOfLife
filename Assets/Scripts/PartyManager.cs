using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
	public List<OwnedSeedlings> ownedSeedlings = new List<OwnedSeedlings>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	[System.Serializable]
	public class OwnedSeedlings
	{
		public BaseStats seedling;
		public int Level;
		public List<BaseAttack> moves = new List<BaseAttack>();
	}
}
