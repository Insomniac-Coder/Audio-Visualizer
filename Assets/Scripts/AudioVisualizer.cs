using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour {
    public GameObject[] audioSpectrumObjects;
    public GameObject Background;
    public MusicPlayer mPlayer;
    public AudioSource Recording;
    [Range(1, 100)] public float heightMultiplier;
    [Range(64, 8192)] public int numberOfSamples = 1024; //step by 2
    public FFTWindow fftWindow;
    public float lerpTime = 1;
    float Factor = 0f;
    public bool AudioRecordingMode = false;
    [HideInInspector]
    public float[] spectrum;
    // Use this for initialization
    void Start () {
        //MICROPHONE recording is not supported in webgl build
        Recording.clip = Microphone.Start(null, true, 5, 44800);
        Recording.loop = true; // Set the AudioClip to loop
        //GetComponent<AudioSource>().mute = true; // Mute the sound, we don't want the player to hear it
        while (!(Microphone.GetPosition(Microphone.devices[0]) > 0)) { } // Wait until the recording has started
        Recording.Play(); // Play the audio source!
    }
    // Update is called once per frame
    void Update () {
        //intensity
        float intensity;
        // initialize our float array
        spectrum = new float[numberOfSamples];
        //plays music instead of live audio stream
        if (Input.GetKeyDown(KeyCode.G))
        {
            AudioRecordingMode = !AudioRecordingMode;
        }
        if(AudioRecordingMode)
        {
            Recording.volume = 1f;
            // populate array with fequency spectrum data
            Recording.GetSpectrumData(spectrum, 0, fftWindow);
            heightMultiplier = 2f;
        }
        if (!AudioRecordingMode)
        {
            Recording.volume = 0f;
        }
        Factor = ((0.2f * audioSpectrumObjects[0].transform.localScale.y) / 0.47f) + ((0.2f * audioSpectrumObjects[1].transform.localScale.y) / 0.47f) + ((0.2f * audioSpectrumObjects[2].transform.localScale.y) / 0.47f) + ((0.2f * audioSpectrumObjects[3].transform.localScale.y) / 0.47f) + ((0.2f * audioSpectrumObjects[4].transform.localScale.y) / 0.47f);
        // loop over audioSpectrumObjects and modify according to fequency spectrum data
        this.transform.position = new Vector3(0.1f, 1f, -5.7f + (Factor));
        Background.transform.position = new Vector3(-3.22f, 1.95f, 4.37f - (4f*(Factor)));
        // this loop matches the Array element to an object on a One-to-One basis.

        for (int i = 0; i < audioSpectrumObjects.Length; i++)
        {
            // apply height multiplier to intensity
            if (AudioRecordingMode)
            {
                intensity = spectrum[i] * heightMultiplier;
            }
            else
            {
                intensity = mPlayer.spectrum[i] * heightMultiplier;
            }
            // calculate object's scale
            float lerpY = Mathf.Lerp(audioSpectrumObjects[i].transform.localScale.y, intensity, lerpTime);
            Vector3 newScale = new Vector3(audioSpectrumObjects[i].transform.localScale.x, lerpY, audioSpectrumObjects[i].transform.localScale.z);
            // appply new scale to object
            audioSpectrumObjects[i].transform.localScale = newScale;
            if(audioSpectrumObjects[i].transform.localScale.y <= 0.2f)
            {
                audioSpectrumObjects[i].GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, (audioSpectrumObjects[i].transform.localScale.y / 0.2f));
            }
            else
            {
                audioSpectrumObjects[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
