using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Player Attributes")]
    GameObject player1;
    //GameObject player2;

    [Header ("GameObject Attributes")]
    public Rigidbody player1rb;
    //public Rigidbody player2rb;

    [Header("Movement Attributes")]
    public float speed;
    private Vector3 movement;
    private Vector3 jumpmovement;
    public float jump;
    public float dropSpeed;
    public bool isJumping = false;
    public float maxSpeed;

    [Header("Local Multiplayer Attributes")]
    public string horizontalattribiute = "Horizontal_P1";
    public string verticalattribute = "Forward_P1";
    public string jumpattribute = "Jump_P1";

    //public string controllerhorizontal = "Horizontal";

    void Start()
    {
        player1 = GameObject.Find("Player 1");
        //player2 = GameObject.Find("Player 2");
    }

    void FixedUpdate()
    {
        MovementFunction();

        if (Input.GetButtonDown(jumpattribute) && !isJumping)
        {
            isJumping = true;
            player1rb.AddForce(transform.up * jump);
        }
        else if (isJumping)
        {
            player1rb.AddForce(-transform.up * Time.deltaTime);
            isJumping = false;
        }
    }

    void MovementFunction()
    {
        float h = Input.GetAxis(horizontalattribiute) * speed * Time.deltaTime;
        float v = Input.GetAxis(verticalattribute) * speed * Time.deltaTime;

        //float h2 = Input.GetAxis(controllerhorizontal) * speed * Time.deltaTime;

        movement = new Vector3(h, 0, v);
        if (player1rb.velocity.magnitude > maxSpeed)
        {
            player1rb.velocity = player1rb.velocity.normalized * maxSpeed;
        }
        player1rb.AddForce(movement);

        //movementcontroller = new Vector3(h2, 0, 0);
        //player2rb.AddForce(movementcontroller);
    }
}
