using System;
using System.Collections.Generic;
using TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Configuration
{
    public class ClimberConfiguration<TState, TEvaluation> : IClimberConfiguration<TState, TEvaluation> where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        public IComparer<TEvaluation> EvaluationComparer { get; internal set; }
        public Func<TState,IEnumerable<TState>> SuccessorGenerationFunction { get; internal set; }
        public IClimberAlgorithm<TState, TEvaluation> ClimberAlgorithm { get; private set; } 

        public ClimberConfiguration<TState, TEvaluation> ComparesUsing(IComparer<TEvaluation> comparisonStrategy)
        {
            EvaluationComparer = comparisonStrategy;
            return this;
        }

        /// <summary>
        /// Use a given function to generate successors for any given state.
        /// </summary>
        /// <param name="successorGenerationFunction">The function that crete neighbor states from any given state 'c'/></param>
        /// <returns>The modified configuration</returns>
        public ClimberConfiguration<TState, TEvaluation> GeneratesSuccessorsWith(
            Func<TState, IEnumerable<TState>> successorGenerationFunction)
        {
            SuccessorGenerationFunction = successorGenerationFunction;
            return this;
        }

        /// <summary>
        /// Use a concrete SuccessorGenerator object to generate successor states.
        /// </summary>
        /// <param name="successorGenerator">A successor generator that will create neighbor states for any given state</param>
        /// <returns>The modified configuration</returns>
        public ClimberConfiguration<TState, TEvaluation> GeneratesSuccessorsWith(ISuccessorGenerator<TState, TEvaluation> successorGenerator)
        {
            if (successorGenerator == null)
            {
                throw new ArgumentNullException("SuccessorGenerator cannot be null");
            }
            return GeneratesSuccessorsWith(c => successorGenerator.GetSuccessors(c));
        }

        /// <summary>
        /// Use a pre-existing <see cref="IClimberAlgorithm{TState, TEvaluation}" to build the <see cref="HillClimber{TState, TEvaluation}"/>/>
        /// </summary>
        /// <param name="algorithm">The <see cref="ClimberAlgorithm"/> to Hill Climb with</param>
        /// <returns>Teh modified configuration</returns>
        public ClimberConfiguration<TState, TEvaluation> UsingAlgorithm(IClimberAlgorithm<TState, TEvaluation> algorithm)
        {
            ClimberAlgorithm = algorithm;
            return this;
        }

        /// <summary>
        /// Creates a <see cref="HillClimber{TState, TEvaluation}"/> from the current configuration
        /// </summary>
        /// <returns>The constructed <see cref="HillClimber{TState, TEvaluation}"/></returns>
        /// <exception cref="ConfigurationException">If the configuration is not valid at the time of execution</exception>
        public IHillClimber<TState, TEvaluation> Build()
        {
            if (!IsValid())
            {
                throw new ConfigurationException();
            }

            IClimberAlgorithm<TState, TEvaluation> algorithm;

            // create a climber algorithm if one is not already configured
            if (ClimberAlgorithm == null)
            {
                ClimberSuccessorSelector<TState, TEvaluation> successorSelector = new ClimberSuccessorSelector<TState, TEvaluation>(EvaluationComparer, SuccessorGenerationFunction);
                algorithm = new LocalClimberAlgorithm<TState, TEvaluation>(successorSelector);
            }
            else
            {
                algorithm = ClimberAlgorithm;
            }

            HillClimber<TState, TEvaluation> climber = new HillClimber<TState, TEvaluation>(algorithm);

            return climber;
        }

        private bool IsValid()
        {
            if (ClimberAlgorithm == null)
            {
                return HasRequiredComponents();
            }

            return ClimberAlgorithm != null;
        }

        private bool HasRequiredComponents()
        {
            return (EvaluationComparer != null && SuccessorGenerationFunction != null);
        }
    }
}
