using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SceneController sceneController;
    Rigidbody rb;

    Vector3 startingPosition;

    [SerializeField] float moveSpeed = 10f;
    
    [SerializeField] float jumpForce = 10f;

    [SerializeField] int jumpsTaken = 0;

    [SerializeField] int maxJumps = 2;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] float fallOffDistance = -10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position; //set player starting position
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
        FallRecovery();
        IsGrounded();


    }

    bool PlayerDied()
    {
        if (transform.position.y < fallOffDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        if(Physics.Raycast(transform.position, Vector3.down, 1f, ~playerLayer))
        {
            jumpsTaken = 0;
        }
    }

    public void AddJump()
    {
        maxJumps++;
    }

#endregion 

        
    
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision" + collision.gameObject.name);
        StickyPlatform stickyPlatform = collision.collider.GetComponentInParent<StickyPlatform>();
        
        if (stickyPlatform)
        {
            transform.SetParent(stickyPlatform.transform);
        }

        if(collision.gameObject.CompareTag("Enemy Body"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Die();
            }
        }

        if(collision.gameObject.CompareTag("Victory Platform"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            //sceneController.LoadNextScene();
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
    void FallRecovery()
    {
        if (transform.position.y < fallOffDistance) 
        {
            Die();
        }
    }
    
    void Die()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
            renderer.enabled = false;
            }
        }
        transform.position += new Vector3(0, 3, 0);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(ReloadLevel), 1.3f);
    }

    void ReloadLevel()
    {
        sceneController.ReloadScene();
    }
}