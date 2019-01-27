using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SingleTargetCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float smoothTime = .5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;


    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

    }

    void Update()
    {
        if(target == null)
        {
            return;
        }

        Move();
    }

    void Zoom()
    {
        Debug.Log(GetGreatestDistance());
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);

    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(target.position, Vector3.zero);
        //for(int i = 0; i< target.Count; i++)
        //{
        //    bounds.Encapsulate(targets[i].position);
        //}
        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        //if(targets.Count == 1)
        //{
        //    return targets[0].position;
        //}

        var bounds = new Bounds(target.position, Vector3.zero);
        //for(int i = 0; i < targets.Count; i++)
        //{
        bounds.Encapsulate(target.position);
        //}
        return bounds.center;

    }


}
