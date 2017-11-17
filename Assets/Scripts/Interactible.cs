using UnityEngine;
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
    public static bool menuBtnTimer = false;
    Image fillMenuBtn;
    float time = 0;


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

        fillMenuBtn = this.GetComponent<Image>();
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
        //When timer is out then Gesture set to default
        if (menuBtnTimer == true)
        {
            time += Time.deltaTime;
            Debug.Log("t: " + time);

            if (fillMenuBtn != null)
            {
                Debug.Log("Yes");
                fillMenuBtn.fillAmount = time;
            }
            else
            {
                Debug.Log("No");
                return;
            }

            if (time >= 3f)
            {
                menuBtnTimer = false;
                time = 0;
                Debug.Log("Done");
            }
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
        menuBtnTimer = false;
        if (fillMenuBtn)
            fillMenuBtn.fillAmount = 0;
        time = 0;
    }

    void OnSelect()
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

        this.SendMessage("PerformTagAlong");
        this.SendMessage("PerfomText");
    }

    void OnGazeEntered(string itemSelected)
    {
        switch (itemSelected)
        {
            case "BtnAir":
                Debug.Log("Dev-> BtnAir");
                menuBtnTimer = true;

                break;

            case "BtnSea":
                Debug.Log("Dev-> BtnSea");
                menuBtnTimer = true;

                break;

            case "BtnLand":
                Debug.Log("Dev-> BtnLand");
                menuBtnTimer = true;

                break;

            default:
                break;
        }
    }

}