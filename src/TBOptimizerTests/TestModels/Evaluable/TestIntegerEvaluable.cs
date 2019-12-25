using System;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.Evaluable
{
    public class TestIntegerEvaluable : IEvaluable<int>
    {
        public TestIntegerEvaluable(int sequence, int value)
        {
            SequenceNumber = sequence;
            Value = value;
        }

        public int SequenceNumber { get; set; }
        public int Value { get; set; }

        public int GetEvaluation()
        {
            return Value;
        }
    }
}
