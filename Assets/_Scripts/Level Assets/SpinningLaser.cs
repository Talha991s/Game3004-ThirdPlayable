using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningLaser : MonoBehaviour
{
    public float spinSpeed;

    void Start()
    {
        
    }

    
    void Update()
    {
        Spin();
    }

    void Spin()
    {
        transform.Rotate(new Vector3(0, 1, 0) * spinSpeed * Time.deltaTime);
    }
}
