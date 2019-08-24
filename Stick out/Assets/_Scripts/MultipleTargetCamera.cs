using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public  Vector3 offset;
    private Vector3 veclocity;
    public float smoothTime = 0.5f;
    public float maxZoom=7f, minZoom=20f;
    public float zoomDivider=4f, zoomLimiter=50f;
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref veclocity, smoothTime);
        Zoom();
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetDistance()/zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom/zoomDivider, Time.deltaTime);

    }
    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds (targets[0].position , Vector3.zero);
        for(int i = 0;i < targets.Count;i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
    float GetDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i<targets.Count;++i)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
}
