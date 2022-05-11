using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dreamteck.Splines;

public class PathGenerator : MonoBehaviour
{

    [HideInInspector]
    public List<Vector3> WayPoints;
    public event Action OnStartGenerate;
    public event Action OnGenerateFinished;

    [SerializeField]
    private PathGenerationUtility _utility;
    [SerializeField]
    private SplineComputer _splineComputer;
    [SerializeField]
    private float _distanceBtwPoints;

    private Vector3 _previousPoint, _currentPoint;

    public void GenerateSpline()
    {
        StopAllCoroutines();

        _splineComputer.type = Spline.Type.BSpline;

        SplinePoint[] splinePoints = new SplinePoint[WayPoints.Count];

        for (int i = 0; i < WayPoints.Count - 1; i++)
        {
            splinePoints[i].SetPosition(WayPoints[i]);
        }

        _splineComputer.SetPoints(splinePoints);
    }
    private void Start()
    {
        OnStartGenerate?.Invoke();

        WayPoints = new List<Vector3>();

        StartCoroutine(GeneratePointsForSpline());

        _currentPoint = _utility.transform.position;
        _previousPoint = _currentPoint;

    }


    private IEnumerator GeneratePointsForSpline()
    {
        while (true)
        {
            _currentPoint = _utility.transform.position;

            if (Vector3.Distance(_currentPoint, _previousPoint) >= _distanceBtwPoints)
            {
                _previousPoint = _currentPoint;
                WayPoints.Add(_currentPoint);
            }

            yield return null;

        }
    }
    
}
