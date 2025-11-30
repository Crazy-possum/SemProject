using UnityEngine;
using UnityEngine.UI;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private MousePosition3D _mousePosition;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Slider _reloadSlider;

    private CharacterShoot _characterShoot;
    private GameObject _character;
    private Vector3 _startPosotion = new Vector3(0, 9.5f, 0);

    private void Start()
    {
        GameObject sceneGObject = GameObject.Instantiate(_characterPrefab, _startPosotion, Quaternion.identity);
        _character = sceneGObject;
        _characterShoot = _character.GetComponent<CharacterShoot>();
        _characterShoot.ReloadSlider = _reloadSlider;
    }

}
