using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public class GrayscaleParameters : IParameters
    {
        public double Coefficient { get; set; }
        public ParameterInfo[] GetDesсription()
        {
            return new ParameterInfo[0];
        }

        public void Parse(double[] value)
        {
        }
    }
}
