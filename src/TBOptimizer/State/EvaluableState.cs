using System;
using System.Diagnostics.CodeAnalysis;
using TBUtils.Types;

namespace TrailBlazer.TBOptimizer.State
{
    /// <summary>
    /// Represents an evaluable that will lazily produce an evaluation.
    /// </summary>
    /// <typeparam name="TState">THe type of the state being evaluated</typeparam>
    /// <typeparam name="TEvaluation">The return type of the evaluation, must be IComparable</typeparam>
    public abstract class EvaluableState<TState, TEvaluation> : IEvaluable<TEvaluation>, IComparable<TState>, ITypedClonable<TState>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        private Lazy<TEvaluation> _evaluation;

        protected EvaluableState()
        {
            _evaluation = new Lazy<TEvaluation>(() => Evaluate());
        }

        /// <summary>
        /// The evaluation score for this state. If the evaluation has not yet been completed, it will be computed and stored.
        /// </summary>
        protected TEvaluation Evaluation {
            get => _evaluation.Value;
        }

        public abstract TState Clone();

        /// <summary>
        /// Compares this evaluable to another evaluable.
        /// </summary>
        /// <param name="other">The evaluable to compare with</param>
        /// <returns> negative if this is less than other, 0 if this and other are equal, positive if this is greater than other</returns>
        public virtual int CompareTo([AllowNull] TState other)
        {
            return GetEvaluation().CompareTo(other.GetEvaluation());
        }

        /// <summary>
        /// Gets the evaluation of this state. Calculation will occur if the evaluation has not yet been performed.
        /// </summary>
        /// <returns>The evaluation of this state</returns>
        public virtual TEvaluation GetEvaluation()
        {
            return Evaluation;
        }

        /// <summary>
        /// Calculate the evaluation score for this state.
        /// </summary>
        /// <returns></returns>
        protected abstract TEvaluation Evaluate();
    }
}
