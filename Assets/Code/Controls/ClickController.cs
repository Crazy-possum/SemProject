using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] private TowerBuilder _towerBuilder;

    public GameObject ObjectUnderMouse;

    public void ClickBehavior()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (ObjectUnderMouse.TryGetComponent(out TowerBehavior towerBehavior))
            {
                _towerBuilder.BuildPointObject = ObjectUnderMouse;
                _towerBuilder.SpawnTower();
            }
        }
    }
}
