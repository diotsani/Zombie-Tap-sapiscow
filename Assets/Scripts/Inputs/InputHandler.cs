using UnityEngine;

namespace Sapi.ZombieTap.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastObject(Input.mousePosition);
            }
        }

        private void RaycastObject(Vector2 screenPosition)
        {
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                IRaycastable raycastableObj = hit.collider.GetComponent<IRaycastable>();
                raycastableObj?.OnRaycasted();
            }
        }
    }
}