using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
	public struct Pixel
	{
		public Pixel(double r, double g, double b)
        {
			this.r = this.b = this.g = 0;
			this.R = r;
			this.G = g;
			this.B = b;
        }
		private double CheackDouble(double value)
		{
			if (value > 1 || value < 0)
				throw new ArgumentException();
			return value;
		}
		public static double Trim(double value)
        {
			if (value > 1) return 1;
			if (value < 0) return 0;
			return value;
        }


		private double r;
		public double R
		{
			get { return r; }
			set { r = CheackDouble(value); }
		}

		private double g;
		public double G
		{
			get { return g; }
			set { g = CheackDouble(value); }
		}

		private double b;
		public double B
		{
			get { return b; }
			set{ b = CheackDouble(value); }
		}
		public static Pixel operator *(Pixel pixel, double vector)
        {
			return new Pixel(Pixel.Trim(pixel.R * vector), Pixel.Trim(pixel.G * vector), Pixel.Trim(pixel.B * vector));
        }
		public static Pixel operator *(double vector, Pixel pixel)
		{
			return new Pixel(Pixel.Trim(pixel.R * vector), Pixel.Trim(pixel.G * vector), Pixel.Trim(pixel.B * vector));
		}
		
	}
}
