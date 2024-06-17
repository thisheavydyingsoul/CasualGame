namespace CasualGame.FSM
{
    public abstract class BaseState
    {
        public abstract void Execute();
        public abstract void StartExecuting();
        public abstract void StopExecuting();

    }
}
