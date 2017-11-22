using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleActionAir : MonoBehaviour
{
    AppManager appManager;
    LoadOnStart loadOnStart;


    // Use this for initialization
    void Start()
    {
        appManager = FindObjectOfType(typeof(AppManager)) as AppManager;
        loadOnStart = FindObjectOfType(typeof(LoadOnStart)) as LoadOnStart;

        /* Cube = GameObject.Find("CubeModel");
         Cylinder = GameObject.Find("CylinderModel");
         Sphere = GameObject.Find("SphereModel");*/


        /*BtnFixPosCube = GameObject.Find("BtnFixPosCube");
        BtnFixPosCylinder = GameObject.Find("BtnFixPosCylinder");
        BtnFixPosSphere = GameObject.Find("BtnFixPosSphere");*/
    }

    void CubeAction()
    {
        Debug.Log("Cube Action");
        appManager.InitiateManipulationRecognizer();
        //GameObject BtnFixPosCube = GameObject.Find("BtnFixPosCube");
        loadOnStart.BtnFixPosCube.SetActive(true);
    }

    void CylinderAction()
    {
        Debug.Log("Cylinder Action");
        appManager.InitiateManipulationRecognizer();
        // GameObject BtnFixPosCylinder = GameObject.Find("BtnFixPosCylinder");
        loadOnStart.BtnFixPosCylinder.SetActive(true);
    }

    void SphereAction()
    {
        Debug.Log("Sphere Action");
        appManager.InitiateManipulationRecognizer();
        //GameObject BtnFixPosSphere = GameObject.Find("BtnFixPosSphere");
        loadOnStart.BtnFixPosSphere.SetActive(true);
    }

    //------------------------------------------------------

    void FixPosCube()
    {
        Debug.Log("Show Cylinder");
        appManager.InitiateDefaultRecognizer();
        // GameObject BtnObj = GameObject.Find(objName);
        //BtnObj.SetActive(false);
        loadOnStart.BtnFixPosCube.SetActive(false);
        //GameObject Cylinder = GameObject.Find("Cylinder");
        if (!loadOnStart.Cylinder.activeSelf)
            loadOnStart.Cylinder.SetActive(true);
    }

    void FixPoxCylinder()
    {
        Debug.Log("Show Sphere");
        appManager.InitiateDefaultRecognizer();
        //GameObject BtnObj = GameObject.Find(objName);
        //BtnObj.SetActive(false);
        loadOnStart.BtnFixPosCylinder.SetActive(false);
        //GameObject Sphere = GameObject.Find("Sphere");
        if (!loadOnStart.Sphere.activeSelf)
            loadOnStart.Sphere.SetActive(true);
    }

    void FixPosSphere()
    {
        Debug.Log("Showed Everything");
        appManager.InitiateDefaultRecognizer();
        loadOnStart.BtnFixPosSphere.SetActive(false);
        // GameObject BtnObj = GameObject.Find(objName);
        //BtnObj.SetActive(false);
    }


}
