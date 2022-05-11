using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsSorter : MonoBehaviour
{
    [HideInInspector]
    public Vector3 CurrentPoint;

    [SerializeField]
    private PathGenerator _pathGenerator;
    [SerializeField]
    private Transform _cameraTrigger;
    private Vector3[] _sortWayPoints;
    private void Awake()
    {
        _pathGenerator.OnGenerateFinished += Initialize;
    }
    private void OnDestroy()
    {
        _pathGenerator.OnGenerateFinished -= Initialize;
    }
    private void Initialize()
    {
        _sortWayPoints = new Vector3[_pathGenerator.WayPoints.Count];

        for (int i = 0; i < _pathGenerator.WayPoints.Count - 1; i++)
        {
            _sortWayPoints[i] = _pathGenerator.WayPoints[i];
        }

        CurrentPoint = _sortWayPoints[0];

        StartCoroutine(Sort());
    }
    private IEnumerator Sort()
    {
        while (true)
        {
            Vector3 pos;

            for (int i = 0; i < _sortWayPoints.Length; i++)
            {
                for (int j = i + 1; j < _sortWayPoints.Length; j++)
                {
                    if (Vector3.Distance(_cameraTrigger.position, _sortWayPoints[i]) > Vector3.Distance(_cameraTrigger.position, _sortWayPoints[j]))
                    {
                        pos = _sortWayPoints[i];
                        _sortWayPoints[i] = _sortWayPoints[j];
                        _sortWayPoints[j] = pos;
                    }
                }
            }

            CurrentPoint = _sortWayPoints[0];
            yield return null;
        }
    }
   
}
