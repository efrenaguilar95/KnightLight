using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

	[SerializeField] float speed = 3.5f;
	[SerializeField] LampManager Lamp;
	[SerializeField] bool childInLampAOE;
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
}
