using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public abstract class ParametrizedFilter<Tparams> : IFilter
                            where Tparams : IParameters, new()
    {
        public ParameterInfo[] GetParameters()
        {
            return new Tparams().GetDesсription();
        }

        public Photo Process(Photo original, double[] value)
        {
            var parameters = new Tparams();
            parameters.Parse(value);
            return Process(original, parameters);
        }

        public abstract Photo Process(Photo original, Tparams parameters);
    }
}
