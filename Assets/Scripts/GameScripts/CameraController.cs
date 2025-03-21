using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float offsetX = 5f;
    public float offsetY = 5f;
    public float smoothSpeed = 5f;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

    private Vector3 originalPosition;
    private bool isShaking = false;
    private bool stopCamera = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (stopCamera || player == null)
            return;

        float targetX = player.position.x + offsetX;
        float smoothX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(smoothX, offsetY, -10);
    }

    public void StopCamera()
    {
        stopCamera = true;
    }

    public void StartCameraShake()
    {
        if (!isShaking)
            StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        isShaking = true;
        originalPosition = transform.position;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isShaking = false;
    }
}
