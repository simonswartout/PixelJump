using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerAnimation playerAnimation;

    
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerAnimation PlayerAnimation => playerAnimation;
    public LayerMask PlayerLayer => playerLayer;
    public float Health => health;

    [Header("References")]
    [SerializeField] SceneController sceneController;
    [SerializeField] GameObject playerModel;

    [Header("Behavior")]
    [SerializeField] float fallOffDistance = -10f;
    [SerializeField] LayerMask playerLayer;

    [Header("Stats")]
    [SerializeField] float health = 1f;



    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update() 
    {
        FallRecovery();
    }

#region Health Functions
    public void DealDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DieFromEnemy();
        }
    }
    void DieFromFall()
    {
        playerModel.SetActive(false);
        ReloadLevel(1.3f);
    }

    void DieFromEnemy()
    {
        ReloadLevel(1.3f);
    }

   
#endregion

#region Collisions
    public void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Victory Platform"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            //sceneController.LoadNextScene();
        }
    }

#endregion

    void ReloadLevel(float waitTime = 0)
    {
        StartCoroutine(ReloadLevel_Routine(waitTime));
    }
    IEnumerator ReloadLevel_Routine(float waitTime = 0) //this is a coroutine, which is a function that can be paused and resumed
    {
        yield return new WaitForSeconds(waitTime);
        sceneController.ReloadScene();
    } 
    void FallRecovery()
    {
        if (transform.position.y < fallOffDistance) 
        {
            DieFromFall();
        }
    }
}
