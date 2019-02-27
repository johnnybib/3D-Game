using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttacking : BaseState
{
    private state ThisState = state.Attacking;
    private bool Attacking = false;

    void OnEnable()
    {
        Debug.Log("Attacking");
        //Start animation
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Still Attacking");
        if(!Attacking)
        {
            _playerCon.StartAttack();
            Attacking = true;
            StartCoroutine("WaitForAttack");
        }
    }

    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(1.0f);
        Attacking = false;
        _playerCon.StopAttack();
        CheckStateChange(ThisState);
    }
}
