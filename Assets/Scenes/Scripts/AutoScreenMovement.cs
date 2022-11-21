using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoScreenMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 9f;
    private float timeElapsed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    if (timeElapsed>timer) {
            SceneManager.LoadScene("scene2");
    }
    }
}
