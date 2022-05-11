using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private SplineFollower _splineFollower;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _speedDifferenceByDefault;

    private float _speedDifference;

    private void Start()
    {
        _speedDifference = _speedDifferenceByDefault;
    }

    private void FixedUpdate()
    {
        _splineFollower.followSpeed = _player.NowSpeed - _speedDifference;

        if (Vector3.Distance(transform.position, _player.transform.position) > _distance)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeSpeed(-1));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ChangeSpeed(_speedDifferenceByDefault)); 
        }
    }
    private IEnumerator ChangeSpeed(float aim)
    {
        while (true)
        {
            _speedDifference = Mathf.Lerp(_speedDifference, aim, Time.deltaTime);
            yield return null;
        }
    }
 

}
