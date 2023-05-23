using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _tirPrefab = default;
    [SerializeField] private GameObject _tirMitraillePrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;
    [SerializeField] private AudioClip _tirSound = default;
    [SerializeField] private bool _mitrailleType = false;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _fireRate  = 0.5f;
    [SerializeField] private int _viesJoueur = 12;
    private float _canFire = -1.0f; 
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private Animator _anim;
 
    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        transform.position = new Vector3(0f, -2.89f, 0f);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _anim = GetComponent<Animator>();
    }
         
    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && !_mitrailleType){
            Fire();
        }
    }


    private void Move() {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * Time.deltaTime * _speed);
        
        if (horizontalInput < 0)
        {
            _anim.SetBool("Turn_Left", true);
            _anim.SetBool("Turn_Right", false);
        }
        else if(horizontalInput > 0)
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", true);
        }
        else
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0);
        if(transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if(transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    private void Fire() {
        AudioSource.PlayClipAtPoint(_tirSound, Camera.main.transform.position, 0.2f);
        _canFire = Time.time + _fireRate;
        Instantiate(_tirPrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity);
    }

    public void MitrailleFire() {
        _mitrailleType = true;
        StartCoroutine(MitrailleFireCoroutine());
        StartCoroutine(MitrailleTempsCoroutine());
    }


    IEnumerator MitrailleFireCoroutine()
    {
        while (_mitrailleType == true)
        {
           AudioSource.PlayClipAtPoint(_tirSound, Camera.main.transform.position, 0.15f);
           Instantiate(_tirMitraillePrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity);
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator MitrailleTempsCoroutine()
    {
            yield return new WaitForSeconds(5f);
            _mitrailleType = false;
    }

    public void Degats(){
        _viesJoueur--;
        _uiManager.ChangeLivesDisplayImage(_viesJoueur);
        if(_viesJoueur < 1){
            Destroy(this.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.FinJeu();
        }
    }
    
}
 