﻿using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace Academy.HoloToolkit.Unity
{
    public class GestureManager : Singleton<GestureManager>
    {
        // Tap and Navigation gesture recognizer.
        public GestureRecognizer NavigationRecognizer { get; private set; }

        // Manipulation gesture recognizer.
        public GestureRecognizer ManipulationRecognizer { get; private set; }

        // Currently active gesture recognizer.
        public GestureRecognizer ActiveRecognizer { get; private set; }

        public GestureRecognizer DefaultRecognizer { get; private set; }

        public bool IsNavigating { get; private set; }

        public Vector3 NavigationPosition { get; private set; }

        public bool IsManipulating { get; private set; }

        public Vector3 ManipulationPosition { get; private set; }

        private GestureRecognizer CurrentRecognizer;

        public int currentGestureState = 0;

        void Awake()
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.b */

            // 2.b: Instantiate the NavigationRecognizer.
            NavigationRecognizer = new GestureRecognizer();
            DefaultRecognizer = new GestureRecognizer();
            // Instantiate the ManipulationRecognizer.
            ManipulationRecognizer = new GestureRecognizer();

            // 2.b: Add Tap and NavigationX GestureSettings to the NavigationRecognizer's RecognizableGestures.
            NavigationRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.NavigationX);
            DefaultRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.DoubleTap);
            // Add the ManipulationTranslate GestureSetting to the ManipulationRecognizer's RecognizableGestures.
            ManipulationRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);

            // 2.b: Register for the Tapped with the NavigationRecognizer_Tapped function.
            NavigationRecognizer.Tapped += NavigationRecognizer_Tapped;
            // 2.b: Register for the NavigationStarted with the NavigationRecognizer_NavigationStarted function.
            NavigationRecognizer.NavigationStarted += NavigationRecognizer_NavigationStarted;
            // 2.b: Register for the NavigationUpdated with the NavigationRecognizer_NavigationUpdated function.
            NavigationRecognizer.NavigationUpdated += NavigationRecognizer_NavigationUpdated;
            // 2.b: Register for the NavigationCompleted with the NavigationRecognizer_NavigationCompleted function. 
            NavigationRecognizer.NavigationCompleted += NavigationRecognizer_NavigationCompleted;
            // 2.b: Register for the NavigationCanceled with the NavigationRecognizer_NavigationCanceled function. 
            NavigationRecognizer.NavigationCanceled += NavigationRecognizer_NavigationCanceled;

            // Register for the Manipulation events on the ManipulationRecognizer.
            ManipulationRecognizer.TappedEvent += ManipulationRecognizer_TappedEvent;
            ManipulationRecognizer.ManipulationStarted += ManipulationRecognizer_ManipulationStarted;
            ManipulationRecognizer.ManipulationUpdated += ManipulationRecognizer_ManipulationUpdated;
            ManipulationRecognizer.ManipulationCompleted += ManipulationRecognizer_ManipulationCompleted;
            ManipulationRecognizer.ManipulationCanceled += ManipulationRecognizer_ManipulationCanceled;

            DefaultRecognizer.TappedEvent += DoubleTapRecognizer_TappedEvent;

            ResetGestureRecognizers();
        }

        void OnDestroy()
        {
            // 2.b: Unregister the Tapped and Navigation events on the NavigationRecognizer.
            NavigationRecognizer.Tapped -= NavigationRecognizer_Tapped;
            NavigationRecognizer.NavigationStarted -= NavigationRecognizer_NavigationStarted;
            NavigationRecognizer.NavigationUpdated -= NavigationRecognizer_NavigationUpdated;
            NavigationRecognizer.NavigationCompleted -= NavigationRecognizer_NavigationCompleted;
            NavigationRecognizer.NavigationCanceled -= NavigationRecognizer_NavigationCanceled;


            // Unregister the Manipulation events on the ManipulationRecognizer.
            ManipulationRecognizer.TappedEvent -= ManipulationRecognizer_TappedEvent;
            ManipulationRecognizer.ManipulationStarted -= ManipulationRecognizer_ManipulationStarted;
            ManipulationRecognizer.ManipulationUpdated -= ManipulationRecognizer_ManipulationUpdated;
            ManipulationRecognizer.ManipulationCompleted -= ManipulationRecognizer_ManipulationCompleted;
            ManipulationRecognizer.ManipulationCanceled -= ManipulationRecognizer_ManipulationCanceled;

            DefaultRecognizer.TappedEvent -= DoubleTapRecognizer_TappedEvent;
             
        }

        /// <summary>
        /// Revert back to the default GestureRecognizer.
        /// </summary>
        public void ResetGestureRecognizers()
        {
            // Default to the navigation gestures.
            if (currentGestureState == 0)
            {
                CurrentRecognizer = DefaultRecognizer;
            }

            else if (currentGestureState == 1)
            {
                CurrentRecognizer = ManipulationRecognizer;
            }

            else if (currentGestureState == 2)
            {
                // CurrentRecognizer = RotationRecognizer;
            }

            else if (currentGestureState == 3)
            {
                //CurrentRecognizer = ScalingRecognizer;
            }

            Transition(CurrentRecognizer);
        }

        /// <summary>
        /// Transition to a new GestureRecognizer.
        /// </summary>
        /// <param name="newRecognizer">The GestureRecognizer to transition to.</param>
        /// 
        private void DoubleTapRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
        {
            GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;

            if (focusedObject != null)
            {
                focusedObject.SendMessageUpwards("OnSelect");
            }
            //MainMenuTapController(tapCount);
        }

        public void Transition(GestureRecognizer newRecognizer)
        {
            if (newRecognizer == null)
            {
                return;
            }

            if (ActiveRecognizer != null)
            {
                if (ActiveRecognizer == newRecognizer)
                {
                    return;
                }

                ActiveRecognizer.CancelGestures();
                ActiveRecognizer.StopCapturingGestures();
            }

            newRecognizer.StartCapturingGestures();
            ActiveRecognizer = newRecognizer;
        }

        private void NavigationRecognizer_NavigationStarted(NavigationStartedEventArgs obj)
        {
            // 2.b: Set IsNavigating to be true.
            IsNavigating = true;

            // 2.b: Set NavigationPosition to be Vector3.zero.
            NavigationPosition = Vector3.zero;
        }

        private void NavigationRecognizer_NavigationUpdated(NavigationUpdatedEventArgs obj)
        {
            // 2.b: Set IsNavigating to be true.
            IsNavigating = true;

            // 2.b: Set NavigationPosition to be obj.normalizedOffset.
            NavigationPosition = obj.normalizedOffset;
        }

        private void NavigationRecognizer_NavigationCompleted(NavigationCompletedEventArgs obj)
        {
            // 2.b: Set IsNavigating to be false.
            IsNavigating = false;
        }

        private void NavigationRecognizer_NavigationCanceled(NavigationCanceledEventArgs obj)
        {
            // 2.b: Set IsNavigating to be false.
            IsNavigating = false;
        }

        private void RotationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
        {
            GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;

            if (focusedObject != null)
            {
                focusedObject.SendMessageUpwards("OnSelect");
            }
            //MainMenuTapController(tapCount);
        }

        //------------------------------------------------------------------------------------------------------

        private void ManipulationRecognizer_ManipulationStarted(ManipulationStartedEventArgs obj)
        {
            if (HandsManager.Instance.FocusedGameObject != null)
            {
                IsManipulating = true;

                ManipulationPosition = Vector3.zero;

                HandsManager.Instance.FocusedGameObject.SendMessageUpwards("PerformManipulationStart", ManipulationPosition);
            }
        }

        private void ManipulationRecognizer_ManipulationUpdated(ManipulationUpdatedEventArgs obj)
        {
            if (HandsManager.Instance.FocusedGameObject != null)
            {
                IsManipulating = true;

                ManipulationPosition = obj.cumulativeDelta;

                HandsManager.Instance.FocusedGameObject.SendMessageUpwards("PerformManipulationUpdate", ManipulationPosition);
            }
        }

        private void ManipulationRecognizer_ManipulationCompleted(ManipulationCompletedEventArgs obj)
        {
            IsManipulating = false;
        }

        private void ManipulationRecognizer_ManipulationCanceled(ManipulationCanceledEventArgs obj)
        {
            IsManipulating = false;
        }

        private void NavigationRecognizer_Tapped(TappedEventArgs obj)
        {
            GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;

            if (focusedObject != null)
            {
                focusedObject.SendMessageUpwards("OnSelect");
            }
        }

        private void ManipulationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
        {
            GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;

            if (focusedObject != null)
            {
                focusedObject.SendMessageUpwards("OnSelect");
            }
            //MainMenuTapController(tapCount);
        }
    }
}