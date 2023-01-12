using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    PlayerController playerController;

    public float Health => playerController.Health;

    public PlayerController PlayerController => playerController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       IsRunning();
    }

    void IsRunning() 
    {
            if (animator.GetBool("IsDead") == false && Input.GetKey(KeyCode.A)) 
            {
                animator.SetBool("IsRunning", true);
            }
            else if (animator.GetBool("IsDead") == false && Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsRunning", true);
            }
            else if (animator.GetBool("IsDead") == false && Input.GetKey(KeyCode.W))
            {
                animator.SetBool("IsRunning", true);
            }
            else if (animator.GetBool("IsDead") == false && Input.GetKey(KeyCode.S))
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
    }
    void OnDeathAnimation() // this function plays the death animation and returns the length of the animation
    {
        animator.SetBool("IsDead", true);
        
    }
}
        
    


