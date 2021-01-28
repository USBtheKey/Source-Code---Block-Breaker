using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


[RequireComponent(typeof(Slider))]

public class SetVolume : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameParam;

    private Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        float currentdB;
        audioMixer.GetFloat(nameParam, out currentdB);
        volumeSlider.value = currentdB;

    }

    public void SetVolumeAudioM(float vol)
    {
        audioMixer.SetFloat(nameParam, vol);
        volumeSlider.value = vol;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
