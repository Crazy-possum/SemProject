using UnityEngine;

public class ClickController : MonoBehaviour
{
    [Tooltip("������")]
    [SerializeField] private TowerBuilder _towerBuilder;

    public void ClickBehavior(GameObject objectUnderMouse)
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (objectUnderMouse.TryGetComponent(out TowerBehavior towerBehavior))
            {
                _towerBuilder.BuildPointObject = objectUnderMouse;
                _towerBuilder.SpawnTower();
            }
        }
    }
}
