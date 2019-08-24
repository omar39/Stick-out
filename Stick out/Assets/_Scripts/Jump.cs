using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Transform throw_point_player;
    public float time_to_fall = 1f;
    Rigidbody2D rigid;
    float fall = -3f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) //  will be changed according according to controlls
        {
            jump();
        }

    }
    void jump()
    {
        float x_distance;
        x_distance = (throw_point_player.position.x - 2) + throw_point_player.position.x; // (+/- 2) changed according to the player destination

        float y_distance;
        y_distance = (throw_point_player.position.y + 2) - throw_point_player.position.y;

        float thrown_angle;
        thrown_angle = Mathf.Atan((y_distance + 4.905f * (time_to_fall * time_to_fall)) / x_distance);

        float intial_velocity = x_distance / (Mathf.Cos(thrown_angle) * time_to_fall);

        float x_velocity, y_velocity;
        x_velocity = intial_velocity * Mathf.Cos(thrown_angle);
        y_velocity = intial_velocity * Mathf.Sin(thrown_angle);

        rigid.velocity = new Vector2(x_velocity, y_velocity);


    }

}
