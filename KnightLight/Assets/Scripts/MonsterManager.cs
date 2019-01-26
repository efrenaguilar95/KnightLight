using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

	[SerializeField] float speed = 3.5f;
	[SerializeField] int attackStrength = 6;
	[SerializeField] LampManager Lamp;
	[SerializeField] bool childInLampAOE;
	[SerializeField] bool isToy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		ChildInLight();
	}

	private void ChildInLight()
	{
		if(Lamp.isChildInLampAOE())
		{
			stopMovement();
		}
	}

	private void stopMovement()
	{

	}

	public void changeToToy()
	{
		isToy = true;
	}

	public bool GetIsToy()
	{
		return isToy;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Kid")
		{
			if (GetIsToy())
			{
				collision.gameObject.GetComponent<KidManager>().RecoveryBraveryChunk();
				Destroy(gameObject);
			}
			else
			{
				Attack(collision);
			}
		}
		if(collision.gameObject.tag == "KnightLight" || collision.gameObject.tag == "Lamp")
		{
			Animator monsterAnimator = gameObject.GetComponent<Animator>();
			monsterAnimator.SetBool("InLight", true);
		}
	}

	private void Attack(Collision collision)
	{
		collision.gameObject.GetComponent<KidManager>().LoseBravery(attackStrength);
	}
}
