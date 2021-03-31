using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log(gameObject.name + "died");
            Destroy(gameObject);
        }
    }

}
