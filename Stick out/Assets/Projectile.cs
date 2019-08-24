//	Name			Chau Tran
//	Last Modified	Auf 9th,2017

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Transform target;
    public Transform throwPoint;
	public GameObject Bomb;
    public float timeTillHit = 1f;
    public float destroyTime = .5f;

	void Start () {
	}

    void Update() {
        if(Input.GetKeyUp(KeyCode.LeftControl))
            Throw();    
    }

    public void Throw()
    {
        float xdistance;
        xdistance = target.position.x -throwPoint.position.x;

        float ydistance;
        ydistance = target.position.y - throwPoint.position.y;

        float throwAngle; // in radian
		//OLD
		//throwAngle = Mathf.Atan ((ydistance + 4.905f) / xdistance);
       	//UPDATED
		throwAngle = Mathf.Atan ((ydistance + 4.905f*(timeTillHit*timeTillHit)) / xdistance);
		//OLD
		//float totalVelo = xdistance / Mathf.Cos(throwAngle) ;
		//UPDATED
		float totalVelo = xdistance / (Mathf.Cos(throwAngle) * timeTillHit);

        float xVelo, yVelo;
        xVelo = totalVelo * Mathf.Cos (throwAngle);
		yVelo = totalVelo * Mathf.Sin (throwAngle);

        GameObject bulletInstance = Instantiate (Bomb, throwPoint.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
        Rigidbody2D rigid;
        rigid = bulletInstance.GetComponent<Rigidbody2D> ();
        rigid.velocity = new Vector2 (xVelo, yVelo);

        Bomb bomb;        
        bulletInstance.GetComponent<Bomb>().triggerTime = destroyTime;
        Destroy(bulletInstance, destroyTime + .05f);
    }



}
