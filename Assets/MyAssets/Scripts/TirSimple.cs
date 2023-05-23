using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirSimple : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;


    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.up * Time.deltaTime * _speed );
       if(transform.position.y > 8f) {
            Destroy(this.gameObject);
       } 
    } 
}
