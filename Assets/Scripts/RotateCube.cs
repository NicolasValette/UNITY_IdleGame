using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, _rotateSpeed * Time.deltaTime, 0f), Space.World);
        
    }
}
