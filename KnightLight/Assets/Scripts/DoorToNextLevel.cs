using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{ 
    public GameObject kidGameObject;
    public Transform knightTransform;
    public Camera cam;
    //public bool isPaused;
    private bool showDoor;
    public float timeBetweenPause = 1f;
    public float countdown = 0;
    private Vector3 doorPosition;
    private float kidDistance;
    private float knightDistance;

    // Start is called before the first frame update
    void Start()
    {
        kidGameObject = GameObject.FindGameObjectWithTag("Kid");
        knightTransform = GameObject.FindGameObjectWithTag("KnightLight").transform;
        cam = Camera.FindObjectOfType<Camera>();
        if (kidGameObject == null)
        {
            Debug.LogError("No Kid referenced.");
        }
        if (knightTransform == null)
        {
            Debug.LogError("No Knight referenced.");
        }
        doorPosition = this.transform.position;
        showDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer())
        {
            countdown -= Time.deltaTime;
            if (showDoor)
            {
                this.gameObject.transform.GetChild(2).GetComponent<Light>().enabled = true;
            }
        }
        else
        {
            changeCamera();
        }
        checkDistanceFromDoor();
    }

    private bool startTimer()
    {
        if (countdown >= 0) //if countdown is set
        {
            return true;
        }
        return false; 
    }

    private void checkDistanceFromDoor()
    {
        if (showedDoor())
        {
            kidDistance = Vector3.Distance(doorPosition, kidGameObject.transform.position);
            knightDistance = Vector3.Distance(doorPosition, knightTransform.position);
            if (kidDistance <= 5f && knightDistance <= 5f)
            {
                //change scene
                //Debug.Log("Changing Scene");
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void changeCamera()
    {
        if (!showDoor)
        {
            if (kidGameObject.GetComponent<KidManager>().hasKey)
            {
                //Put Audio of unlocking door here
                FindObjectOfType<AudioManager>().Play("DoorOpen");

                cam.GetComponent<SingleTargetCamera>().enabled = true;
                cam.GetComponent<MultipleTargetCamera>().enabled = false;
                countdown = timeBetweenPause;
                showDoor = true;
            }
        }
        else
        {
            cam.GetComponent<MultipleTargetCamera>().enabled = true;
            cam.GetComponent<SingleTargetCamera>().enabled = false;
        }
    }

    public bool showedDoor()
    {
        return showDoor;
    }
}
