using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float xScale = 1;
    private float yScale = 1;

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

    void Start () {
        grndAcc = 0.15f * masterSpeed;
        grndMax = 4f * masterSpeed;

        airAcc = 0.1f * masterSpeed;
        airSpeed = 3.5f * masterSpeed;

        jumpSpeed = 3 * masterSpeed;
    }
	
	void Update () {
		 
	}
}
