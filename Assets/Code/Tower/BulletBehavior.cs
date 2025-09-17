using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameObject BulletsCurrentTarget;

    private Rigidbody _bulletRB;

    private void Start()
    {
        _bulletRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
    }
}
