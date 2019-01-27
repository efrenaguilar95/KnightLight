using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class KidManager : MonoBehaviour
{
    public enum KidState { ATTACKING , RUNNING , CRYING }

    //Bravery
	[SerializeField] int braveryMeterValue;
	[SerializeField] int braveryDecayRate = 2;
	[SerializeField] int braveryRecovery = 5;
	[SerializeField] Slider braveryBarUI;
	Coroutine decay;
	Coroutine heal;
	int braveryMaxValue = 100;
	float decayTimer = .01f;

    //Speed
    [SerializeField] float speed = 3.5f;
    [SerializeField] float currentSpeed;
	[SerializeField] float speedWalking = 2f;
	[SerializeField] float speedRunning = 4f;
	
	//Two-Player Flash
	[SerializeField] float flashAOERadius = 2f;
	[SerializeField] bool flashOn = true;
	[SerializeField] float flashTimer = 10f;
    public float timeBetweenAttacks = 1.5f;

    public bool hasKey;
    public float kidCountdown;
	[SerializeField] KnightLightManager KnightLight;

    private float xCoord;
    private NavMeshAgent agent;
    private Transform player;
    private Transform[] toypos;
    private KidState state = KidState.RUNNING;
    
    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;
		decay = StartCoroutine(braveryDecay());
		currentSpeed = speedWalking;
        player = GameObject.FindGameObjectWithTag("KnightLight").transform;
        agent = GetComponent<NavMeshAgent>();
       // alive = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log("KEY: " + hasKey);
        //GO TO KEY
        GameObject key = GameObject.FindGameObjectWithTag("Key");
        if (key != null)
        {
            agent.SetDestination(key.transform.position);
            state = KidState.RUNNING;
            if (Vector3.Distance(transform.position, key.transform.position) <= 2.5f)
            {
                hasKey = true;
                Destroy(key.gameObject);
                FindObjectOfType<AudioManager>().Play("ToySqueak02");
            }

        }
        else
        {
            //GO TO FIRST INSTANTIATED TOY
            GameObject[] toys = GameObject.FindGameObjectsWithTag("Toy");
            if (toys.Length > 0)
            {
                if (Vector3.Distance(transform.position, toys[0].transform.position) <= 20f)
                {
                    agent.SetDestination(toys[0].transform.position);
                    //IF CLOSE ENOUGH, DESTROY THE TOY
                    if (Vector3.Distance(transform.position, toys[0].transform.position) <= 2.5f && state == KidState.RUNNING)
                    {
                       
                        state = KidState.ATTACKING;
                        Destroy(toys[0].gameObject);
                        state = KidState.RUNNING;
                       FindObjectOfType<AudioManager>().Play("ToySqueak01");
                    }
                }
            }
            else //GO TO PLAYER IF NO TOYS
            {
                state = KidState.RUNNING;
                if (Vector3.Distance(transform.position, player.position) <= 2f)
                {
                    stopMovement();
                }
                else
                {
                    moveToPlayer();
                }
            }

        }
        //END OF PATHING

        //if(Vector3.Distance(transform.position, player.position) >= 10f)
        //{
            //Debug.Log("TESTING");
        //    braveryDecay();
        //}
        //braveryBarUI.value = braveryMeterValue;
        //setCurrentSpeed();
        //flashRecharger();
    }

    private void stopMovement()
    {
        //agent.isStopped = true;
        transform.position = this.transform.position;
        agent.SetDestination(transform.position);        
    }
    //Speed Functions Start
    /*	private void setCurrentSpeed()
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
        */
    //Speed Functions End


    //Bavery Functions Start

    private void moveToPlayer()
    {
        xCoord = agent.transform.position.x;
        if (xCoord <= player.position.x)
        {
         //   monster_Anime.SetBool("IsMoveLeft", false);
         //  monster_Anime.SetBool("IsMoveRight", true);
        }

        agent.isStopped = false;
        agent.speed = speed;
        agent.SetDestination(player.position);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles = new Vector3(eulerAngles.x, 0, eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);

    }

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

	private void OnTriggerEnter(Collider LightSource)
	{
		GameObject lightSource = LightSource.gameObject;
		if (LightSource.tag == "KnightLight")
		{
			StopCoroutine(decay);
			heal = StartCoroutine(BraveryRecoveryRate(1.5f));
		}
		if (LightSource.tag == "Lamp")
		{
			StopCoroutine(decay);
			heal = StartCoroutine(BraveryRecoveryRate(0.1f));
		}

        if(LightSource.tag=="Toy")
        {
            Debug.Log("TEST COLLIDE TOY");
            Destroy(LightSource.gameObject);
        }

	}



	private void OnTriggerExit(Collider LightSource)
	{
		if (LightSource.tag == "KnightLight" || LightSource.tag == "Lamp")
		{
			StopCoroutine(heal);
			decay = StartCoroutine(braveryDecay());
		}
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
}
