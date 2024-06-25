using CasualGame.Exceptions;
using System.Collections.Generic;
using UnityEngine;

namespace CasualGame.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentState;

        protected List<BaseState> _states;
        private Dictionary<BaseState, List<Transition>> _transitions;

        public BaseStateMachine()
        {
            _states = new List<BaseState>();
            _transitions = new Dictionary<BaseState, List<Transition>>();
        }

        public void SetInitialState(BaseState state)
        {
            _currentState = state; 
        }

        public void AddState(BaseState state, List<Transition> transitions)
        {
            if (_states.Contains(state)) throw new AlreadyExistsException($"State {state.GetType()} already exists in state machine!");
            _states.Add(state);
            _transitions.Add(state, transitions);
        }

        public void Update()
        {
            foreach(var transition in _transitions[_currentState])
                if (transition.Condition())
                {
                    _currentState.StopExecuting();
                    _currentState = transition.ToState;
                    _currentState.StartExecuting();
                    break;
                }
            _currentState.Execute();
        }
    }
}
