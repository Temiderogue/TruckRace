using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonChanger : MonoBehaviour
{
    [SerializeField] private List<GameObject> _wagons = new List<GameObject>();
    [SerializeField] private Rigidbody[] _woods;

    private int _randonNum;

    private void Start()
    {
        for (int i = 0; i < _wagons.Count; i++)
        {
            _wagons[i].SetActive(false);
        }


        _randonNum = Random.Range(0, _wagons.Count);
        _wagons[_randonNum].SetActive(true);

        
    }

    public void ActivateWood()
    {
        for (int i = 0; i < _woods.Length; i++)
        {
            _woods[i].isKinematic = false;
            _woods[i].useGravity = true;
        }
    }
}
