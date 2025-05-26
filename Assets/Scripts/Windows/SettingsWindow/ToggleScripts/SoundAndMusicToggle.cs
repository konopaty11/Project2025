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
        // �������� �� �������
        togSoundAndMusic.onValueChanged.AddListener(ChangeVolumeSoundsAndMusic);
    }

    /// <summary>
    /// ������ ��������� ����� �������
    /// </summary>
    /// <param name="isActive"> ���������� ����� </param>
    public void ChangeVolumeSoundsAndMusic(bool isActive)
    {
        volume = isActive ? 0 : -80;
        audioMixer.SetFloat(nameGroup, volume);

        Image togImage = togSoundAndMusic.GetComponent<Image>();
        togImage.sprite = isActive ? togOn : togOff;

        // � ������ ������� ������ �� �� ������� ������� ������ ���������� �����
        togSoundAndMusic.isOn = isActive;

        ManagerSettingsWindow.IsActiveSoundNew = isActive;
    }
}
