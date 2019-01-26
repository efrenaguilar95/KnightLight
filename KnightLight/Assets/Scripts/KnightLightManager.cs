using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightLightManager : MonoBehaviour
{

	[SerializeField] float speed = 6f;
	[SerializeField] float lightAOERadius = 7f;
	[SerializeField] bool monsterInAOE = false;

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
		monsterInAOE = false;

	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "monster")
		{
			monsterInKnightAOE();
		}
	}

	public bool monsterInKnightAOE()
	{
		monsterInAOE = true;
		return monsterInAOE;
	}

	public void AddToEnergyMeter()
	{
		if(energyMeterValue < energyMeterMax)
		{
			energyMeterValue += energyRechargePerMonster;
		}
	}
}
