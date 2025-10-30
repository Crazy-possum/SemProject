using UnityEngine;
using UnityEngine.UI;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private MousePosition3D _mousePosition;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Slider _reloadSlider;

    private CharacterShoot _characterShoot;
    private GameObject _character;
    private Vector3 _startPosotion = new Vector3(0, 10.8f, 0);
    private float _currentTimerTime;
    private float _reloadTime;

    private void Start()
    {
        GameObject sceneGObject = GameObject.Instantiate(_characterPrefab, _startPosotion, Quaternion.identity);
        _character = sceneGObject;
        _characterShoot = _character.GetComponent<CharacterShoot>();

        TimerSliderUpdate();
    }

    private void FixedUpdate()
    {
        TimerSliderUpdate();
    }

    private void TimerSliderUpdate()
    {
        _currentTimerTime = _characterShoot.CurrentTime;
        _reloadTime = _characterShoot.AttakReload;

        _reloadSlider.maxValue = _reloadTime;
        _reloadSlider.value = _currentTimerTime;
    }
}
