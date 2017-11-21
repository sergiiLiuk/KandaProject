using Academy.HoloToolkit.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class AppManager : Singleton<AppManager>
{
    [HideInInspector]
    public static APP_STATES appState;

    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;

    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;

    public enum APP_STATES
    {
        MENU,
        PLACING_MODE,
        SHOW_MODE
    }


    void Start()
    {
        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword to start manipulation.
        keywordCollection.Add("Move", MoveModelCommand);

        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

        appState = APP_STATES.MENU;
    }

    void OnDestroy()
    {
        keywordRecognizer.Dispose();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }

    private void MoveModelCommand(PhraseRecognizedEventArgs args)
    {
        GestureManager.Instance.Transition(GestureManager.Instance.ManipulationRecognizer);
    }

    //------------Initiate Recognizers---------------
    public void InitiateDefaultRecognizer()
    {
        GestureManager.Instance.Transition(GestureManager.Instance.DefaultRecognizer);
        GestureManager.Instance.currentGestureState = 0;
    }

    public void InitiateManipulationRecognizer()
    {
        GestureManager.Instance.Transition(GestureManager.Instance.ManipulationRecognizer);
        GestureManager.Instance.currentGestureState = 1;

        // Refresh default Gesture timer
        //timerDone = false;
        //DefaultRecognizerTimer = 6.0f;
    }

    public void InitiateNavigationRecognizer()
    {
        GestureManager.Instance.Transition(GestureManager.Instance.NavigationRecognizer);
        GestureManager.Instance.currentGestureState = 2;
        // Refresh default Gesture timer
        //timerDone = false;
        //Instance.DefaultRecognizerTimer = 6.0f;
    }


    public void Update()
    {
        AppState(appState);
    }


    public void AppState(APP_STATES appState)
    {
        switch (appState)
        {
            case APP_STATES.MENU:
                //Debug.Log("Dev-> MENU");

                break;
            case APP_STATES.PLACING_MODE:

                GameObject menu = GameObject.Find("SelectionMenu");
                if (menu != null)
                {
                    menu.SetActive(false);
                }
                //Debug.Log("Dev-> Scene is --AirScene-- Loaded");
                //Debug.Log("Dev-> PLACING_MODE");

                break;
            case APP_STATES.SHOW_MODE:
                Debug.Log("Dev-> SHOW_MODE");

                break;

            default:
                break;
        }
    }
}