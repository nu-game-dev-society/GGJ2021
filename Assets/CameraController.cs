using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraControls playerInput;
    public Transform target;
    public float damping;
    Vector3 offset;
    float zoomScale = 100f;
    bool introCompleted;

    const float MIN_ZOOM = 10f;
    const float MAX_ZOOM = 100f;

    void Awake()
    {
        InitialiseControls();
    }
    void Start()
    {
        zoomScale = 10f;
        offset = transform.position - target.position;

        StartCoroutine(IntroZoom());
    }

    private void InitialiseControls()
    {
        playerInput = new CameraControls();
        playerInput.Camera.Zoom.performed += OnZoom;
        playerInput.Camera.SecondaryTouchContact.started += StartTouchZoom;
        playerInput.Camera.SecondaryTouchContact.canceled += EndTouchZoom;

    }
    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = Vector3.Lerp(transform.position, target.position + (offset.normalized * zoomScale), Time.deltaTime * damping);
        transform.position = point;
        transform.LookAt(point);
    }
    private Coroutine zoomCoroutine;
    public void StartTouchZoom(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        zoomCoroutine = StartCoroutine(TouchZoomDetection());
    }
    public void EndTouchZoom(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StopCoroutine(zoomCoroutine);
    }

    IEnumerator TouchZoomDetection()
    {
        float previousDistance = Vector2.Distance(playerInput.Camera.PrimaryFingerPosition.ReadValue<Vector2>(),
                playerInput.Camera.SecondaryFingerPosition.ReadValue<Vector2>());
        float distance = 0.0f;
        while (true)
        {
            distance = Vector2.Distance(playerInput.Camera.PrimaryFingerPosition.ReadValue<Vector2>(),
                playerInput.Camera.SecondaryFingerPosition.ReadValue<Vector2>());
            ChangeZoom((distance - previousDistance) * -1.0f);
            previousDistance = distance;
            yield return null;
        }
    }
    void ChangeZoom(float amount)
    {
        if (introCompleted)
            zoomScale = Mathf.Clamp(zoomScale + amount, MIN_ZOOM, MAX_ZOOM);
    }
    void ZoomIn() => ChangeZoom(-1f);
    void ZoomOut() => ChangeZoom(1f);

    public void OnZoom(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        float ctxValue = ctx.ReadValue<float>();
        if (ctxValue < 0)
        {
            ZoomOut();
        }
        else if (ctxValue > 0)
        {
            ZoomIn();
        }
    }
    public void OnZoomTouch(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Vector2 finger1 = ctx.ReadValue<Vector2>();
    }

    public IEnumerator IntroZoom()
    {
        yield return new WaitForSeconds(2f);

        while (zoomScale < 50f)
        {
            zoomScale = Mathf.Lerp(zoomScale, 51f, Time.deltaTime * 3f);
            yield return null;
        }

        introCompleted = true;
    }
}
