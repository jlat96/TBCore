using System;
namespace TrailBlazer.TBOptimizer.Configuration
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException() : base("An object cannot be created because of an invalid configuration")
        {
        }
    }
}
