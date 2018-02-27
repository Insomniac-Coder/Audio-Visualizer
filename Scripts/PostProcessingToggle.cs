using System.Collections;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class PostProcessingToggle : MonoBehaviour
{
    public Toggle checkMark;

    public bool PostPorcessingEffects = true;
    private void Update()
    {
        Check();
        Toggle();
    }
    public void Check()
    {
        if (checkMark.isOn)
        {
            PostPorcessingEffects = true;
        }
        else
        {
            PostPorcessingEffects = false;
        }
    }
    public void Toggle()
    {
        if (PostPorcessingEffects)
        {
            this.GetComponent<PostProcessingBehaviour>().enabled = true;
        }
        else
        {
            this.GetComponent<PostProcessingBehaviour>().enabled = false;
        }
    }
}
