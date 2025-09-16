using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
    [SerializeField] private ClickController _clickController;
    
    [SerializeField] private Camera _mainCamera;

    public Transform MauseTransform;
    private GameObject _currentObject;

    private void Start()
    {
    }

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;  

            if (Physics.Raycast(ray, out raycastHit))
            {
                _currentObject = raycastHit.collider.gameObject;
                _clickController.ObjectUnderMouse = _currentObject;

                _clickController.ClickBehavior();
            }
        }
    }
}
