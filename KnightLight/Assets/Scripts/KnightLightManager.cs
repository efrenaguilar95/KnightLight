using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightLightManager : MonoBehaviour
{

	[SerializeField] float speed = 1000f;
	[SerializeField] float lightAOERadius = 7f;
	[SerializeField] bool monsterInKnightAOE = false;
	[SerializeField] Sprite toySprite;

	//Energy Meter
	[SerializeField] int energyMeterValue = 0;
	[SerializeField] int energyRechargePerMonster = 5;
	int energyMeterMax = 100;

	Animator monster_Anime;
	// Start is called before the first frame update
	void Start()
    {
		
		monster_Anime = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		monsterInKnightAOE = false;
		//Move();
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Monster")
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


   /* private void Move()
	{
		Rigidbody rb = GetComponent<Rigidbody>();

		if (Input.GetKey(KeyCode.A))
		{
			rb.AddForce(Vector3.left * speed);
			monster_Anime.SetBool("isWalkRight", false);
			monster_Anime.SetBool("isWalkLeft", true);
		}
			

		if (Input.GetKey(KeyCode.D))
		{
			rb.AddForce(Vector3.right * speed);
			monster_Anime.SetBool("isWalkLeft", false);
			monster_Anime.SetBool("isWalkRight", true);
		}
			

		if (Input.GetKey(KeyCode.W))
			rb.AddForce(Vector3.up);

		if (Input.GetKey(KeyCode.S))
			rb.AddForce(Vector3.down);
	}
    */
}
