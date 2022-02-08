using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRCamera : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 140f;
    [SerializeField] private float zoomTime = 0.1f;
    [SerializeField] private float maxHeight = 100f;
    [SerializeField] private float minHeight = 10f;
    [SerializeField] private float dragPanSpeed = 50f;
    [SerializeField] private float keyPanSpeed = 50f;
    [SerializeField] private KeyCode mouseRotationKey;
    [SerializeField] private bool useKeyboardRotation;
    [SerializeField] private bool useMouseRotation;
    [SerializeField] private float mouseRotationSpeed;
    [SerializeField] private float rotSmoothFactor;
    [SerializeField] private float screenEdgeBorder = 25f;
    [SerializeField] private float screenEdgeMovementSpeed = 3f;
    [SerializeField] private float limitX = 50f; //x limit of map
    [SerializeField] private float limitY = 50f; //z limit of map
    private Vector2 MouseAxis
    {
        get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
    }
    private Vector2 MouseInput
    {
        get { return Input.mousePosition; }
    }

    private float zoomVelocity = 0f;
    private float targetHeight;
    private Vector3 newPosition = new Vector3();
    private Vector3 focusPosition = new Vector3(0, 0, 0);
    private Vector3 offset = new Vector3();

    void Start()
    {
        // Start zoomed out
        targetHeight = maxHeight;
        offset = transform.position;
    }

    void LateUpdate()
    {
        newPosition = transform.position;
        offset = transform.position - focusPosition;

        Zoom();
        Pan();
        Rotation();

        transform.position = newPosition;
        transform.LookAt(focusPosition);
    }
    private void Zoom()
    {
        // First, calculate the height we want the camera to be at
        targetHeight += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1f;
        targetHeight = Mathf.Clamp(targetHeight, minHeight, maxHeight);

        // Then, interpolate smoothly towards that height
        newPosition.y = Mathf.SmoothDamp(transform.position.y, targetHeight, ref zoomVelocity, zoomTime);
    }
    private void Pan()
    {
        // Always pan the camera using the keys
        var pan = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * keyPanSpeed * Time.deltaTime;

        // Optionally pan the camera by either dragging with middle mouse or when the cursor touches the screen border
        if (Input.GetMouseButton(2))
        {
            pan -= MouseAxis * dragPanSpeed * Time.deltaTime;
        }
        else if (pan == Vector2.zero) //Screen border pan
        {
            Rect leftRect = new Rect(0, 0, screenEdgeBorder, Screen.height);
            Rect rightRect = new Rect(Screen.width - screenEdgeBorder, 0, screenEdgeBorder, Screen.height);
            Rect upRect = new Rect(0, Screen.height - screenEdgeBorder, Screen.width, screenEdgeBorder);
            Rect downRect = new Rect(0, 0, Screen.width, screenEdgeBorder);

            pan.x = leftRect.Contains(MouseInput) ? -1 : rightRect.Contains(MouseInput) ? 1 : 0;
            pan.y = upRect.Contains(MouseInput) ? 1 : downRect.Contains(MouseInput) ? -1 : 0;

            pan *= screenEdgeMovementSpeed * Time.deltaTime;
        }

        //input applies to the red axis of the camera, but to vector fwd for vertical axis, bc you want the camera to stay in the same plane.
        var moveHor = transform.right * pan.x;
        var moveVer = Vector3.forward;

        //this is like projecting transform.forward to the horizontal plane at which the camera is
        float yrot = transform.rotation.eulerAngles.y;
        moveVer = Quaternion.AngleAxis(yrot, Vector3.up) * moveVer;
        moveVer *= pan.y;

        newPosition += moveHor + moveVer;
        focusPosition += moveHor + moveVer;

    }
    private void Rotation()
    {
        if (useMouseRotation && Input.GetKey(mouseRotationKey))
        {
            Quaternion rotAngle = Quaternion.AngleAxis(MouseAxis.x * mouseRotationSpeed, transform.up);
            offset = rotAngle * offset;

            Vector3 objPosition = focusPosition + offset;

            newPosition = Vector3.Slerp(newPosition, objPosition, rotSmoothFactor);
        }
    }
    private void LimitPosition() //for future implementation
    {
        var separation = 60;
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, -limitX, limitX), newPosition.y, Mathf.Clamp(newPosition.z, -limitY, limitY));
        focusPosition = new Vector3(Mathf.Clamp(focusPosition.x, -limitX + separation, limitX - separation), 
                                    focusPosition.y, 
                                    Mathf.Clamp(focusPosition.z, -limitY + separation, limitY - separation));
    }
}