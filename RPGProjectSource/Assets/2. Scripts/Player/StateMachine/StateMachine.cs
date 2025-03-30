
// 플레이어 상태들이 받을 상태 인터페이스
public interface IPlayerState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

public abstract class StateMachine
{
    // 현재 상태
    //protected IPlayerState currentState;
    protected IPlayerState currentMovementState;
    protected IPlayerState currentAttackState;

    // 상태 변경
    //public void ChangeState(IPlayerState state)
    //{
    //    currentState?.Exit();
    //    currentState = state;
    //    currentState?.Enter();
    //}
    public void ChangeMovementState(IPlayerState state)
    {
        currentMovementState?.Exit();
        currentMovementState = state;
        currentMovementState?.Enter();
    }
    public void ChangeAttackState(IPlayerState state)
    {
        currentAttackState?.Exit();
        currentAttackState = state;
        currentAttackState?.Enter();
    }

    public void HandleInput()
    {
        currentMovementState?.HandleInput();
        currentAttackState?.HandleInput();
    }

    public void Update()
    {
        currentMovementState?.Update();
        currentAttackState?.Update();
    }
    public void PhysicsUpdate()
    {
        currentMovementState?.PhysicsUpdate();
        currentAttackState?.PhysicsUpdate();
    }

    public bool IsGroundState()
    {
        return (currentMovementState is PlayerGroundState);
    }
}
