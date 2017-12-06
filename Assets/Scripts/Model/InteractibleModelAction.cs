using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleModelAction : MonoBehaviour
{
    //GameObject ModelMenu;
    // Use this for initialization
    void Start()
    {
        // ModelMenu=GameObject.Find
    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (SharedVariables.allowGazePlacing)
        {
            if (HandsManager.Instance.FocusedGameObject && (HandsManager.Instance.FocusedGameObject.name == "Sea Tel 9711 Model" /*|| HandsManager.Instance.FocusedGameObject.name == "SAILOR 4300 Iridium Model" || HandsManager.Instance.FocusedGameObject.name == "SAILOR 150 FleetBroadband Modell"*/))
            {
                // Debug.Log("Here1");
                UIController.Instance.ModelMenu.SetActive(false);
                SpatialMapping.Instance.DrawVisualMeshes = true;
                // Do a raycast into the world that will only hit the Spatial Mapping mesh.
                var headPosition = Camera.main.transform.position;
                var gazeDirection = Camera.main.transform.forward;

                RaycastHit hitInfo;
                if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                    30.0f, SpatialMapping.PhysicsRaycastMask))
                {
                    // Move this object's parent object to
                    // where the raycast hit the Spatial Mapping mesh.
                    transform.parent.position = hitInfo.point;
                    // Rotate this object's parent object to face the user.
                    Quaternion toQuat = Camera.main.transform.localRotation;
                    toQuat.x = 0;
                    toQuat.z = 0;
                    transform.parent.rotation = toQuat;
                }
            }
            else
            {
                UIController.Instance.ModelMenu.SetActive(true);
                SpatialMapping.Instance.DrawVisualMeshes = false;
            }
        }

        if (SharedVariables.allowGesturePlacing || SharedVariables.allowGestureRotation)
        {
            if (HandsManager.Instance.FocusedGameObject && (HandsManager.Instance.FocusedGameObject.name == "Sea Tel 9711 Model" /*|| HandsManager.Instance.FocusedGameObject.name == "SAILOR 4300 Iridium Model" || HandsManager.Instance.FocusedGameObject.name == "SAILOR 150 FleetBroadband Modell"*/))
            {
                //Debug.Log("Here2");
                UIController.Instance.ModelMenu.SetActive(false);
                SpatialMapping.Instance.DrawVisualMeshes = true;
            }
            else
            {
                UIController.Instance.ModelMenu.SetActive(true);
                SpatialMapping.Instance.DrawVisualMeshes = false;
            }
        }
    }

}