using System;
using System.Collections.Generic;
using System.Text;

namespace TrailBlazer.TBOptimizer.State
{
    /// <summary>
    /// Represents a member that can generate randomized configurations of a state of type TState that can be evaluated to type TEvaluable
    /// </summary>
    /// <typeparam name="TState">The type of the state to randomize</typeparam>
    /// <typeparam name="TEvaluation">The return type of the stated Evaluate function</typeparam>
    public interface IStateRandomizer<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Returns a legal randomization of the input state
        /// </summary>
        /// <param name="state">The initial state to ranzomize</param>
        /// <returns>The randomized state</returns>
        TState Randomize(TState state);
    }
}
