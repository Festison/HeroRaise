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
        /// 상태머신 반환 함수
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
        /// 상태에 진입할 때,
        /// </summary>
        public virtual void Enter()
        {
            if(onEnter != null) onEnter();
        }

        /// <summary>
        /// 상태를 유지할 때,
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// 상태가 종료될 때,
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
        /// 현재 상태머신에 상태 추가 함수
        /// </summary>
        /// <param name="name"> 저장할 키 값 </param>
        /// <param name="state"> 상태 클래스 이름 </param>
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
        /// 상태 변경 함수
        /// </summary>
        /// <param name="stateName"> 변경할 상태의 키 값 </param>
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
