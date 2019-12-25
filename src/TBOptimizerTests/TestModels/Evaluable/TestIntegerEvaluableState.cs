using System;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.Evaluable
{
    public class TestIntegerEvaluableState : EvaluableState<TestIntegerEvaluableState, int>
    {
        public TestIntegerEvaluableState(int value)
        {
            Value = value;
            TimesEvaluated = 0;
        }

        public int TimesEvaluated { get; set; }
        public int Value { get; set; }

        public override TestIntegerEvaluableState Clone()
        {
            return new TestIntegerEvaluableState(Value);
        }

        protected override int Evaluate()
        {
            TimesEvaluated++;
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
