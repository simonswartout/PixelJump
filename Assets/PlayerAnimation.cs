using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    public PlayerController PlayerController => playerController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       IsRunning();
       IsDead();
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

    void IsDead()
    {
        if (playerController.Health <= 0)
        {
            animator.SetBool("IsDead", true);
            Debug.Log("Dead");
        }
        else
        {
            animator.SetBool("IsDead", false);
        }
    }
}
