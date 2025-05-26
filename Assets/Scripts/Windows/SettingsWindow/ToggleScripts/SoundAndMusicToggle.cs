using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundAndMusicToggle : MonoBehaviour
{
    [SerializeField] Toggle togSoundAndMusic;
    [SerializeField] Sprite togOn;
    [SerializeField] Sprite togOff;
    [SerializeField] AudioMixer audioMixer;

    float volume;
    string nameGroup = "MasterVolume";

    void Start()
    {
        // подписка на событие
        togSoundAndMusic.onValueChanged.AddListener(ChangeVolumeSoundsAndMusic);
    }

    /// <summary>
    /// меняет громкость звука миксера
    /// </summary>
    /// <param name="isActive"> активность тогла </param>
    public void ChangeVolumeSoundsAndMusic(bool isActive)
    {
        volume = isActive ? 0 : -80;
        audioMixer.SetFloat(nameGroup, volume);

        Image togImage = togSoundAndMusic.GetComponent<Image>();
        togImage.sprite = isActive ? togOn : togOff;

        // в случае запуска метода не по событию вручную менять активность тогла
        togSoundAndMusic.isOn = isActive;

        ManagerSettingsWindow.IsActiveSoundNew = isActive;
    }
}
