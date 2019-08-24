using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float triggerTime;
    void Start()
    {
        Invoke("triggerBomb", triggerTime);
	}

    void triggerBomb()
    {
        GetComponent<PointEffector2D>().enabled = true;
    }

}