using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.Evaluation;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    public class ClimberSuccessorPicker<TState, TEvaluation> : ISuccessorPicker<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation: IComparable<TEvaluation>
    {
        private readonly ISuccessorGenerator<TState, TEvaluation> generator;
        private readonly IComparer<TEvaluation> comparer;

        private readonly ISet<TState> encounteredStates;

        public ClimberSuccessorPicker(ISuccessorGenerator<TState, TEvaluation> generator, IComparer<TEvaluation> comparer)
        {
            this.generator = generator;
            this.comparer = comparer;

            encounteredStates = new HashSet<TState>();
        }

        public TState Next(TState current)
        {
            TState bestSuccessor = generator.GetSuccessors(current).Extrema(comparer);

            if (comparer.Compare(current.GetEvaluation(), bestSuccessor.GetEvaluation()) <= 0 ||
                encounteredStates.Contains(bestSuccessor))
            {
                return current;
            }

            encounteredStates.Add(bestSuccessor);
            return bestSuccessor;
        }
    }
}
