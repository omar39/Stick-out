using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour 
{
    public AudioSource audio_source;
    public AudioClip dashSoundClip;
    public float dashSpeed,startDashTime;
    public static int dashCharge;
    Vector2 distination; 
    Rigidbody2D rigidBody;
    float dashTime;
    bool isFlying,isDashing,playerGoesDown;
    char direction;
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        isFlying = isDashing = playerGoesDown =false;
        dashTime = startDashTime;
        direction = ' ';
        dashCharge = 0;
    }
    void Update()
    {
        getLastCharPressed();
        if(Input.GetKeyDown(KeyCode.LeftShift) && isFlying && direction!= ' ' && dashCharge == 1)
        {
            isDashing = true;
            audio_source.clip = dashSoundClip;
            audio_source.Play();
        }  
        DashPlayer();
    }
    void DashPlayer()
    {
        if(isDashing)
        {   
            dashCharge = 0;
            if(dashTime<=0)
            {
                isDashing = false;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if(direction == 'd')
                {
                    rigidBody.velocity = Vector2.right * dashSpeed;
                }
                else if(direction == 'a')
                {
                    rigidBody.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }
    void getLastCharPressed()
    {
        if(Input.GetKey("d"))
        {
            direction = 'd';
        }
        else if(Input.GetKey("a"))
        {
            direction = 'a';
        }
    }
    void OnCollisionStay2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Floor" && collisionInfo.gameObject.name != "Line")
        {
            isFlying = false;
            dashCharge = 0;
        }
    }
    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Floor")
        {
            isFlying = true;
            dashCharge = 1;
        }
    }
}