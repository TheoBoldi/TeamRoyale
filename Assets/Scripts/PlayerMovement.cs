using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playercontroller;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (movement.x > 0)
        {
            if (movement.y != 0)
                playercontroller.SetInteger("updownint", 0);
            playercontroller.SetInteger("leftrightint", 1);
        }

        if (movement.x < 0)
        {
            if (movement.y != 0)
                playercontroller.SetInteger("updownint", 0);
            playercontroller.SetInteger("leftrightint", -1);
        }

        if (movement.y > 0)
        {
            playercontroller.SetInteger("updownint", 1);
        }

        if (movement.y < 0)
        {
            playercontroller.SetInteger("updownint", -1);
        }

        if (movement.x == 0 && movement.y == 0)
        {
            playercontroller.SetInteger("updownint", 0);
            playercontroller.SetInteger("leftrightint", 0);
        }

        if (movement.x == 0 )
        {
            playercontroller.SetInteger("leftrightint", 0);
        }

        if (movement.y == 0)
        {
            playercontroller.SetInteger("updownint", 0);
        }
    }

    private void FixedUpdate()
    {
        if (movement == Vector2.zero) return;

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
