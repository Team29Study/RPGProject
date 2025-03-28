using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedCombo;
    private bool alreadyApplyForce;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        alreadyAppliedCombo = false;
        alreadyApplyForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfoData(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if(!alreadyAppliedCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");

        // 애니메이션이 끝나지 않았다면
        if(normalizedTime < 1f)
        {
            // 콤보를 진행할 수 있는 임계시간이 끝났다면
            if(normalizedTime >= attackInfoData.ComboTransitionTime)
            {
                TryComboAttack();
            }

            if(normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                TryApplyForce();
            }
        }
        else
        {
            // 애니메이션이 끝났고 콤보가 진행 중이라면
            if(alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                // 다시 콤보 어택 상태로 만들어 줌
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            // 콤보가 진행 중이지 않다면
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    private void TryComboAttack()
    {
        Debug.Log(alreadyAppliedCombo);
        Debug.Log("공격 중인가?" + stateMachine.IsAttacking);

        // 이미 콤보가 진행 중이라면 실행 X
        if (alreadyAppliedCombo) return;
        
        // 콤보의 마지막 공격이라면 실행 X
        if (attackInfoData.ComboStateIndex == -1) return;

        // 공격하지 않는 경우
        if (!stateMachine.IsAttacking) return;

        alreadyAppliedCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyApplyForce) return;
        alreadyApplyForce = true;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }
}
