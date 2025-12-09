using UnityEngine;
using UnityEngine.SceneManagement;

public class MousePosition3D : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private ClickController _clickController;
    [SerializeField] private MoveOnClick _moveOnClick;
    [Tooltip("Камера")]
    [SerializeField] private Camera _mainCamera;

    private const string _interactlayer = "Interactive";
    private const string _groundlayer = "Ground";

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, 
            LayerMask.GetMask(_groundlayer)))
        {
            transform.position = raycastHit.point;

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _moveOnClick.ClickChecker(raycastHit.point);
            }

            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, 
                LayerMask.GetMask(_interactlayer)))
            {
                GameObject currentObject = raycastHit.collider.gameObject;

                _clickController.ClickBehavior(currentObject);
            }
        }
    }
}
