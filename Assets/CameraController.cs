using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping;
    Vector3 offset;
    float zoomScale = 100f;
    bool introCompleted;

    const float MIN_ZOOM = 10f;
    const float MAX_ZOOM = 100f;


    void Start()
    {
        zoomScale = 10f;
        offset = transform.position - target.position;

        StartCoroutine(IntroZoom());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = Vector3.Lerp(transform.position, target.position + (offset.normalized *  zoomScale), Time.deltaTime * damping);
        transform.position = point;
        transform.LookAt(point);
    }

    void ChangeZoom(float amount)
    {
        if(introCompleted)
            zoomScale = Mathf.Clamp(zoomScale + amount, MIN_ZOOM, MAX_ZOOM);
    }
    void ZoomIn() => ChangeZoom(-1f);
    void ZoomOut() => ChangeZoom(1f);

    public void OnZoom(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        float ctxValue = ctx.ReadValue<float>();
        if(ctxValue < 0)
        {
            ZoomOut();
        }
        else if(ctxValue > 0)
        {
            ZoomIn();
        }
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
