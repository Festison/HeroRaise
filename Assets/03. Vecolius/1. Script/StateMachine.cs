using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public interface IStateMachine
    {
        void SetState(string stateName);

        /// <summary>
        /// ���¸ӽ� ��ȯ �Լ�
        /// </summary>
        /// <returns></returns>
        object GetOwner();
    }
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
        /// ���¿� ������ ��,
        /// </summary>
        public virtual void Enter()
        {
            if(onEnter != null) onEnter();
        }

        /// <summary>
        /// ���¸� ������ ��,
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// ���°� ����� ��,
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
        /// ���� ���¸ӽſ� ���� �߰� �Լ�
        /// </summary>
        /// <param name="name"> ������ Ű �� </param>
        /// <param name="state"> ���� Ŭ���� �̸� </param>
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
        /// ���� ���� �Լ�
        /// </summary>
        /// <param name="stateName"> ������ ������ Ű �� </param>
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
