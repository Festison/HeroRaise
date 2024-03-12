using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class State
    {
        public IStateMachine sm;
        public event Action onEnter;
        public event Action onExit;
        public virtual void Init(IStateMachine sm)
        {
            this.sm = sm;
        }

        /// <summary>
        /// State Enter Method
        /// </summary>
        public virtual void Enter()
        {
            if(onEnter != null) onEnter();
        }

        /// <summary>
        /// State Update Method
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// State Exit Method
        /// </summary>
        public virtual void Exit()
        {
            if (onExit != null) onExit();
        }
    }
    public class StateMachine<T> : IStateMachine where T : class
    {
        public T owner = null;
        public State curState = null;
        Dictionary<string, State> stateDic = null;
        public StateMachine()
        {
            stateDic = new Dictionary<string, State>();
        }

        /// <summary>
        /// Current State Add Method
        /// </summary>
        /// <param name="name"> Save Dictionary Key name </param>
        /// <param name="state"> State Class Name </param>
        public void AddState(string name, State state)
        {
            if (stateDic.ContainsKey(name)) return;
            state.Init(this);
            stateDic.Add(name, state);
        }

        public object GetOwner()
        {
            return owner;
        }

        /// <summary>
        /// State Change Method 
        /// </summary>
        /// <param name="stateName"> Change State Key Name </param>
        public void SetState(string stateName)
        {
            if (stateDic.ContainsKey(stateName))
            {
                if (curState != null) curState.Exit();
                curState = stateDic[stateName];
                curState.Enter();
            }
        }

        public void Update()
        {
            curState.Update();
        }
    }

}
