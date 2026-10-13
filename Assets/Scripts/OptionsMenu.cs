using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    [Header("Mixers")]
    [SerializeField] AudioMixer mainMixer;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mainMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        mainMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }
}