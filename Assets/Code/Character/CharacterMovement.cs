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

    private SideIndexEnum _sideIndex = SideIndexEnum.Upper;
    private float _upSV = 10.8f;
    private float _riSV = 20f;
    private float _loSV = -10.8f;
    private float _leSV = -20f;

    private void Start()
    {
        _characterRb = GetComponent<Rigidbody>();
        _charP = GetComponent<Transform>();
    }

    private void Update()
    {
        switch (_sideIndex)
        {
            case SideIndexEnum.Upper:
                OnUpperSide();break;
            case SideIndexEnum.Right:
                OnRightSide();break;
            case SideIndexEnum.Lower:
                OnLowerSide();break;
            case SideIndexEnum.Left:
                OnLeftSide();break;
            default: break;
        }
    }

    private void OnUpperSide()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (_charP.position.x > _leSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            else if (_charP.position.x <= _leSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            else if (_charP.position.x < _riSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            else if (_charP.position.x >= _riSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _charP.position = new Vector3(_charP.position.x, _loSV, _charP.position.z);
            _sideIndex = SideIndexEnum.Lower;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_charP.position.x < _riSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            else if (_charP.position.x >= _riSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            else if (_charP.position.x > _leSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            else if (_charP.position.x <= _leSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.y <= _loSV)
            {
                _sideIndex = SideIndexEnum.Left;
            }
            else if ((_charP.position.x <= _leSV))
            {
                _sideIndex = SideIndexEnum.Lower;
            }
            else if (_charP.position.x >= _riSV)
            {
                _sideIndex = SideIndexEnum.Right;
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnRightSide()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (_charP.position.x >= _riSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            else if (_charP.position.x > _loSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            else if (_charP.position.x <= _leSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            else if (_charP.position.x < _riSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _charP.position = new Vector3(-20f, _charP.position.y, _charP.position.z);
            _sideIndex = SideIndexEnum.Left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_charP.position.x >= _riSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            else if (_charP.position.x > _leSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            else if (_charP.position.x <= _leSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            else if (_charP.position.x < _riSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.y <= _loSV)
            {
                _sideIndex = SideIndexEnum.Lower;
            }
            else if (_charP.position.y >= _upSV)
            {
                _sideIndex = SideIndexEnum.Upper;
            }
            else if ((_charP.position.x <= _leSV))
            {
                _sideIndex = SideIndexEnum.Left;
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnLowerSide()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _charP.position = new Vector3(_charP.position.x, _upSV, _charP.position.z);
            _sideIndex = SideIndexEnum.Upper;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (_charP.position.x > _leSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            else if (_charP.position.x <= _leSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            else if (_charP.position.x < _riSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            else if (_charP.position.x >= _riSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_charP.position.x < _riSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            else if (_charP.position.x >= _riSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            else if (_charP.position.x > _leSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            else if (_charP.position.x <= _leSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.x <= _leSV)
            {
                _sideIndex = SideIndexEnum.Left;
            }
            else if (_charP.position.x >= _riSV)
            {
                _sideIndex = SideIndexEnum.Right;
            }
            else if (_charP.position.y >= _upSV)
            {
                _sideIndex = SideIndexEnum.Upper;
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnLeftSide()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (_charP.position.x <= _leSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            else if (_charP.position.x < _riSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            else if (_charP.position.x >= _riSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            else if (_charP.position.x > _leSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_charP.position.x <= _leSV && _charP.position.y > _loSV)
            {
                _move = new Vector3(0, -1, 0);
            }
            else if (_charP.position.x < _riSV && _charP.position.y <= _loSV)
            {
                _move = new Vector3(1, 0, 0);
            }
            else if (_charP.position.x >= _riSV && _charP.position.y < _upSV)
            {
                _move = new Vector3(0, 1, 0);
            }
            else if (_charP.position.x > _leSV && _charP.position.y >= _upSV)
            {
                _move = new Vector3(-1, 0, 0);
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _charP.position = new Vector3(20f, _charP.position.y, _charP.position.z);
            _sideIndex = SideIndexEnum.Right;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charP.position.y <= _loSV)
            {
                _sideIndex = SideIndexEnum.Lower;
            }
            else if (_charP.position.y >= _upSV)
            {
                _sideIndex = SideIndexEnum.Upper;
            }
            else if (_charP.position.x >= _riSV)
            {
                _sideIndex = SideIndexEnum.Right;
            }
        }
    }
}
