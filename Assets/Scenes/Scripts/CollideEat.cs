using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideEat : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInChildren<Animator>().enabled = true;
        Debug.Log("Trigger Detected");
        Destroy(gameObject, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInChildren<Animator>().enabled = true;
        Debug.Log("Hit Detected");
    }

  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
