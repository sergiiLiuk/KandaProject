
using UnityEngine;

public class UnloadUnusedAssets : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Resources.UnloadUnusedAssets();
        Debug.Log("Resources.UnloadUnusedAssets");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
