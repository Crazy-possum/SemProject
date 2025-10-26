using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class MousePosition3D : MonoBehaviour
{
    [SerializeField] private ClickController _clickController;
    [SerializeField] private Camera _mainCamera;

    private GameObject _currentObject;

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            transform.position = raycastHit.point;

            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, LayerMask.GetMask("Interactive")) )
            {
                _currentObject = raycastHit.collider.gameObject;
                _clickController.ObjectUnderMouse = _currentObject;

                _clickController.ClickBehavior();
            }
        }
    }
}
