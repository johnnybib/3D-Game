using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : BaseState
{
    private state ThisState = state.Idle;
    void OnEnable()
    {
        Debug.Log("Idle");
        //Start animation
    }

    // Update is called once per frame
    void Update()
    {
        _playerCon.UpdateGravity();
        CheckStateChange(ThisState);
    }
}
