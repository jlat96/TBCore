using System;
namespace TrailBlazer.TBOptimizer.Randomizer
{
    public class RandomizerOptions
    {
        public RandomizerOptions()
        {
        }

        public int RandomizationAttempts { get; set; } = -1;
    }

    public enum RandomizationMode
    {
        NUMERIC, TIMEOUT, INFINITE
    }

}
