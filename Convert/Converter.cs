using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Convert
{
    public class Converter
    {
        private static double angle = (Math.PI / 180) * -32.7;
        public static double runwayX = 1085.33;

        public static double NmToMeters(double nm)
        {
            return nm * 1852;
        }

        public static double MetersToNm(double m)
        {
            return m / 1852;
        }

        public static double FeetToM(double feet)
        {
            return feet * 0.3048 * 0.1;
        }

        public static double MtoFeet(double m)
        {
            return m / 0.3048;
        }

        public static double ConvertX(double x)
        {
            return NmToMeters(x / 10);
        }

        public static double ConvertY(double x)
        {
            return NmToMeters(x / 10);
        }

        public static Vector3 CalculatePosition(Vector3 pos)
        {
            var x = ConvertX(pos.z);
            var y = ConvertY(pos.x);

            var rotatedX = -5917.685 + x * Math.Cos(angle) - y * Math.Sin(angle);
            var rotatedY = 12434.355 + x * Math.Sin(angle) + y * Math.Cos(angle);

            return new Vector3((float)rotatedY, 0, (float)rotatedX);
        }
    }
}
