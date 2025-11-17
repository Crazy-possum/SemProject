using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Tooltip("Скорость движения персонажа")]
    [SerializeField] private int _characterSpeed;

    private SideIndexEnum _sideIndex = SideIndexEnum.Upper;
    private Vector3 _moveToUp = new Vector3(0, 1, 0);
    private Vector3 _moveToRight = new Vector3(1, 0, 0);
    private Vector3 _moveToDown = new Vector3(0, -1, 0);
    private Vector3 _moveToLeft = new Vector3(-1, 0, 0);
    private Rigidbody _characterRb;
    private Transform _charPos;
    private Vector3 _move;

    private float _upBorder = 9.5f;
    private float _riBorder = 20f;
    private float _loBorder = -9.5f;
    private float _leBorder = -20f;

    private void Start()
    {
        _characterRb = GetComponent<Rigidbody>();
        _charPos = GetComponent<Transform>();
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
            if (_charPos.position.x > _leBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToLeft;
            }
            else if (_charPos.position.x <= _leBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            else if (_charPos.position.x < _riBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToRight;
            }
            else if (_charPos.position.x >= _riBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _charPos.position = new Vector3(_charPos.position.x, _loBorder, _charPos.position.z);
            _sideIndex = SideIndexEnum.Lower;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_charPos.position.x < _riBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToRight;
            }
            else if (_charPos.position.x >= _riBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            else if (_charPos.position.x > _leBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToLeft;
            }
            else if (_charPos.position.x <= _leBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charPos.position.y <= _loBorder)
            {
                _sideIndex = SideIndexEnum.Left;
            }
            else if ((_charPos.position.x <= _leBorder))
            {
                _sideIndex = SideIndexEnum.Lower;
            }
            else if (_charPos.position.x >= _riBorder)
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
            if (_charPos.position.x >= _riBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            else if (_charPos.position.x > _loBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToLeft;
            }
            else if (_charPos.position.x <= _leBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            else if (_charPos.position.x < _riBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToRight;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _charPos.position = new Vector3(_leBorder, _charPos.position.y, _charPos.position.z);
            _sideIndex = SideIndexEnum.Left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_charPos.position.x >= _riBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            else if (_charPos.position.x > _leBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToLeft;
            }
            else if (_charPos.position.x <= _leBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            else if (_charPos.position.x < _riBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToRight;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charPos.position.y <= _loBorder)
            {
                _sideIndex = SideIndexEnum.Lower;
            }
            else if (_charPos.position.y >= _upBorder)
            {
                _sideIndex = SideIndexEnum.Upper;
            }
            else if ((_charPos.position.x <= _leBorder))
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
            _charPos.position = new Vector3(_charPos.position.x, _upBorder, _charPos.position.z);
            _sideIndex = SideIndexEnum.Upper;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (_charPos.position.x > _leBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToLeft;
            }
            else if (_charPos.position.x <= _leBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            else if (_charPos.position.x < _riBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToRight;
            }
            else if (_charPos.position.x >= _riBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_charPos.position.x < _riBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToRight;
            }
            else if (_charPos.position.x >= _riBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            else if (_charPos.position.x > _leBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToLeft;
            }
            else if (_charPos.position.x <= _leBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charPos.position.x <= _leBorder)
            {
                _sideIndex = SideIndexEnum.Left;
            }
            else if (_charPos.position.x >= _riBorder)
            {
                _sideIndex = SideIndexEnum.Right;
            }
            else if (_charPos.position.y >= _upBorder)
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
            if (_charPos.position.x <= _leBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            else if (_charPos.position.x < _riBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToRight;
            }
            else if (_charPos.position.x >= _riBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            else if (_charPos.position.x > _leBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToLeft;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_charPos.position.x <= _leBorder && _charPos.position.y > _loBorder)
            {
                _move = _moveToDown;
            }
            else if (_charPos.position.x < _riBorder && _charPos.position.y <= _loBorder)
            {
                _move = _moveToRight;
            }
            else if (_charPos.position.x >= _riBorder && _charPos.position.y < _upBorder)
            {
                _move = _moveToUp;
            }
            else if (_charPos.position.x > _leBorder && _charPos.position.y >= _upBorder)
            {
                _move = _moveToLeft;
            }
            _characterRb.velocity = _move * _characterSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _charPos.position = new Vector3(_riBorder, _charPos.position.y, _charPos.position.z);
            _sideIndex = SideIndexEnum.Right;
        }
        else
        {
            _characterRb.velocity = Vector3.zero;
            if (_charPos.position.y <= _loBorder)
            {
                _sideIndex = SideIndexEnum.Lower;
            }
            else if (_charPos.position.y >= _upBorder)
            {
                _sideIndex = SideIndexEnum.Upper;
            }
            else if (_charPos.position.x >= _riBorder)
            {
                _sideIndex = SideIndexEnum.Right;
            }
        }
    }
}
