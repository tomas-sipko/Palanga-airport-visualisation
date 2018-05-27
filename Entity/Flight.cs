using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Entity
{
    public class Flight
    {
        public Flight()
        {
            coordinates = new List<Coordinate> ();
        }

        public Flight(string name, DateTime date) : this()
        {
            this.name = name;
            this.date = date;
        }

        public string name { get; set; }

        public DateTime date { get; set; }

        public IList<Coordinate> coordinates { get; set; }
    }
}
