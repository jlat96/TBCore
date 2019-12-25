using System;

namespace TrailBlazer.TBOptimizer.State
{
    /// <summary>
    /// Signifies a class that can resolve some evaluation.
    /// </summary>
    /// <typeparam name="TEvaluation">The result type of the evaluation, must be a compartable type</typeparam>
    public interface IEvaluable<TEvaluation> where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Get the evaluation score for the state.
        /// </summary>
        /// <returns>The calculated score</returns>
        TEvaluation GetEvaluation();
    }
}
