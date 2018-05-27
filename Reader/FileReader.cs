using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts;

public class FileReader : MonoBehaviour
{

    public ICollection<Flight> readFile(TextAsset text)
    {
        try
        {
            var result = new List<Flight>();
            string[] lines = text.text.Split(Environment.NewLine.ToArray());

            var linesList = new List<string>(lines);
            lines = linesList.Where(t => !string.IsNullOrEmpty(t)).ToArray();

            foreach (var line in lines)
            {
                var parts = line.Split(" ".ToArray()).Where(t => !String.IsNullOrEmpty(t)).ToArray();
                var flight = readFlight(parts);
                flight.coordinates = flight.coordinates.Where(c => c.elav <= 4000).ToList();
                result.Add(flight);
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could not read the file");
        }

    }

    private Flight readFlight(string[] parts)
    {
        if (string.IsNullOrEmpty(parts[0]))
        {
            throw new ArgumentException("invalid string. Cannot read flight info");
        }

        var flight = readFlightInfoString(parts[0]);

        for (int i = 2; i < parts.Length;)
        {
            flight.coordinates.Add(readCoordinate(parts[i], parts[++i], parts[++i], flight));
            i++;
        }

        return flight;

    }

    private Coordinate readCoordinate(string x, string y, string elev, Flight flight)
    {


        return new Coordinate(flight, parseCoordinate(x), parseCoordinate(y), parseElavation(elev), parseCoordinateTime(elev, flight.date));
    }

    private DateTime parseCoordinateTime(string elev, DateTime flightDate)
    {
        var elavStr = elev.Substring(elev.Length - 6);
        var time = readTime(elavStr);

        var result = new DateTime(flightDate.Year, flightDate.Month, flightDate.Day, time[0], time[1], time[2]);

        return result;
    }

    private double parseElavation(string elav)
    {
        var elavStr = elav.Substring(0, elav.Length - 6);

        return double.Parse(elavStr) * 100;
    }

    private double parseCoordinate(string x)
    {
        var xLength = x.Length;
        var xPart = double.Parse(x.Substring(0, xLength - 2));

        if (xPart > 0)
        {
            return xPart + double.Parse(x.Substring(xLength - 2, 2)) / 100;

        }

        return xPart - double.Parse(x.Substring(xLength - 2, 2)) / 100;

    }

    private Flight readFlightInfoString(string flightinfo)
    {
        var flightName = flightinfo.Substring(14, 7);
        var date = flightinfo.Substring(0, 8);

        var dateObj = new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6, 2)));

        return new Flight(flightName, dateObj);
    }

    private int[] readTime(string time)
    {
        try
        {
            return new int[3] { int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(2, 2)), int.Parse(time.Substring(4, 2)) };
        }
        catch (Exception e)
        {
            throw new ArgumentException("Could not read time in readTime() method. time: " + time);
        }
    }
}