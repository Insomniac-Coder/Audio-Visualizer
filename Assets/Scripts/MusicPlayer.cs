using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    AudioVisualizer obj;
    public AudioSource music;
    bool flag = true;
    public FFTWindow fftWindow;
    [HideInInspector]
    public float[] spectrum;
    [Range(64, 8192)] public int numberOfSamples;
    // Use this for initialization
    void Start () {
        obj = Camera.main.GetComponent<AudioVisualizer>();
	}
	
	// Update is called once per frame
	void Update () {
        numberOfSamples = obj.numberOfSamples;
        spectrum = new float[numberOfSamples];
        fftWindow = obj.fftWindow;
        if (obj.AudioRecordingMode)
        {
            music.Stop();
            flag = true;
        }
        if(!obj.AudioRecordingMode)
        {
            if (flag)
            {
                music.Play();
                flag = false;
            }
            obj.heightMultiplier = 1f;
            music.GetSpectrumData(spectrum, 0, fftWindow);
            obj.heightMultiplier = 1f;
        }
	}
}
