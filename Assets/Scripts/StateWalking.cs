using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalking : BaseState
{
    private state ThisState = state.Walking;
    void OnEnable()
    {
        Debug.Log("Walking");
        //Start animation
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        _playerCon.Move(inputX, inputZ);
        _playerCon.UpdateGravity();
        CheckStateChange(ThisState);
    }
}
