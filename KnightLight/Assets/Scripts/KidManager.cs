using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidManager : MonoBehaviour
{
	//Bravery
	[SerializeField] int braveryMeterValue = 100;
	[SerializeField] int braveryDecayRate = 2;
	[SerializeField] int braveryRecovery = 5;
	int braveryMaxValue = 100;
	float decayTimer = .01f;

	//Speed
	[SerializeField] float currentSpeed;
	[SerializeField] float speedWalking = 2f;
	[SerializeField] float speedRunning = 4f;
	
	//Two-Player Flash
	[SerializeField] float flashAOERadius = 2f;
	[SerializeField] bool flashOn = true;
	[SerializeField] float flashTimer = 10f;

	[SerializeField] KnightLightManager KnightLight;


    // Start is called before the first frame update
    void Start()
    {
		currentSpeed = speedWalking;
    }

    // Update is called once per frame
    void Update()
    {
		setCurrentSpeed();
		braveryDecay();
		//flashRecharger();
	}

	//Speed Functions Start
	private void setCurrentSpeed()
	{
		if (KnightLight.isMonsterInKnightAOE())
		{
			currentSpeed = speedRunning;
		}
		else
		{
			currentSpeed = speedWalking;
		}
	}
	//Speed Functions End


	//Bavery Functions Start
	IEnumerator braveryDecay()
	{ 
		if(decayTimer > 0)
		{
			braveryMeterValue -= braveryDecayRate;
			yield return new WaitForSeconds(1);
		}
	}

	public void RecoveryBraveryChunk()
	{
		if(braveryMeterValue < braveryMaxValue)
		{
			if((braveryMeterValue + braveryRecovery) > braveryMaxValue)
			{
				braveryMeterValue += braveryMaxValue - braveryMeterValue;
			}
			else
			{
				braveryMeterValue += braveryRecovery;
			}
		}
		
	}

	public void LoseBravery(int monsterAttack)
	{
		braveryMeterValue -= monsterAttack;
	}

	IEnumerator BraveryRecoveryRate(float recoverySpeed)
	{
		braveryMeterValue += 1;
		yield return new WaitForSeconds(recoverySpeed);
	}
	//Bavery Functions End


	//Flash Attack Functions Start
	public void flashRecharger()
	{
		flashTimer -= Time.deltaTime;
		if (flashTimer < 0)
		{
			flashOn = true;
			flashTimer = 10f;
		}
	}


	//Flash Attack Functions End

	//Light Source Collision Trigger Functions
	private void OnTriggerEnter(Collider LightSource)
	{
		GameObject lightSource = LightSource.gameObject;
		if (LightSource.tag == "KnightLight")
		{
			StartCoroutine(BraveryRecoveryRate(1.5f));
		}
		if(LightSource.tag == "Lamp")
		{
			StartCoroutine(BraveryRecoveryRate(0.1f));
		}
	}

	//Light Source Tirgger Functions End
}
