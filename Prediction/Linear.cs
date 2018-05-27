using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Prediction
{
    public class Linear
    {
        IList<Coordinate> lastCoordinates = new List<Coordinate>();
        Dictionary<string, double> values = new Dictionary<string, double>();

        public Linear(IList<Coordinate> lastCoordinates)
        {
            this.lastCoordinates = lastCoordinates;
        }

        public Dictionary<string, double> Update(Coordinate c)
        {
            var latest = GetLatest();
            var coordX = lastCoordinates.Select(cord => cord.x);
            var coordY = lastCoordinates.Select(cord => cord.y);
            var coordE = lastCoordinates.Select(cord => cord.elav);

            var dx = (c.x - latest.x) * CalculateCoef(coordX, -1);
            var dy = (c.y - latest.y) * CalculateCoef(coordX, -1);
            var de = (c.elav - latest.elav) * CalculateCoef(coordX, -1);

            values["dx"] = dx;
            values["dy"] = dy;
            values["de"] = de;

            return values;

        }

        private double CalculateCoef(IEnumerable<double> prevCoord, int take)
        {
            Double prevVal = 0;
            var deltaSum = 0d;

            if (take != -1)
            {
                prevCoord = prevCoord.Skip(Math.Max(0, prevCoord.Count() - take));
            }
            else
            {
                take = prevCoord.Count();
            }


            foreach (var val in prevCoord)
            {
                if (prevVal == 0)
                {
                    prevVal = val;
                    continue;
                }

                deltaSum += val / prevVal;
            }

            var deltaAvg = deltaSum / ((take - 1));

            return deltaAvg;
        }

        private Coordinate GetLatest()
        {
            if (lastCoordinates.Count > 0)
            {
                return lastCoordinates.Last();
            }

            return null;
        }
    }
}
