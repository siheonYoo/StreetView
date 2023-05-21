using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sight : MonoBehaviour
{
    public float fbspeed = 30f;
    public float lrspeed = 30f;
    Vector3 fb = new Vector3(1, 0, 0);
    Vector3 lr = new Vector3(0, 1, 0);

    

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical") * Time.deltaTime;
        float h = Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.Rotate(fb * v * fbspeed);
        transform.Rotate(lr * h * lrspeed);
        
    }
}
