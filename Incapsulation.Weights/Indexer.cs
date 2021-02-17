using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
	public class Indexer
    {
        double[] range;
        int start = 0;
        int length = 0;
        public int Length
        {
            get => length;
        }
        public Indexer(double[] range, int start, int length)
        {
            if (range.Length >= (length + start) && start >= 0 && length >= 0)
            {
                this.length = length;
                this.start = start;
                this.range = range;
            }
            else
                throw new ArgumentException();
        }
        public double this [int index]
        {
            get
            {
                if ((index+start) <= length && index>=0)
                    return range[index+start];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if ((index+start) <= length && index >= 0)
                    range[index+start] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
