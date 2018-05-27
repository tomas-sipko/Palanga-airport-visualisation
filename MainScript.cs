using Assets.Scripts.Sender;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Entity;
using System;
using Assets.Scripts.Convert;
using Assets.Scripts;

public class MainScript : MonoBehaviour {

    private static MainScript _instance;

    public FileReader fileReader;
    public CoordinateSender coordinateSender;
    public GameObject trackBegining;
    public GameObject trackEnd;
    public GameObject plane;
    public GameObject camera;
    public GameObject sidePlane;
    public GameObject frontPlane;
    public List<int> separatorHeights = new List<int>();
    public TextAsset textAsset;

    private List<Flight> flights = new List<Flight>();
    private List<GameObject> activePlanes = new List<GameObject>();


    public static MainScript Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("InitScript");
                go.AddComponent<MainScript>();
            }

            return _instance;
        }
    }

    private void Awake()
    {

        _instance = this;
    }


    // Use this for initialization
    void Start() {
        Assets.Scripts.EventManager.OnCoordinateSent += onCoordinate;
        Assets.Scripts.EventManager.OnFlightEnded += OnFlightEnd;

        Converter.runwayX = trackBegining.transform.position.x - 2;

        createVerticalSeparators();
        var dist = Vector3.Distance(trackBegining.transform.position, trackEnd.transform.position);

        var coordinates = fileReader.readFile(textAsset);
        coordinateSender.sendCoordiantes(coordinates.First().coordinates);
    }

    private void OnFlightEnd(string flightName)
    {
        var planeToRemove = activePlanes.Find(p => p.GetComponent<PlaneBehaviour>().GetFlightName() == flightName);
        ResetCammera();

        if (planeToRemove == null)
        {
            return;
        }

        activePlanes.Remove(planeToRemove);
    }

    private void onCoordinate(Coordinate coordinate)
    {
        if (flights.Contains(coordinate.flight))
        {
            return;
        }
        var pos = Converter.CalculatePosition(new Vector3((float)coordinate.y, 0, (float)coordinate.x));
        var planeTransform = new Vector3(pos.x, (float)Converter.FeetToM(coordinate.elav), pos.z);
        GameObject planeClone = Instantiate(plane);
        planeClone.GetComponent<PlaneBehaviour>().AddFlight(coordinate.flight);
        planeClone.transform.position = planeTransform;

        flights.Add(coordinate.flight);
        activePlanes.Add(planeClone);

        EventManager.publishOnFlightAdded();
    }

    public void FocusCamera(string flightName)
    {
        var plane = activePlanes.Find(p => p.GetComponent<PlaneBehaviour>().GetFlightName() == flightName);
        camera.GetComponent<CameraBehaviour>().target = plane.transform;

    }

    public void ResetCammera()
    {
        camera.GetComponent<CameraBehaviour>().Reset();

    }

    private void createVerticalSeparators()
    {
        separatorHeights.ForEach(t =>
        {

            var separator = Resources.Load("VerticalSeparator") as GameObject;
            var separatorObj = Instantiate(separator);

            var sepBehaviour = separatorObj.GetComponent<SeparatorBehaviour>();
            sepBehaviour.SetHeight(t);
            sepBehaviour.sidePlain = sidePlane;
            sepBehaviour.frontPlain = frontPlane;

        });
    }

    public IList<Flight> GetFlights()
    {
        return flights;
    }

}
