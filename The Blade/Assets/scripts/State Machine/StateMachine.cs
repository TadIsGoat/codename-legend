public class StateMachine
{
    public State state;

    public void Set(State newState, bool forceReset = false)
    {
        if (newState != state || forceReset)
        {
            state?.Exit();
            state = newState;
            state.Initialize();
            state.Enter();
        }
    }
}