using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public static class Utils
    {
        public static string FormatFloatDisplay(double f)
        {
            string sign = "+";

            if (f < 0f)
            {
                sign = "-";
            }

            double abs = Math.Abs(f);
            return sign + abs.ToString("F3");
        }

        public static string FormatDoubleDisplay(double d, int dec)
        {
            string sign = "+";

            if (d < 0)
            {
                sign = "-";
            }
            double abs = Math.Abs(d);
            string format = "F5";
            switch (dec)
            {
                case 1:
                    format = "F1";
                    break;
                case 2:
                    format = "F2";
                    break;
                case 3:
                    format = "F3";
                    break;
                case 4:
                    format = "F4";
                    break;
                case 5:
                    format = "F5";
                    break;
                default:
                    break;
            }
            return sign + abs.ToString("F3");
        }

        public static string GetEnumString(Enum value)
        {
            //return "Not Implemented";
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static bool PointInTriangle(CNCPoint p, CNCPoint p0, CNCPoint p1, CNCPoint p2)
        {
            var s = p0.y * p2.x - p0.x * p2.y + (p2.y - p0.y) * p.x + (p0.x - p2.x) * p.y;
            var t = p0.x * p1.y - p0.y * p1.x + (p0.y - p1.y) * p.x + (p1.x - p0.x) * p.y;

            if ((s < 0) != (t < 0))
                return false;

            var A = -p1.y * p2.x + p0.y * (p2.x - p1.x) + p0.x * (p1.y - p2.y) + p1.x * p2.y;
            if (A < 0.0)
            {
                s = -s;
                t = -t;
                A = -A;
            }
            return s > 0 && t > 0 && (s + t) < A;
        }
    }

    public class CNCPlane
    {
        private double a;

        private double b;

        private double c;

        private double d;

        public CNCPlane(CNCPoint pt1, CNCPoint pt2, CNCPoint pt3)
        {
            CNCVector vt1 = new CNCVector(pt1, pt2);
            CNCVector vt2 = new CNCVector(pt1, pt3);

            CNCVector perpVt = CNCVector.GetPerpendicularVector(vt1, vt2);

            this.a = perpVt.a;
            this.b = perpVt.b;
            this.c = perpVt.c;

            this.d = pt1.x * a + pt1.y * b + pt1.z * c;
        }

        public double GetZOfPoint(double x, double y)
        {
            return (this.d - a * x - b * y) / c;
        }

    }

    public class CNCVector
    {
        public double a, b, c;

        public CNCVector(CNCPoint ptA, CNCPoint ptB)
        {
            a = ptB.x - ptA.x;
            b = ptB.y - ptA.y;
            c = ptB.z - ptA.z;
        }

        public CNCVector(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public static CNCVector GetPerpendicularVector(CNCVector vt1, CNCVector vt2)
        {
            double a = vt1.b * vt2.c - vt1.c * vt2.b;
            double b = -(vt1.a * vt2.c - vt1.c * vt1.a);
            double c = vt1.a * vt2.b - vt1.b * vt2.a;

            return new CNCVector(a, b, c);
        }
    }

    public class CNCPoint
    {
        public double x;

        public double y;

        public double z;

        public int idx;

        public bool isZSet = false;

        public int row;
        public int col;

        public bool HasScanned
        {
            get; set;
        }
        public string ToString()
        {
            return "{" + x + "," + y + "," + z + "}";
        }
        public CNCPoint(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public CNCPoint(string xStr, string yStr, string zStr)
        {
            this.x = double.Parse(xStr);
            this.y = double.Parse(yStr);
            this.z = double.Parse(zStr);
        }

        public CNCPoint MiddlePointXY(CNCPoint pt)
        {
            double x = (pt.x + this.x) / 2;
            double y = (pt.y + this.y) / 2;
            return new CNCPoint(x, y, 0);
        }
        public double DistXY(CNCPoint pt)
        {
            return Math.Sqrt(Math.Pow(pt.x - this.x, 2) + Math.Pow(pt.y - this.y, 2));
        }

        public double DistXYZ(CNCPoint pt)
        {
            return Math.Sqrt(Math.Pow(pt.x - this.x, 2) + Math.Pow(pt.y - this.y, 2) + Math.Pow(pt.z - this.z, 2));
        }


    }

    public class Point2D
    {
        public double x;

        public double y;

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsInTriangle(Point2D pt1, Point2D pt2, Point2D pt3)
        {
            bool b1, b2, b3;
            b1 = Sign(this, pt1, pt2) < 0;
            b2 = Sign(this, pt2, pt3) < 0;
            b3 = Sign(this, pt3, pt1) < 0;

            return ((b1 == b2) && (b2 == b3));
        }

        private static double Sign(Point2D p1, Point2D p2, Point2D p3)
        {
            return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
        }
    }
}
