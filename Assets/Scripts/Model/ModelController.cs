using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public GameObject model2;
    public GameObject model3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateNextModel(int nextModel)
    {
        if (!model2.activeSelf && nextModel == 2)
        {
            model2.SetActive(true);
        }

        if (model2.activeSelf && !model3.activeSelf && nextModel == 3)
        {
            model3.SetActive(true);
        }
    }
}
