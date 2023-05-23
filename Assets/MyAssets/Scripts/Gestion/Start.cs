using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    [SerializeField] private GameObject _instructionsPanel = default;
    private bool _instructionsOn = false;
    [SerializeField] private AudioClip _fondMusique = default;    
    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField] private bool _isMusicPlaying = true;

    void Awake()
    { 
       _audioSource.clip = _fondMusique;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isMusicPlaying)
            {
                _audioSource.Pause();
                _isMusicPlaying = false;
            }
            else
            {
                _audioSource.UnPause();
                _isMusicPlaying = true;
            }
        }
    }
    
    public void Quitter()
    {
        Application.Quit();
    }

    public void ChangerScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void InstructionsGame()
    {
        if(!_instructionsOn){
            _instructionsOn = true;
            _instructionsPanel.SetActive(true);
        }
        else{
            _instructionsOn = false;
            _instructionsPanel.SetActive(false);
        }
    }
}
