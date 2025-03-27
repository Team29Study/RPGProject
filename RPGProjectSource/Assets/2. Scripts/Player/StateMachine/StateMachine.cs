
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
    protected IPlayerState currentState;

    // 상태 변경
    public void ChangeState(IPlayerState state)
    {
        // 같은 상태라면 실행 X
        if (currentState == state) return;

        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }
    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
