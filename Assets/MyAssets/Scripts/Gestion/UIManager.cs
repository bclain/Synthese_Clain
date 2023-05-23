using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int _score = default;
    [SerializeField] private float _timeElapsed = 0;
    [SerializeField] private float _elapsedTime = 0;
    [SerializeField] private bool _gameOver = false;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtTimeElapsed = default;
    [SerializeField] private TextMeshProUGUI _txtGameOver = default;
    [SerializeField] private AudioClip _fondMusique = default;    
    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField] private bool _isMusicPlaying = true;
    [SerializeField] private Image _livesDisplayImage = default;
    [SerializeField] private Sprite[] _liveSprites = default;
    [SerializeField] private GameObject _pausePanel = default;
    private bool _pauseOn = false;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = FindObjectOfType<Enemy>();
        _score = 0;
        _txtGameOver.gameObject.SetActive(false);
        ChangeLivesDisplayImage(12);
        UpdateScore();

        _audioSource.clip = _fondMusique;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    private void Update()
    {
        if(!_gameOver){
            _timeElapsed += Time.deltaTime;
            _elapsedTime += Time.deltaTime;
            UpdateTimeElapsed();
        }

        if (_elapsedTime >= 15f)
        {
            _elapsedTime = 0f;
            _enemy.MoreSpeed();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseOn)
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pauseOn)
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }

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

    public void AjouterScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _txtScore.text = _score.ToString();
    }

    private void UpdateTimeElapsed()
    {
        int minutes = Mathf.FloorToInt(_timeElapsed / 60);
        int seconds = Mathf.FloorToInt(_timeElapsed % 60);
        if(_txtTimeElapsed){
            _txtTimeElapsed.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ChangeLivesDisplayImage(int noImage)
    {
        if (noImage < 0)
        {
            noImage = 0;
        }
        _livesDisplayImage.sprite = _liveSprites[noImage];
        if (noImage == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _txtGameOver.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Score", _score);
        PlayerPrefs.SetFloat("Temps", _timeElapsed);
        StartCoroutine(GameOverBlinkRoutine());
    }

    IEnumerator GameOverBlinkRoutine()
    {
            _gameOver=true;
            _txtGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _txtGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
            SceneManager.LoadScene(2);
        
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

     public void ChargerMenu()
    {
        SceneManager.LoadScene(0);
    }
}