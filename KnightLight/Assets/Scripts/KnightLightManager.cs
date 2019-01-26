using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightLightManager : MonoBehaviour
{

	[SerializeField] float speed = 6f;
	[SerializeField] float lightAOERadius = 7f;
	[SerializeField] bool monsterInKnightAOE = false;

	//Energy Meter
	[SerializeField] int energyMeterValue = 0;
	[SerializeField] int energyRechargePerMonster = 5;
	int energyMeterMax = 100;
	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		monsterInKnightAOE = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "monster")
		{
			isMonsterInKnightAOE();
			collision.gameObject.GetComponent<MonsterManager>().changeToToy();
		}
	}

	public bool isMonsterInKnightAOE()
	{
		monsterInKnightAOE = true;
		return monsterInKnightAOE;
	}

	public void AddToEnergyMeter()
	{
		if(energyMeterValue < energyMeterMax)
		{
			energyMeterValue += energyRechargePerMonster;
		}
	}
}
