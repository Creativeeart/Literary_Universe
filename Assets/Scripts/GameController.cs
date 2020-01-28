using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
namespace cakeslice
{
    public class GameController : MonoBehaviour
    {
        public Slider soundUISlider;
        public Slider musicSlider;
        public ToggleController _toggleStartInro;

        SupportScripts _supportScripts;
        public void Start()
        {
            if (GameObject.Find("SupportSripts"))
            {
                _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
            }
            else
            {
                Debug.Log("SupportSripts - не найден либо отключен!");
                return;
            }
            if (_supportScripts._configurationProject.startIntro) _toggleStartInro.isOn = true; else _toggleStartInro.isOn = false;
            if (_supportScripts._configurationProject.backgroundSound)
            {
                MusicPlaying();
                if (FindObjectOfType<AudioManager>().IsPlaying("BackgroundMusic") == false)
                {
                    FindObjectOfType<AudioManager>().Play("BackgroundMusic");
                }
            }
            else
            {
                MusicNotPlaying();
                if (FindObjectOfType<AudioManager>().IsPlaying("BackgroundMusic") == true)
                {
                    FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
                }
            }
            LoadSettings();
            
        }

        void Update()
        {
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            if (_supportScripts._configurationProject.backgroundSound) MusicPlaying(); else MusicNotPlaying();
            if (_supportScripts._configurationProject.IsShowTips) _supportScripts._toggleControllerTips.isOn = true; else _supportScripts._toggleControllerTips.isOn = false;

        }
        public void StartIntroSettings()
        {
            _supportScripts._configurationProject.startIntro = !_supportScripts._configurationProject.startIntro;
            if (_supportScripts._configurationProject.startIntro)
            {
                _toggleStartInro.isOn = true;
            }
            else
            {
                _toggleStartInro.isOn = false;
            }
        }
        public void SaveSettings()
        {
            _supportScripts._configurationProject.soundVolume = soundUISlider.value;
            _supportScripts._configurationProject.musicVolume = musicSlider.value;
            _supportScripts._configurationProject.SavePlayerPrefs();
        }
        public void LoadSettings()
        {
            soundUISlider.value = _supportScripts._configurationProject.soundVolume;
            musicSlider.value = _supportScripts._configurationProject.musicVolume;
            _supportScripts._configurationProject.LoadPlayerPrefs();
            FindObjectOfType<AudioManager>().AudioPlay();
        }
        public void ShowHideTips()
        {
            _supportScripts._configurationProject.IsShowTips = !_supportScripts._configurationProject.IsShowTips;
            if (_supportScripts._configurationProject.IsShowTips)
            {
                _supportScripts._toggleControllerTips.isOn = true;
            }
            else
            {
                _supportScripts._toggleControllerTips.isOn = false;
            }
            _supportScripts._configurationProject.SavePlayerPrefs();
        }
        public void SoundEnabledDisabled()
        {
            _supportScripts._configurationProject.backgroundSound = !_supportScripts._configurationProject.backgroundSound;
            if (_supportScripts._configurationProject.backgroundSound)
            {
                MusicPlaying();
                if (FindObjectOfType<AudioManager>().IsPlaying("BackgroundMusic") == false)
                {
                    FindObjectOfType<AudioManager>().Play("BackgroundMusic");
                }
                _supportScripts._configurationProject.backgroundSound = true;
            }
            else
            {
                MusicNotPlaying();
                if (FindObjectOfType<AudioManager>().IsPlaying("BackgroundMusic") == true)
                {
                    FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
                }
                _supportScripts._configurationProject.backgroundSound = false;
            }
            _supportScripts._configurationProject.SavePlayerPrefs();
        }
        void MusicPlaying()
        {
            _supportScripts._toggleControllerMusic.isOn = true;
        }
        void MusicNotPlaying()
        {
            _supportScripts._toggleControllerMusic.isOn = false;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void AppExitBtn()
        {
            Application.Quit();
        }
    }
}
