using UnityEngine;

public class TowerAttak : MonoBehaviour
{
    [Tooltip("������ �������� ����")]
    [SerializeField] private GameObject _towerBulletPrefab;
    [Tooltip("�����, �� ������� �������� ����")]
    [SerializeField] private Transform _bulletSpawner;
    //[Tooltip("������")]
    //[SerializeField] private float _rotationSpeed = 2;
    [Tooltip("������ ����������� � ���")]
    [SerializeField] private float _attakReload = 1;

    private GameObject _currentTarget;
    private Timer _attakTimer;

    public GameObject CurrentTarget { get => _currentTarget; set => _currentTarget = value; }

    private void Start()
    {
        _attakTimer = new Timer(_attakReload);
    }

    private void FixedUpdate()
    {
        _attakTimer.Wait();

        if (_currentTarget != null)
        {
            if (!_attakTimer.StartTimer)
            {
                _attakTimer.StartCountdown();
            }

            if (_attakTimer.ReachingTimerMaxValue == true)
            {
                Attak();
                TowerRotate();
                _attakTimer.StopCountdown();
            }
        }

        //TODO ������� ������� �����
        // Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;
        // Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        //transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    public void Attak()
    {
        Vector3 position = _bulletSpawner.position;
        GameObject localBullet = GameObject.Instantiate(_towerBulletPrefab, position, Quaternion.identity, _bulletSpawner);

        localBullet.GetComponent<TowerBulletBehavior>().BulletsCurrentTarget = _currentTarget;
    }

    private void TowerRotate()
    {
        //TODO ������� ������� �����
        // gameObject.transform.LookAt(CurrentTarget.transform);
    }

}

