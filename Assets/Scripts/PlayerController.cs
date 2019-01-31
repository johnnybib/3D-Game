using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform _cameraContainer;
    private CharacterController _controller;

    [SerializeField]
    private float stickThreshold;
    [SerializeField]
    private float moveSpeed;//units per second
    [SerializeField]
    private float turnSpeed;//rad per second

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraContainer = GameObject.FindWithTag("MainCamera").transform.parent;
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("HorizontalL") + ", " + Input.GetAxis("VerticalL") + ", " + Input.GetAxis("HorizontalR") + ", " + Input.GetAxis("VerticalR"));
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        //if (Mathf.Abs(inputX) < stickThreshold)
        //{
        //    inputX = 0;
        //}
        //if(Mathf.Abs(inputY) < stickThreshold)
        //{
        //    inputY = 0;
        //}

        Vector3 move = new Vector3(inputX, 0, inputZ);
        Vector3 rotatedMove = _cameraContainer.rotation * move;
        _controller.Move(rotatedMove * moveSpeed * Time.deltaTime);
        

        if (rotatedMove != Vector3.zero)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, rotatedMove, turnSpeed * Time.deltaTime, 0.0f);
    
            // Move our position a step closer to the target
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
