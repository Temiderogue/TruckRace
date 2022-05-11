using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class PathGenerationUtility : MonoBehaviour
{
    [SerializeField]
    private SplineFollower _splineFollower;
    [SerializeField]
    private PathGenerator _pathGenerator;
    private void OnEnable()
    {
        _pathGenerator.OnStartGenerate += Move;
    }
    private void OnDisable()
    {
        _pathGenerator.OnStartGenerate -= Move;
    }
    private void Move()
    {
        _splineFollower.followSpeed += 40;
    }

}
