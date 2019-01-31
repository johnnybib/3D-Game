using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal2");
        //if (Mathf.Abs(inputX) < 0.3)
        //{
        //    inputX = 0;
        //}
        transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime * inputX);
    }
}
