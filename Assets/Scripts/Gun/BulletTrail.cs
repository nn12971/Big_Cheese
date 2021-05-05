using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{

    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Translate(move);
    }
}
