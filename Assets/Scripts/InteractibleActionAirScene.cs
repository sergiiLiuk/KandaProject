using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleActionAirScene : MonoBehaviour
{

    GameObject Cube;
    GameObject Cylinder;
    GameObject Sphere;

    public GameObject PlaceCubeButton;
    public GameObject PlaceCylinderButton;
    public GameObject PlaceSphereButton;

    // Use this for initialization
    void Start()
    {
        Cube = GameObject.Find("CubeModel");
        Cylinder = GameObject.Find("CylinderModel");
        Sphere = GameObject.Find("SphereModel");

        Cube.SetActive(true);
        Cylinder.SetActive(false);
        Sphere.SetActive(false);

        //PlaceCubeButton = GameObject.Find("BtnPlaceCube");
        //PlaceCylinderButton = GameObject.Find("BtnPlaceCylinder");
        //PlaceSphereButton = GameObject.Find("BtnPlaceSphere");

        PlaceCubeButton.SetActive(false);
        PlaceCylinderButton.SetActive(false);
        PlaceSphereButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaceCube()
    {
        Debug.Log("Show Cylinder");
        //appManager.InitiateDefaultRecognizer();
        PlaceCubeButton.SetActive(false);
        Cylinder.SetActive(true);
    }

    void PlaceCylinder()
    {
        Debug.Log("Show Sphere");
        //appManager.InitiateDefaultRecognizer();
        PlaceCylinderButton.SetActive(false);
        Sphere.SetActive(true);
    }

    void PlaceSphere()
    {
        Debug.Log("Showed Everything");
        //appManager.InitiateDefaultRecognizer();
        PlaceSphereButton.SetActive(false);
    }

    void CubeAction()
    {
        Debug.Log("Cube Action");
        //appManager.InitiateManipulationRecognizer();
        PlaceCubeButton.SetActive(true);
    }

    void CylinderAction()
    {
        Debug.Log("Cylinder Action");
        //appManager.InitiateManipulationRecognizer();
        PlaceCylinderButton.SetActive(true);
    }

    void SphereAction()
    {
        Debug.Log("Sphere Action");
        //appManager.InitiateManipulationRecognizer();
        PlaceSphereButton.SetActive(true);
    }
}
