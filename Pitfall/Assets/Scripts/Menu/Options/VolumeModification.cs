using UnityEngine;
using UnityEngine.Audio;

public class VolumeModification : MonoBehaviour
{

    private AudioSource audioSrc;
    public AudioMixer aAudioMixer;

    private float musicVolume = 1f;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }


    public void SetVolume(float volume)
    {
        aAudioMixer.SetFloat("Volume", volume);
        audioSrc.volume = musicVolume;

    }
}