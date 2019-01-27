using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToNextLevel : MonoBehaviour
{

    public GameObject kidGameObject;
    public Camera cam;
    //public bool isPaused;
    private bool showDoor;
    public float timeBetweenPause = 1f;
    public float countdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        kidGameObject = GameObject.FindGameObjectWithTag("Kid");
        cam = Camera.FindObjectOfType<Camera>();
        if (kidGameObject == null)
        {
            Debug.LogError("No Kid referrenced.");
        }


        showDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Updating");
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
    }

    private bool startTimer()
    {
        if (countdown >= 0) //if countdown is set
        {
            return true;
        }
        return false; 
    }

    private void changeCamera()
    {
        if (!showDoor)
        {
            Debug.Log("ShowDoor = false");
            if (kidGameObject.GetComponent<KidManager>().hasKey)
            {
                Debug.Log("Kid Has Key");
                //Put Audio of unlocking door here
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
