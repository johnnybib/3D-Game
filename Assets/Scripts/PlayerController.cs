using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform _cameraContainer;
    private CharacterController _controller;
    private Transform _groundChecker;

    private Vector3 _velocity;
    private bool _isGrounded;
    private int _ground;

    private SphereCollider _attackHitBox;

    //State in order of precedence
    private enum States {Idle, Moving, Attacking}
    private States state;

    [SerializeField]
    private float MoveSpeed = 10.0f;//units per second
    [SerializeField]
    private float TurnSpeed = 10.0f;//rad per second
    [SerializeField]
    private float GroundDistance = 1.1f;


    [SerializeField]
    private float Health = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraContainer = GameObject.FindWithTag("MainCamera").transform.parent;
        _controller = GetComponent<CharacterController>();
        _velocity = Vector3.zero;
        _groundChecker = transform.Find("Ground Checker");
        _ground = 1 << LayerMask.NameToLayer("Ground");

        _attackHitBox = transform.Find("Attack Hit Box").gameObject.GetComponent<SphereCollider>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        //======Combat======
        if (Input.GetButton("Attack"))
        {
            Debug.Log("Attack");
            state = States.Attacking;
            Attack();
            //Do not exit until attack animation is done.
        }
        //======Movement======
        //Debug.Log(Input.GetAxis("HorizontalL") + ", " + Input.GetAxis("VerticalL") + ", " + Input.GetAxis("HorizontalR") + ", " + Input.GetAxis("VerticalR"));
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        //if (Mathf.Abs(inputX) > 0 && Mathf.Abs(inputZ) > 0)
        //{
            state = States.Moving;
            Move(inputX, inputZ);
        //}
    }

    private void Move(float inputX, float inputZ)
    {
        Vector3 move = new Vector3(inputX, 0, inputZ);
        Vector3 rotatedMove = _cameraContainer.rotation * move;
        _controller.Move(rotatedMove * MoveSpeed * Time.deltaTime);
        if (rotatedMove != Vector3.zero)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, rotatedMove, TurnSpeed * Time.deltaTime, 0.0f);

            // Move our position a step closer to the target
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, _ground, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
        else
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }

    private void Attack()
    {
        _attackHitBox.enabled = true;
    }
}
