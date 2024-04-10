using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

[RequireComponent(typeof(Camera))]
public class InputMouse : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionWithMouse;

    private Camera _camera;
    private RaycastHit _hit;
    private Ray _ray;
    private float _raycastDistance;

    private void Awake()
    {
        _raycastDistance = 100;
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, _raycastDistance, _interactionWithMouse))
            {
                if (_hit.collider.TryGetComponent<CubeDivisible>(out CubeDivisible cubeDivisible))
                {
                    cubeDivisible.OnClick();
                }
            }
        }
    }
}
