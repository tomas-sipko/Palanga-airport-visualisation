using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EventManager: MonoBehaviour
    {
        public delegate void FlightAdded();
        public static event FlightAdded OnFlightAdded;

        public delegate void FlightEnded(string flightName);
        public static event FlightEnded OnFlightEnded;

        public delegate void CoordinateSent(Coordinate coordinate);
        public static event CoordinateSent OnCoordinateSent;

        public delegate void SpeedChanged(float waitTime);
        public static event SpeedChanged onSpeedChanged;

        public static void publishOnCoordinateSentEvent(Coordinate coord)
        {
            if (OnCoordinateSent != null)
            {
                OnCoordinateSent(coord);

            }
        }

        public static void publishOnFlightAdded()
        {
            if (OnFlightAdded != null)
            {
                OnFlightAdded();

            }
        }

        public static void publishOnFlightEnded(string flightName)
        {
            if (OnFlightEnded != null)
            {
                OnFlightEnded(flightName);

            }
        }

        public static void publishOnSpeedChange(float waitTime)
        {
            if (onSpeedChanged != null)
            {
                onSpeedChanged(waitTime);

            }
        }
    }
}
