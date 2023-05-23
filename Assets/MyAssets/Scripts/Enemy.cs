using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private GameObject _explosionPrefab = default;
    [SerializeField] private AudioClip _explosionSound = default;
    private UIManager _uiManager;

    void Start(){
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement(){
        transform.Translate(Vector3.down *  Time.deltaTime * _speed);
        if(transform.position.y <= -5.0f) {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            Player player = collision.transform.GetComponent<Player>();
            player.Degats();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0f);
            AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position, 0.3f);
        }
        else if(collision.tag == "TirSimple")
        { 
            _uiManager.AjouterScore(10);
            Destroy(collision.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0f);
            AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position, 0.3f);
        }
    }

    public void MoreSpeed()
    {
        _speed += 4f;
    }

}


