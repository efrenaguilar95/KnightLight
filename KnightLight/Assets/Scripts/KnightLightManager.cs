using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightLightManager : MonoBehaviour
{

	[SerializeField] float speed = 6f;
	[SerializeField] float lightAOERadius = 7f;
	[SerializeField] bool monsterInKnightAOE = false;
	[SerializeField] Sprite toySprite;

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
		Move();
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

	private void Move()
	{
		Rigidbody rb = GetComponent<Rigidbody>();

		if (Input.GetKey(KeyCode.A))
			rb.AddForce(Vector3.left);

		if (Input.GetKey(KeyCode.D))
			rb.AddForce(Vector3.right);

		if (Input.GetKey(KeyCode.W))
			rb.AddForce(Vector3.up);

		if (Input.GetKey(KeyCode.S))
			rb.AddForce(Vector3.down);
	}
}
