using System;
using System.Collections.Generic;
using System.Linq;

namespace Inheritance.Geometry.Virtual
{
    abstract public class Body
    {
        public Vector3 Position { get; }

        protected Body(Vector3 position)
        {
            Position = position;
        }

        public abstract bool ContainsPoint(Vector3 point);

        public abstract RectangularCuboid GetBoundingBox();
        public abstract void Accept(IVisitor visitor);
    }

    public interface IVisitor
    {
        void Visit(Ball ball);
        void Visit(RectangularCuboid cuboid);
        void Visit(Cylinder cylinder);
        void Visit(CompoundBody body);
    }

    public class Ball : Body
    {
        public double Radius { get; }

        public Ball(Vector3 position, double radius) : base(position)
        {
            Radius = radius;
        }

        public override bool ContainsPoint(Vector3 point)
        {   
            var vector = point - Position;
            var length2 = vector.GetLength2();
            return length2 <= Radius * Radius;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, Radius * 2, Radius * 2, Radius * 2);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class RectangularCuboid : Body
    {
        public double SizeX { get; }
        public double SizeY { get; }
        public double SizeZ { get; }

        public RectangularCuboid(Vector3 position, double sizeX, double sizeY, double sizeZ) : base(position)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            var minPoint = new Vector3(
                    Position.X - SizeX / 2,
                    Position.Y - SizeY / 2,
                    Position.Z - SizeZ / 2);
            var maxPoint = new Vector3(
                Position.X + SizeX / 2,
                Position.Y + SizeY / 2,
                Position.Z + SizeZ / 2);

            return point >= minPoint && point <= maxPoint;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, SizeX, SizeY, SizeZ);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Cylinder : Body
    {
        public double SizeZ { get; }

        public double Radius { get; }

        public Cylinder(Vector3 position, double sizeZ, double radius) : base(position)
        {
            SizeZ = sizeZ;
            Radius = radius;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            var vectorX = point.X - Position.X;
            var vectorY = point.Y - Position.Y;
            var length2 = vectorX * vectorX + vectorY * vectorY;
            var minZ = Position.Z - SizeZ / 2;
            var maxZ = minZ + SizeZ;

            return length2 <= Radius * Radius && point.Z >= minZ && point.Z <= maxZ;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, Radius * 2, Radius * 2, SizeZ);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class CompoundBody : Body
    {
        public IReadOnlyList<Body> Parts { get; }

        public CompoundBody(IReadOnlyList<Body> parts) : base(parts[0].Position)
        {
            Parts = parts;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            return Parts.Any(body => body.ContainsPoint(point));
        }

        public override RectangularCuboid GetBoundingBox()
        {
            IEnumerable<RectangularCuboid> cuboids = Parts.Select(cub => cub.GetBoundingBox());
            Vector3 min = Position;
            Vector3 max = Position;
            foreach(var c in cuboids)
            {
                min = vector.GetMinVector(min, c);
                max = vector.GetMaxVector(max, c);
            }
            Vector3 posit = new Vector3((min.X + max.X) / 2, (min.Y + max.Y) / 2, (min.Z + max.Z) / 2);
            Vector3 size = max - min;
            return new RectangularCuboid(posit, size.X, size.Y, size.Z);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    public class vector
    {
        public static Vector3 GetMinVector(Vector3 a, RectangularCuboid b)
        {
            //find min point in object B
            Vector3 minPointObjectB = b.Position - new Vector3(b.SizeX / 2, b.SizeY / 2, b.SizeZ / 2);
            //check if the point is below and to the left of point a
            Vector3 vector = a - minPointObjectB;
            //if yes, set the coordinate of the minimum point else leave the coordinates a
            return new Vector3(vector.X > 0 ? minPointObjectB.X : a.X,
                              vector.Y > 0 ? minPointObjectB.Y : a.Y,
                              vector.Z > 0 ? minPointObjectB.Z : a.Z);
        }
        public static Vector3 GetMaxVector(Vector3 a, RectangularCuboid b)
        {
            //find max point in object B
            Vector3 maxPointObjectB = b.Position + new Vector3(b.SizeX / 2, b.SizeY / 2, b.SizeZ / 2);
            //check if the point is above and to the right of point a
            Vector3 vector = maxPointObjectB - a;
            //if yes, set the coordinate of the max point else leave the coordinates a
            return new Vector3(vector.X > 0 ? maxPointObjectB.X : a.X,
                              vector.Y > 0 ? maxPointObjectB.Y : a.Y,
                              vector.Z > 0 ? maxPointObjectB.Z : a.Z);
        }
    }
    public class DimensionsVisitor : IVisitor
    {
        public (double, double) Dimensions { get; private set; }
        public void Visit(Ball ball)
        {
            Dimensions = (ball.Radius * 2, ball.Radius * 2);
        }

        public void Visit(RectangularCuboid cuboid)
        {
            Dimensions = (cuboid.SizeX, cuboid.SizeZ);
        }

        public void Visit(Cylinder cylinder)
        {
            Dimensions = (cylinder.Radius * 2, cylinder.SizeZ);
        }

        public void Visit(CompoundBody body)
        {
            Dimensions = (body.GetBoundingBox().SizeX, body.GetBoundingBox().SizeZ);
        }
    }

    public class SurfaceAreaVisitor : IVisitor
    {
        public double SurfaceArea { get; private set; }
        public void Visit(Ball ball)
        {
            SurfaceArea = 4.0 * Math.PI * ball.Radius * ball.Radius;
        }

        public void Visit(RectangularCuboid cuboid)
        {
            SurfaceArea = 2.0 * (cuboid.SizeZ * cuboid.SizeY + cuboid.SizeX * cuboid.SizeY + cuboid.SizeX * cuboid.SizeZ);
        }

        public void Visit(Cylinder cylinder)
        {
            SurfaceArea = 2.0 * Math.PI * cylinder.SizeZ * cylinder.Radius + 2.0 * Math.PI * cylinder.Radius * cylinder.Radius;
        }

        public void Visit(CompoundBody body)
        {
            List<Body> parts = new List<Body>();
            foreach (var b in body.Parts)
                b.Accept(new SurfaceAreaVisitor());

        }
    }
}