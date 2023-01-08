using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float DeathAnimation() // this function plays the death animation and returns the length of the animation
    {
        animationController.Play("Die01");
        return animationController.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void Die(float waitToDie = 0f) //overrides the Die function in the Enemy class and makes sure that the goblin does not die instantly
    {
        waitToDie = DeathAnimation();
        base.Die(waitToDie);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.TryGetComponent(out PlayerController player)) //checks if the object that the goblin collided with is the player
        {
             if (player.transform.position.y - transform.position.y > 0)
            {
                DealDamage(Health); //if the player is above the goblin, the goblin dies
            }
            else
            {
                player.DealDamage(player.Health);
            }
        }
    }
}
