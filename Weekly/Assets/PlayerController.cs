using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float count = 0;

    private float xScale = 1;
    private float yScale = 1;

    private float radius = 0.5f;

    private float hsp = 0;
    private float vsp = 0;

    private float masterSpeed = 1;

    private float grndAcc;
    private float grndMax;

    private float airAcc;
    private float airFric = 0.01f;
    private float airSpeed;

    private float jumpSpeed;
    private bool jumpHold = false;

    private bool grounded = false;

    private float grav = 0.03f;
    private float physicsFric = 0.5f;

    private float acc;
    private float spd;
    private float fric;

    private bool wallLe;
    private bool wallRi;

    void Start () {
        grndAcc = 0.15f * masterSpeed;
        grndMax = 4f * masterSpeed;

        airAcc = 0.1f * masterSpeed;
        airSpeed = 3.5f * masterSpeed;

        jumpSpeed = 3 * masterSpeed;
    }
	
	void Update () {
        if (grounded == false) //If we are in the air.
        {
            acc = airAcc;
            spd = airSpeed;
            fric = physicsFric;
        }
        if (grounded == true) //If we are on the ground.
        {
            acc = grndAcc;
            spd = grndMax;
            fric = physicsFric;
        }

        if (wallL(transform.position)) //Check if theres a wall to the left of the player.
        {
            wallLe = true;
        }
        else
        {
            wallLe = false;
        }

        if(wallR(transform.position)) //Check if there is a wall to the right of the player.
        {
            wallRi = true;
        }
        else
        {
            wallRi = false;
        }

        //Get player input.
        float keyHorizontal = Input.GetAxis("Horizontal");
        //bool keyJump = Input.GetButtonDown("Jump");
        //bool keyRun = Input.GetButtonDown("Run");

        //Movment
        if (keyHorizontal <= -0.3f) //Move to the left.
        {
            hsp -= acc;
            if (hsp >= 0.3f) { hsp -= fric; }
        }

        if (keyHorizontal <= -0.3f) //Move to the right.
        {
            hsp += acc;
            if (hsp <= -0.3f) { hsp += fric; }
        }

        //Stop the player.
        Debug.Log(keyHorizontal);
        if (keyHorizontal <= 0.3f)
        {
            if (keyHorizontal > -0.3f)
            {
                if (hsp >= 0.3f)
                {
                    hsp -= fric;
                }
                else if (hsp <= -0.3f)
                {
                    hsp += fric;
                }
                else
                {
                    hsp = 0;
                }
            }
        }

        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            vsp = jumpSpeed;

            xScale = 0.75f;
            yScale = 1.20f;

            jumpHold = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpHold = false;
            count = 0;
        }

        if (jumpHold == true)
        {
            count++;
            if (count >= 20)
            {
                jumpHold = false;
                count = 0;
            }
        }

        //Wall Jumping
        if (!grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (wallLe == true)
                {
                    if (Input.GetAxis("horizontal") < -0.3)
                    {
                        hsp = jumpSpeed * -3;
                        vsp = jumpSpeed * 1;
                    }
                    else if (wallLe == true)
                    {
                        if (Input.GetAxis("Horizontal") > -0.3f)
                        {
                            hsp = jumpSpeed * -0.8f;
                            vsp = jumpSpeed * 0.7f;
                        }
                    }
                }
                else if (wallRi == true)
                {
                    if (Input.GetAxis("horizontal") > 0.3)
                    {
                        hsp = jumpSpeed * 3;
                        vsp = jumpSpeed * 1;
                    }
                    else if (wallLe == true)
                    {
                        if (Input.GetAxis("Horizontal") < 0.3f)
                        {
                            hsp = jumpSpeed * 0.8f;
                            vsp = jumpSpeed * 0.7f;
                        }
                    }
                }
            }
        }
    }




    // This paragraph is used for checking if there´s a wall to the right or left of the player.
    private bool wallL(Vector3 playerPos)
    {
        RaycastHit LRayHitInfo;
        if (Physics.Raycast(new Vector3(playerPos.x, playerPos.y + radius / 2, playerPos.z), Vector3.left, out LRayHitInfo, radius + 0.1f))
        {
            if (LRayHitInfo.transform.tag == "Solid")
            {
                return true;
            }
        }
        else if (Physics.Raycast(new Vector3(playerPos.x, playerPos.y - radius / 2, playerPos.z), Vector3.left, out LRayHitInfo, radius + 0.1f))
        {
            if (LRayHitInfo.transform.tag == "Solid")
            {
                return true;
            }
        }
        return false;
    }

    private bool wallR(Vector3 playerPos)
    {
        RaycastHit RRayHitInfo;
        if (Physics.Raycast(new Vector3(playerPos.x, playerPos.y + radius / 2, playerPos.z), Vector3.right, out RRayHitInfo, radius + 0.1f))
        {
            if (RRayHitInfo.transform.tag == "Solid")
            {
                return true;
            }
        }
        else if (Physics.Raycast(new Vector3(playerPos.x, playerPos.y - radius / 2, playerPos.z), Vector3.right, out RRayHitInfo, radius + 0.1f))
        {
            if (RRayHitInfo.transform.tag == "Solid")
            {
                return true;
            }
        }
        return false;
    }
    // This paragapfh is finished.
}
