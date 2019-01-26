using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{

	[SerializeField] bool childInLampAOE = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		childInLampAOE = false;
	}

	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Child")
		{
			isChildInLampAOE();
		}
	}

	public bool isChildInLampAOE()
	{
		childInLampAOE = true;
		return childInLampAOE;
	}



}
