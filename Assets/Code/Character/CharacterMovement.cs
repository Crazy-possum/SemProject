using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private int _characterSpeed;

    private Rigidbody _characterRb;
    private Transform _charP;
    private Vector3 _move;
    private int _sideIndex = 1;

    private void Start()
    {
        _characterRb = GetComponent<Rigidbody>();
        _charP = GetComponent<Transform>();
    }

    private void Update()
    {
        switch (_sideIndex)
        {
            case 1:
                OnUpperSide();break;
            case 2:
                OnRightSide();break;
            case 3:
                OnLowerSide();break;
            case 4:
                OnLeftSide();break;
            default: break;
        }

        Debug.Log(_sideIndex);
    }

    private void OnUpperSide()
    // index = 1
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (_charP.position.x > -20f)
            {
                _move = new Vector3(-1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(0, -1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _charP.position = new Vector3(_charP.position.x, -10.8f, _charP.position.z);
            _sideIndex = 3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_charP.position.x < 20f)
            {
                _move = new Vector3(1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(0, -1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.x < -20f)
            {
                _sideIndex = 4;
                _charP.position = new Vector3(-20f, _charP.position.y, _charP.position.z);
            }
            else if (_charP.position.x > 20f)
            {
                _sideIndex = 2;
                _charP.position = new Vector3(20f, _charP.position.y, _charP.position.z);
            }
            else if (_charP.position.x == -20f)
            {
                _sideIndex = 4;
            }
            else if (_charP.position.x == 20f)
            {
                _sideIndex = 2;
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnRightSide()
    // index = 2
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (_charP.position.y < 10.8f)
            {
                _move = new Vector3(0, 1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(-1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _charP.position = new Vector3(-20f, _charP.position.y, _charP.position.z);
            _sideIndex = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_charP.position.y > -10.8f)
            {
                _move = new Vector3(0, -1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(-1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.y < -10.8f)
            {
                _sideIndex = 3;
                _charP.position = new Vector3(_charP.position.x, -10.8f, _charP.position.z);
            }
            else if (_charP.position.y > 10.8f)
            {
                _sideIndex = 1;
                _charP.position = new Vector3(_charP.position.x, 10.8f, _charP.position.z);
            }
            else if (_charP.position.y == -10.8f)
            {
                _sideIndex = 3;
            }
            else if (_charP.position.y == 10.8f)
            {
                _sideIndex = 1;
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnLowerSide()
    // index = 3
    {
        if (Input.GetKey(KeyCode.W))
        {
            _charP.position = new Vector3(_charP.position.x, 10.8f, _charP.position.z);
            _sideIndex = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (_charP.position.x > -20f)
            {
                _move = new Vector3(-1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(0, 1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_charP.position.x < 20f)
            {
                _move = new Vector3(1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(0, 1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.x < -20)
            {
                _sideIndex = 4;
                _charP.position = new Vector3(-20, _charP.position.y, _charP.position.z);
            }
            else if (_charP.position.x > 20)
            {
                _sideIndex = 2;
                _charP.position = new Vector3(20, _charP.position.y, _charP.position.z);
            }
            else if (_charP.position.x == -20)
            {
                _sideIndex = 4;
            }
            else if (_charP.position.x == 20)
            {
                _sideIndex = 2;
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnLeftSide()
    // index = 4
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (_charP.position.y < 10.8f)
            {
                _move = new Vector3(0, 1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_charP.position.y > -10.8f)
            {
                _move = new Vector3(0, -1, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
            else
            {
                _move = new Vector3(1, 0, 0);
                _characterRb.velocity = _move * _characterSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _charP.position = new Vector3(20f, _charP.position.y, _charP.position.z);
            _sideIndex = 2;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.y < -10.8f)
            {
                _sideIndex = 3;
                _charP.position = new Vector3(_charP.position.x, -10.8f, _charP.position.z);
            }
            else if (_charP.position.y > 10.8f)
            {
                _sideIndex = 1;
                _charP.position = new Vector3(_charP.position.x, 10.8f, _charP.position.z);
            }
            else if (_charP.position.y == -10.8f)
            {
                _sideIndex = 3;
            }
            else if (_charP.position.y == 10.8f)
            {
                _sideIndex = 1;
            }
        }
    }
}
