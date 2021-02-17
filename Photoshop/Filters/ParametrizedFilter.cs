using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public abstract class ParametrizedFilter : IFilter
    {
        IParameters parameters;
        public ParametrizedFilter(IParameters parameters)
        {
            this.parameters = parameters;
        }
        public ParameterInfo[] GetParameters()
        {
            return parameters.GetDesсription();
        }

        public Photo Process(Photo original, double[] value)
        {
            this.parameters.Parse(value);
            return Process(original, this.parameters);
        }

        public abstract Photo Process(Photo original, IParameters parameters);
    }
}
