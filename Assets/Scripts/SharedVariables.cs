using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedVariables : MonoBehaviour
{
    public static bool allowGazePlacing = false;
    public static bool allowGesturePlacing = false;
    public static bool allowGestureRotation = false;
    public static int previousGestureState = 0;
    public static int currentGestureState = 0;

    public static bool seaScene = false;
}
