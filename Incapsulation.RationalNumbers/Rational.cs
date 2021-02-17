using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        private int numerator = 0;
        public int Numerator
        {
            get => numerator;
            set => numerator = value;
        }
        private int denominator = 1;
        public int Denominator
        {
            
            get { return denominator; }
            set
            {
                //if (value < 0)
                  //  throw new ArgumentException();
                denominator = value;
            }
        }
        public Rational(int num)
        {
            numerator = num;
            denominator = 1;
        }
        public Rational(int num, int den)
        {
            numerator = num;
            denominator = den;
            Cheak(ref denominator, ref numerator);
            if (numerator == 0 && denominator!=0)
                denominator = 1;
        }

        //перегрузка оператора сложения
        public static Rational operator + (Rational x, Rational y)
        {
            int commonDen;
            int num;
            if (CheakDemoninator(x, y))
            {
                commonDen = x.denominator * y.denominator;
                num = x.numerator * commonDen / x.denominator + y.numerator * commonDen / y.denominator;
            }
            else
            {
                commonDen = 0;
                num = 0;
            }
            Cheak(ref commonDen, ref num);
            return new Rational(num, commonDen);
        }

        

        //перегрузка оператора вычитания
        public static Rational operator -(Rational x, Rational y)
        {
            int commonDen;
            int num;
            if (CheakDemoninator(x, y))
            {
                commonDen = x.denominator * y.denominator;
                num = x.numerator * commonDen / x.denominator - y.numerator * commonDen / y.denominator;
            }
            else
            {
                commonDen = 0;
                num = 0;
            }
            Cheak(ref commonDen, ref num);
            return new Rational(num, commonDen);
        }
        //перегрузка оператора умножения
        public static Rational operator *(Rational x, Rational y)
        {
            int commonDen = x.denominator * y.denominator;
            int num = x.numerator * y.numerator;
            Cheak(ref commonDen, ref num);
            return new Rational(num, commonDen);
        }

        //перегрузка оператора деления
        public static Rational operator /(Rational x, Rational y)
        {
            if(!CheakDemoninator(x,y))
            {
                x.Denominator = 0;
                y.Denominator = 0;
            }
            int commonDen = x.denominator * y.numerator;
            int num = x.numerator * y.denominator;
            Cheak(ref commonDen, ref num);
            return new Rational(num, commonDen);
        }

        public static implicit operator double (Rational r)
        {
            if (r.denominator != 0)
                return (double)r.numerator / (double)r.denominator;
            else
                return double.NaN;
        }

        public static explicit operator int(Rational r)
        {
            if (r.numerator % r.denominator == 0)
                return (int)(r.numerator / r.denominator);
            else
                throw new System.Exception();
        }

        public static implicit operator Rational(int r)
        {
            return new Rational(r);
        }
        public bool IsNan
        {
            get
            {
                if (this.denominator == 0)
                    return true;
                else
                    return false;
            }
        }
        public static void Cheak(ref int commonDen, ref int num)
        {
            if (commonDen < 0)
            {
                num = -num;
                commonDen = -commonDen;
            }
            int minValue = Math.Min(Math.Abs(num), commonDen);
            for (int i=2; i<=minValue;)
            {
                if (Math.Abs(num) % i == 0 && commonDen % i == 0 && commonDen != 0 && num != 0) 
                {
                    num = num / i;
                    commonDen = commonDen / i;
                    minValue = minValue / i;
                    continue;
                }
                i++;
            }
        }
        public static bool CheakDemoninator(Rational one, Rational second)
        {
            return one.Denominator != 0 && second.Denominator != 0;
        }
    }
    
}
