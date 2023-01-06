using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFriend : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Awake()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        cubeRevolve();
        DestroyCube();
    }

    void cubeRevolve()
    {
      transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
      transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playermovement))
        {
            rotationSpeed = 1000f;
            playerMovement = playermovement;

        }
    }

    PlayerMovement playerMovement = null;
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playermovement))
        {
            rotationSpeed = 10f;
        }
    }

    void DestroyCube()
    {
        if (rotationSpeed == 1000f && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
            playerMovement.AddJump();
        }
    }
}

