using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour {

    public Rigidbody PlayerRB;
    public float OriginalSpeed;
    public float Speed;
    public float OriginalMaxVelocity;
    public float MaxVelocity;
    

    // Use this for initialization
    void Start () {

	}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            PlayerRB.AddForce(transform.right * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerRB.AddForce(transform.right * -Speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            PlayerRB.AddForce(transform.forward * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerRB.AddForce(transform.forward * -Speed);
        }
        if(PlayerRB.useGravity == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerRB.AddForce(transform.up * Speed * 40);
            }
        }
        if (PlayerRB.useGravity == true)
        {
            if (PlayerRB.velocity.y <= 0)
            {
                PlayerRB.AddForce(transform.up * Speed * 40 *-1);
            }
        }
    }
        // Update is called once per frame
        void Update ()
    {
        if (PlayerRB.velocity.magnitude > MaxVelocity)
        {
            PlayerRB.velocity = Vector3.ClampMagnitude(PlayerRB.velocity, MaxVelocity);
        }
        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            PlayerRB.velocity = new Vector3 (0, PlayerRB.velocity.y, PlayerRB.velocity.z);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            PlayerRB.velocity = new Vector3(PlayerRB.velocity.x, PlayerRB.velocity.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale -= new Vector3(0, 0.5F, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale += new Vector3(0, 0.5F, 0);
        }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MaxVelocity = OriginalMaxVelocity * 1.5f;
                Speed = OriginalSpeed * 1.5f;
            }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            MaxVelocity = OriginalMaxVelocity;
            Speed = OriginalSpeed;
        }
    }
}
