using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    [SerializeField] float moveSpeed = 10f;
    
    [SerializeField] float jumpForce = 10f;

    [SerializeField] int jumpsTaken = 0;

    [SerializeField] int maxJumps = 2;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>(); 
    }
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        SetLookDirection();
        IsGrounded();


    }
   

    void SetLookDirection()
    {
        Vector3 lookDirection = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
    void Move()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }
#region Jumping
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpsTaken < maxJumps - 1)
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpsTaken++;
        }
    }

    void AutoJump() 
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    public void IsGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, 1f, ~playerController.PlayerLayer))
        {
            jumpsTaken = 0;
        }
    }

    public void AddJump()
    {
        maxJumps++;
    }

#endregion 


#region Collisions
    
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision" + collision.gameObject.name);
        StickyPlatform stickyPlatform = collision.collider.GetComponentInParent<StickyPlatform>();
        
        if (stickyPlatform)
        {
            transform.SetParent(stickyPlatform.transform);
        }

    }

    void OnCollisionExit(Collision collision)
    {
        StickyPlatform stickyPlatform = collision.collider.GetComponentInParent<StickyPlatform>();
        
        if (stickyPlatform)
        {
            transform.SetParent(null);
        }
    }
#endregion

}