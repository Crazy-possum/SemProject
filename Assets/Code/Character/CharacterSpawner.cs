using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private MousePosition3D _mousePosition;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Slider _reloadSlider;

    private GameObject _character;
    private float _currentTimerTime;
    private float _reloadTime;

    private void Start()
    {
        Vector3 position = new Vector3 (0,10.8f,0);
        GameObject sceneGObject = GameObject.Instantiate(_characterPrefab, position, Quaternion.identity);

        _character = sceneGObject;
        TimerSliderUpdate();
    }

    private void Update()
    {
        TimerSliderUpdate();
    }

    private void TimerSliderUpdate()
    {
        _currentTimerTime = _character.GetComponent<CharacterShoot>().CurrentTime;
        _reloadTime = _character.GetComponent<CharacterShoot>().AttakReload;

        _reloadSlider.maxValue = _reloadTime;
        _reloadSlider.value = _currentTimerTime;
    }
}
