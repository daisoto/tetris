using System;
using UniRx;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] InputManager inputManager = null;

    [SerializeField] Transform cameraTransform = null;

    [SerializeField] float maxDistance = -8;
    [SerializeField] float minDistance = -17;

    IDisposable subscription = null;

    private void OnEnable()
    {
        subscription = inputManager.onScroll.Subscribe(scrollValue =>
        {
            float distance = Mathf.Clamp(scrollValue + cameraTransform.transform.localPosition.z, minDistance, maxDistance);
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y, distance);
        });
    }

    private void OnDisable()
    {
        subscription.Dispose();
    }
}
