using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

	[SerializeField] float speed = 3.5f;
	[SerializeField] int attackStrength = 6;
	[SerializeField] bool childInLampAOE;
	[SerializeField] bool isToy = false;
	[SerializeField] LampManager Lamp;
	[SerializeField] Sprite toySprite;
	[SerializeField] GameObject duskParticle;
	bool NotDustPArticle = true;

	Animator monster_Anime;
    // Start is called before the first frame update
    void Start()
    {
        monster_Anime = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		//turnToToy();
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

	}
	private void OnTriggerEnter(Collider Light)
	{
		if (Light.gameObject.tag == "KnightLight" || Light.gameObject.tag == "Lamp")
		{
			Animator monsterAnimator = gameObject.GetComponent<Animator>();
			monsterAnimator.SetBool("InLight", true);
			turnToToy();
			//PlayDustParticle();
		}
	}


	private void Attack(Collision collision)
	{
		collision.gameObject.GetComponent<KidManager>().LoseBravery(attackStrength);
	}

	private void turnToToy()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
			if(isToy)
			{
				monster_Anime.SetBool("IsToy", false);
				PlayDustParticle();
				isToy = false;
			}
			else
			{
				monster_Anime.SetBool("IsToy", true);
				PlayDustParticle();
				isToy = true;
			}
		//}
	}

	private void PlayDustParticle()
	{
		if(NotDustPArticle)
		{
			NotDustPArticle = false;
			GameObject dustObj = Instantiate(duskParticle, this.transform);
			StartCoroutine(destroyDustParticle(dustObj));
		}
		
	}

	IEnumerator destroyDustParticle(GameObject toDestroy)
	{
		yield return new WaitForSeconds(1);
		Destroy(toDestroy);
		NotDustPArticle = true;

	}


}
