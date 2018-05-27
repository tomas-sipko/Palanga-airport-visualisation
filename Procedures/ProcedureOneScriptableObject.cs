using Assets.Scripts.Procedures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ProcedureOneScriptableObject : ScriptableObject {
    public double descentAngle = 2.98;
    public double fafHeight = 1300;
    public double fafDistance = 4;
    public string procedureName;
    public Vector3[] controlPoints;
}
