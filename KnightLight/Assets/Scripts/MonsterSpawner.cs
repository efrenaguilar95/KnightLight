using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, CAPPING, COUNTING };

    [System.Serializable]
    public class Monster
    {
        public string name;
        public Sprite sprite;
    }

    [System.Serializable]
    public class SpawnPoint
    {
        public Transform spawnPoint;
        public bool spawnable;
    }

    //
    public List<Transform> knightAndKid;
    public SpawnPoint[] spawnPoints;
    public Monster[] monsterSprites;
    public Transform monster;

    public float timeBetweenSpawns = 2f;
    public int maxCountForKey = 10;
    public int monsterCap = 6;
    public float spawnCountdown;

    private SpawnState state = SpawnState.COUNTING;
    private bool spawnedKey;
    private float searchCountdown = 1f;
    private int currNumOfMons, monsterCounter;

    // Start is called before the first frame update
    void Start()
    {
        findKnightAndKid();

        if (spawnPoints.Length == 0)
        {
        	Debug.LogError("No spawn points referrenced.");
        }
        if (knightAndKid.Count == 0)
        {
            Debug.LogError("No Knight and Kid referrenced.");
        }
        if (monsterSprites.Length == 0)
        {
            Debug.LogError("No monsters referrenced.");
        }
        isMonsterCap();
        monsterCounter = currNumOfMons;

        spawnCountdown = timeBetweenSpawns;
    }

    private void findKnightAndKid()
    {
        Transform knightTransform = GameObject.FindGameObjectWithTag("KnightLight").transform;
        Transform kidTransform = GameObject.FindGameObjectWithTag("Kid").transform;
        knightAndKid.Add(knightTransform);
        knightAndKid.Add(kidTransform);
    }

    // Update is called once per frame
    void Update()
    {
    	if (spawnCountdown <= 0)
    	{
            if (!isMonsterCap())
            {
                //Move forward and pick a random monster to spawn
                pickMonster();
                monsterCounter++;
                //Debug.Log("monsterCounter = " + monsterCounter);
            }
            spawnCountdown = timeBetweenSpawns;
        }
    	else
    	{
            spawnCountdown -= Time.deltaTime;
        }
        
    }

    bool MonsterIsAlive()
    {
    	searchCountdown -= Time.deltaTime;
    	if (searchCountdown <= 0f)
    	{
    		searchCountdown = 1f;
    		if (GameObject.FindGameObjectWithTag("Monster") == null)
    		{
    			return false;
    		}
    	}
    	return true;
    }

    bool isMonsterCap()
    {
        GameObject[] monsterList = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] toyList = GameObject.FindGameObjectsWithTag("Toy");
        currNumOfMons = monsterList.Length + toyList.Length;
        if (currNumOfMons >= monsterCap)
        {
            return true;
        }
        return false;
    }

    void pickMonster()
    {
        //Debug.Log("Choosing Monster: ");
        state = SpawnState.SPAWNING;
        MonsterManager mon = monster.GetComponent<MonsterManager>();
        if ((monsterCounter >= maxCountForKey) && !spawnedKey)
        {
            mon.toySprite = monsterSprites[0].sprite;
            spawnedKey = true;
        }
        else
        {
            mon.toySprite = monsterSprites[Random.Range(1, monsterSprites.Length)].sprite;
            monster.GetComponent<MonsterManager>().toySprite = mon.toySprite;
        }
        spawnMonster(monster);
    }

    List<Transform> chooseSpawnLocation()
    {
        //Debug.Log("Pick Spawn Location");
        List<Transform> openSP = new List<Transform>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 sp = spawnPoints[i].spawnPoint.position;
            if ((Vector3.Distance(knightAndKid[0].position, sp) > 25) && (Vector3.Distance(knightAndKid[1].position,sp) > 25))
            {
                spawnPoints[i].spawnable = true;
                openSP.Add(spawnPoints[i].spawnPoint);
            }
        }
        return openSP;
    }

    void spawnMonster(Transform monster)
    {
    	//Debug.Log("Spawning monster: " + monster.name);
        Vector3 newSpawnPos, randSpawnPos;
        List<Transform> openSP = chooseSpawnLocation();
    	Transform spawnPoint = openSP[Random.Range(0, openSP.Count)];
        randSpawnPos = Random.insideUnitCircle;
    	newSpawnPos = spawnPoint.position + (randSpawnPos * 3);
        newSpawnPos.y = 0.6f;
    	Instantiate(monster, newSpawnPos, spawnPoint.rotation);
        state = SpawnState.COUNTING;
        spawnCountdown = timeBetweenSpawns;
    }


}
