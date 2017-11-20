using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at.
/// </summary>
public class Interactible : MonoBehaviour
{
    [Tooltip("Audio clip to play when interacting with this hologram.")]
    public AudioClip TargetFeedbackSound;
    private AudioSource audioSource;

    private Material[] defaultMaterials;

    [Space]
    [Header("Menu")]
    [HideInInspector]
    private bool onAirBtnTimer;
    private bool onSeaBtnTimer;
    private bool onLandBtnTimer;

    private Button BtnAir;
    private Button BtnSea;
    private Button BtnLand;

    float time = 0;

    private MENU_BUTTON currentMenuButton;
    public enum MENU_BUTTON
    {
        ON_AIR,
        ON_SEA,
        ON_LAND
    }

    void Start()
    {
        // defaultMaterials = GetComponent<Renderer>().materials;

        // Add a BoxCollider if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        EnableAudioHapticFeedback();

        BtnAir = GameObject.Find("BtnAir").GetComponent<Button>();
        BtnSea = GameObject.Find("BtnSea").GetComponent<Button>();
        BtnLand = GameObject.Find("BtnLand").GetComponent<Button>();

    }

    void Update()
    {
        MenuBtnPressCountDown();

    }

    private void EnableAudioHapticFeedback()
    {
        // If this hologram has an audio clip, add an AudioSource with this clip.
        if (TargetFeedbackSound != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = TargetFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
    }

    private void MenuBtnPressCountDown()
    {

        switch (currentMenuButton)
        {
            case MENU_BUTTON.ON_AIR:
                if (onAirBtnTimer == true)
                {
                    time += Time.deltaTime;
                    BtnAir.image.fillAmount = time;

                    if (time >= 3f)
                    {
                        onAirBtnTimer = false;
                        time = 0;
                        AppManager.appState = AppManager.APP_STATES.PLACING_MODE;
                        SceneManager.LoadScene("AirScene", LoadSceneMode.Additive);

                    }
                }
                break;

            case MENU_BUTTON.ON_SEA:
                if (onSeaBtnTimer == true)
                {
                    time += Time.deltaTime;

                    Debug.Log("Sea Yes");
                    BtnSea.image.fillAmount = time;

                    if (time >= 3f)
                    {
                        onSeaBtnTimer = false;
                        time = 0;
                        Debug.Log("Sea Done");
                    }
                }

                break;
            case MENU_BUTTON.ON_LAND:
                if (onLandBtnTimer == true)
                {
                    time += Time.deltaTime;

                    Debug.Log("Land Yes");
                    BtnLand.image.fillAmount = time;

                    if (time >= 3f)
                    {
                        onLandBtnTimer = false;
                        time = 0;
                        Debug.Log("Land Done");
                    }
                }
                break;

            default:
                break;
        }



    }

    void GazeEntered()
    {
        /*for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .25f);
        }*/

        OnGazeEntered(this.gameObject.name);

    }

    void GazeExited()
    {
        /*for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", 0f);
        }*/
        onAirBtnTimer = false;
        BtnAir.image.fillAmount = 0;

        onSeaBtnTimer = false;
        BtnSea.image.fillAmount = 0;

        onLandBtnTimer = false;
        BtnLand.image.fillAmount = 0;

        time = 0;
    }

    void OnSelect( )
    {
        /*for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .5f);
        }*/

        // Play the audioSource feedback when we gaze and select a hologram.
        

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        //this.SendMessage("PerformTagAlong");
        //this.SendMessage("PerfomText");
    }

    void OnGazeEntered(string btnPressed)
    {
        switch (btnPressed)
        {
            case "BtnAir":
                Debug.Log("Dev-> BtnAir");
                currentMenuButton = MENU_BUTTON.ON_AIR;
                onAirBtnTimer = true;

                break;

            case "BtnSea":
                Debug.Log("Dev-> BtnSea");
                currentMenuButton = MENU_BUTTON.ON_SEA;
                onSeaBtnTimer = true;

                break;

            case "BtnLand":
                Debug.Log("Dev-> BtnLand");
                currentMenuButton = MENU_BUTTON.ON_LAND;
                onLandBtnTimer = true;

                break;

            default:
                break;
        }
    }

}