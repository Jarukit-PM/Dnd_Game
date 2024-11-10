using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField]
    private float z_MoveSpeed = 1;
    private BoxCollider2D z_BoxCollider;


    //Methods
    private void Start()
    {
        z_BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //Get the input X,Y 
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //Cache it in a Vector
        Vector2 moveDelta = new Vector2(moveX, moveY);

        //Flip the player according to the move direction
        if(moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Collision check
        RaycastHit2D castResult;
        //Check if we are hitting something in the X Axis
        castResult = Physics2D.BoxCast(transform.position, z_BoxCollider.size, 0, new Vector2(moveX, 0), Mathf.Abs(moveX * Time.fixedDeltaTime * z_MoveSpeed), LayerMask.GetMask("Enemy","BlockMove"));
        if(castResult.collider)
        {
            //STOP MOVING ON THE X AXIS
            moveDelta.x = 0;
        }

        //Check if we are hitting something in the Y Axis
        castResult = Physics2D.BoxCast(transform.position, z_BoxCollider.size, 0, new Vector2(0, moveY), Mathf.Abs(moveY * Time.fixedDeltaTime * z_MoveSpeed), LayerMask.GetMask("Enemy", "BlockMove"));
        if (castResult.collider)
        {
            //STOP MOVING ON THE Y AXIS
            moveDelta.y = 0;
        }

        bool isWalking = moveDelta.magnitude > 0;

        transform.Translate(moveDelta * Time.fixedDeltaTime * z_MoveSpeed);
    }

}