using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Fin : MonoBehaviour
{

    [SerializeField] private int _score = default;
    [SerializeField] private float _timeElapsed = 0;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtTimeElapsed = default;
    [SerializeField] private AudioClip _fondMusique = default;    
    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField] private bool _isMusicPlaying = true;
    // Start is called before the first frame update
    void Start()
    { 
        _score = PlayerPrefs.GetInt("Score");
        _timeElapsed = PlayerPrefs.GetFloat("Temps");
        _txtScore.text = _score.ToString();
         int minutes = Mathf.FloorToInt(_timeElapsed / 60);
         int seconds = Mathf.FloorToInt(_timeElapsed % 60);
        _txtTimeElapsed.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
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

    
    public void Rejouer()
    { 
        SceneManager.LoadScene(1);
    }

    public void Menu()
    { 
        SceneManager.LoadScene(0);
    }
}
