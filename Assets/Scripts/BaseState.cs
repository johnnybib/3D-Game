using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public PlayerController _playerCon;
    public enum state {Idle, Walking, Attacking};

    void Start()
    {
        _playerCon = GetComponent<PlayerController>();
    }

    public void CheckStateChange(state currentState)
    {
        //Check if current state is in the list of states
        //Just an easier way of doing this rather than ORing everything together
        if (OneOfStates(currentState, new state[] { state.Idle, state.Walking}))
        {
            if (Input.GetButtonDown("Attack"))
            {
                ToStateAttacking();
                return;
            }
        }
        if (OneOfStates(currentState, new state[] { state.Idle }))
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            if (Mathf.Abs(inputX) > 0.01f || Mathf.Abs(inputZ) > 0.01f)
            {
                ToStateWalking();
                return;
            }
        }
        if(OneOfStates(currentState, new state[] { state.Walking }))
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            if (Mathf.Abs(inputX) < 0.01f && Mathf.Abs(inputZ) < 0.01f)
            {
                ToStateIdle();
                return;
            }
        }
        if(OneOfStates(currentState, new state[] { state.Attacking}))
        {
            ToStateIdle();
            return;
        }
    }

    public bool OneOfStates(state currentState, state[] possibleStates)
    {
        for(int i = 0; i < possibleStates.Length; i++)
        {
            if(possibleStates[i] == currentState)
            {
                return true;
            }
        }
        return false;
    }

    public void ToStateIdle()
    {
        this.enabled = false;
        GetComponent<StateIdle>().enabled = true;
    }

    public void ToStateWalking()
    {
        this.enabled = false;
        GetComponent<StateWalking>().enabled = true;
    }

    public void ToStateAttacking()
    {
        this.enabled = false;
        GetComponent<StateAttacking>().enabled = true;
    }
}
