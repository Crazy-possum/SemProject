using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private ClickController _clickController;
    [Tooltip("Камера")]
    [SerializeField] private Camera _mainCamera;

    private const string _interactlayer = "Interactive";

    private void FixedUpdate()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            transform.position = raycastHit.point;

            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, 
                LayerMask.GetMask(_interactlayer)))
            {
                GameObject currentObject = raycastHit.collider.gameObject;

                _clickController.ClickBehavior(currentObject);
            }
        }
    }
}
