using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    public GameObject VictoryText;
    void Start()
    {
        VictoryText.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            VictoryText.gameObject.SetActive(true);
        }
    }
}
