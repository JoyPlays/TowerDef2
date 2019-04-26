using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
	[Header("Destination")]
	public GameObject destination;
	[Header("SpawnPoints")]
	public GameObject[] spawnPoint;
	[Header("CastleHitpoints")]
	public int castleHitpoints;
	[Header("WaveCount")]
	public int monsterWaveCount;
	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
