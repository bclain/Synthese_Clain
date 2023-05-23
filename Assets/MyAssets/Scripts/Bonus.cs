using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private float _speed = 8.0f;
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
            player.MitrailleFire();
            Destroy(this.gameObject, 0f);
        }
    }

}


