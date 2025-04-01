using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BlockStateParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.BlockParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BlockStateParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.BlockParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if(!stateMachine.IsBlocking)
        {
            stateMachine.ChangeAttackState(stateMachine.NoneAttackState);
            return;
        }
    }
}
