using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidManager : MonoBehaviour
{
	//Bravery
	[SerializeField] int braveryMeterValue = 100;
	[SerializeField] int braveryDecayRate = 2;
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
		flashRecharger();
	}

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

	private void braveryDecay()
	{ 
		decayTimer -= Time.deltaTime;
		if(decayTimer < 0)
		{
			braveryMeterValue -= braveryDecayRate;
			decayTimer = .01f;
		}
	}

	public void flashRecharger()
	{
		flashTimer -= Time.deltaTime;
		if(flashTimer < 0)
		{
			flashOn = true;
			flashTimer = 10f;
		}
	}
}
