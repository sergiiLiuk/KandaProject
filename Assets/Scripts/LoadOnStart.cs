using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnStart : MonoBehaviour
{
    [Header("Air Scene")]
    public GameObject Cube;
    public GameObject Cylinder;
    public GameObject Sphere;
    [Space]
    [Header("Air Scene Fix Pos. Btns")]
    public GameObject BtnFixPosCube;
    public GameObject BtnFixPosCylinder;
    public GameObject BtnFixPosSphere;
    // Use this for initialization

    void Start()
    {
        Cube.SetActive(true);
        Cylinder.SetActive(false);
        Sphere.SetActive(false);

        BtnFixPosCube.SetActive(false);
        BtnFixPosCylinder.SetActive(false);
        BtnFixPosSphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
