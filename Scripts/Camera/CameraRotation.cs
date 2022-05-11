using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    private void FixedUpdate()
    {
        transform.LookAt(_player.transform.position);
    }
}
