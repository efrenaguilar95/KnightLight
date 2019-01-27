using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterManager : MonoBehaviour
{
    public Sprite toySprite;

    [SerializeField] float speed = 5.5f;
    [SerializeField] int attackStrength = 6;
    [SerializeField] bool childInLampAOE;
    [SerializeField] bool isToy = false;
    [SerializeField] LampManager Lamp;
    [SerializeField] GameObject duskParticle;
    bool NotDustPArticle = true;

    Animator monster_Anime;

    private NavMeshAgent agent;
    private Transform player;
    private bool alive;
    private float xCoord;
    private Vector3 eulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        monster_Anime = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Kid").transform;
        agent = GetComponent<NavMeshAgent>();
     //   FindObjectOfType<AudioManager>().Play("SleepTight");
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //turnToToy();
        //ChildInLight();
        if (Vector3.Distance(transform.position, player.position) <= 1 || isToy)
        {
            stopMovement();
        }
        else
        {
            moveToPlayer();
        }
    }

    private void ChildInLight()
    {
        //	if(Lamp.isChildInLampAOE())
        //{
        //stopMovement();
        //	}
    }

    private void freezeAngle()
    {
        eulerAngles = transform.rotation.eulerAngles;
        eulerAngles = new Vector3(eulerAngles.x, 0, eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);
    }

    private void moveToPlayer()
    {
        xCoord = agent.transform.position.x;
        if (xCoord <= player.position.x)
        {
            monster_Anime.SetBool("IsMoveLeft", false);
            monster_Anime.SetBool("IsMoveRight", true);
        }
        agent.isStopped = false;
        agent.speed = speed;
        agent.SetDestination(player.position);
        freezeAngle();
    }

    private void stopMovement()
    {
        agent.isStopped = true;
        freezeAngle();
        transform.position = this.transform.position;
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
        if (collision.gameObject.tag == "Kid" && alive)
        {
            if (GetIsToy())
            {
                collision.gameObject.GetComponent<KidManager>().RecoveryBraveryChunk();
                Destroy(gameObject);
                alive = false;
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
            turnToToy();
            if (this.toySprite.name == "key")
            {
                this.gameObject.tag = "Key";
            }
            else
            {
                this.gameObject.tag = "Toy";
            }
        }
    }

    private void OnTriggerExit(Collider Light)
    {
        if (Light.gameObject.tag == "KnightLight" || Light.gameObject.tag == "Lamp")
        {
            monster_Anime = GetComponent<Animator>();
            if (this.gameObject.tag != "Key")
            {
                turnToToy();
                this.gameObject.tag = "Monster";
            }
        }
    }


    private void Attack(Collision collision)
    {
        collision.gameObject.GetComponent<KidManager>().LoseBravery(attackStrength);
    }

    private void turnToToy()
    { FindObjectOfType<AudioManager>().Play("MonsterPoof01");
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        if (isToy)
        {
            monster_Anime.SetBool("IsToy", false);
            this.gameObject.GetComponent<Animator>().enabled = true;
            PlayDustParticle();
           
            isToy = false;
        }
        else
        {
            monster_Anime.SetBool("IsToy", true);
            PlayDustParticle();
            stopMovement();
            this.gameObject.GetComponent<Animator>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = toySprite;
            //Debug.Log(this.gameObject.GetComponent<SpriteRenderer>().sprite);
            isToy = true;
        }
        //}
    }

    private void PlayDustParticle()
    {
        if (NotDustPArticle)
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
