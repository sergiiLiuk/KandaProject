using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTarget : MonoBehaviour
{

    public GameObject target1;
    public Vector3 pos1;
    public GameObject target2;
    public Vector3 pos2;
    // Use this for initialization
    void Start()
    {
        target1.transform.position = transform.TransformPoint(pos1);
        target2.transform.position = transform.TransformPoint(pos2);
    }

    void Update()
    {
        
    }
}
