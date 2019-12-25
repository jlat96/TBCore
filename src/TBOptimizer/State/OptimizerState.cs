using System;
using TrailBlazer.TBOptimizer.State;

namespace Trailblazer.TBOptimizer.State
{
    public abstract class OptimizerState<TEvaluation> : EvaluableState<OptimizerState<TEvaluation>, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected OptimizerState() { }
    }
}
