using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator animationController;
    [SerializeField] protected float health = 1f;

    public float Health => health; //creates a health variable that gets the value of the protected field health
    
    public void DealDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die(float waitToDie = 0f) //virtual functions can be overriden by child classes, e.g. Goblin
    {
        Destroy(gameObject, waitToDie);
    }
}
