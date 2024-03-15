using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public interface IStateMachine
    {
        /// <summary>
        /// State Change IMethod
        /// </summary>
        /// <param name="stateName"> Change State key Name </param>
        void SetState(string stateName);

        /// <summary>
        /// StateMachine return IMethod
        /// </summary>
        /// <returns> StateMachine Parameter return </returns>
        object GetOwner();
    }
    public interface IHitable
    {
        /// <summary>
        /// Character Hit IMethod
        /// </summary>
        /// <param name="damage"> Get Damage </param>
        void Hit(int damage);
    }
    public interface IAttackable
    {
        void Attack(IHitable hitable);
    }
}
