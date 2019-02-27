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
        _attackHitBox = transform.Find("Attack Hit Box").gameObject.GetComponent<SphereCollider>();

        GetComponent<StateIdle>().enabled = true;
    }

    public void Move(float inputX, float inputZ)
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
    }

    public void UpdateGravity()
    {
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

    public void StartAttack()
    {
        _attackHitBox.enabled = true;
    }

    public void StopAttack()
    {
        _attackHitBox.enabled = false;
    }
}
