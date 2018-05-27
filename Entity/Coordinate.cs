using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Entity
{
    public class Coordinate
    {
        public Coordinate(Flight flightName, double x, double y, double elav, DateTime date)
        {
            this.flight = flightName;
            this.x = x;
            this.y = y;
            this.elav = elav;
            this.date = date;
        }

        public Flight flight { get; set; }

        public double x { get; set; }

        public double y { get; set; }

        public double elav { get; set; }

        public DateTime date { get; set; }


    }
}
