using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 startMovementPosition = new Vector3(0, 0, 0);

    private Vector3 startZoomPositionL = new Vector3(0, 0, 0);
    private Vector3 startZoomPositionR = new Vector3(0, 0, 0);
    private Vector3 startZoomPosition = new Vector3(0, 0, 0);
    private Vector3 zoomPositionL = new Vector3(0, 0, 0);
    private Vector3 zoomPositionR = new Vector3(0, 0, 0);
    private float initialControllerSeparation = 0;
    float separation;
    private float zoomIntensity;
    public float zoomSpeed;
    public float zoomSensitivity;


    private OVRInput.Controller activeMovementController;
    private bool movePress = false;
    private bool zoomPress = false;
    private bool movePressed = false;
    private bool zoomPressed = false;

    [SerializeField] private OVRCameraRig cameraRig;
    [SerializeField] private Camera centerEyeCamera;

    private CharacterController player;
    private float playerMovSpeed = 10f;

    private Vector3 movDir = new Vector3(0.002f, 0.003f, 0.005f);

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        startMovementPosition = transform.TransformPoint(OVRInput.GetLocalControllerPosition(activeMovementController));
        startZoomPositionL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        startZoomPositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

    }
    private void Update() //should it be lateupdate?
    {
        movePress = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.2f ^ OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.2f;
        zoomPress = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.2f && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.2f;

        if (zoomPress) Zoom();
        else zoomPressed = false;
        Move();

    }

    private void Zoom()
    {
       
        if (!zoomPressed)
        {
            startZoomPositionL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            startZoomPositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            initialControllerSeparation = (startZoomPositionL - startZoomPositionR).magnitude;
            startZoomPosition = transform.position;


            zoomPressed = true;
            return;
        }
        zoomPositionL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        zoomPositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        
        separation += (zoomPositionL - zoomPositionR).magnitude - separation; 
        //float separation = (OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch) - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch)).magnitude;
        zoomIntensity = separation / initialControllerSeparation;


        Vector3 targetPos = startZoomPosition + centerEyeCamera.transform.forward * (zoomIntensity - 1) * zoomSensitivity;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, zoomSpeed * Time.deltaTime);
        //player.Move(step);


        //if (zoomIntensity > 1 + zoomSensitivity) player.Move(centerEyeCamera.transform.forward * zoomIntensity * zoomSpeed * Time.deltaTime);
        //else if (zoomIntensity < 1 - zoomSensitivity) player.Move(-centerEyeCamera.transform.forward * zoomIntensity * zoomSpeed * Time.deltaTime);

    }

    private void Move()
    {
        if (movePress && !movePressed)
        {
            activeMovementController = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0 ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;

            startMovementPosition = transform.TransformPoint(OVRInput.GetLocalControllerPosition(activeMovementController));

            movePressed = true;
            return;

        }

        if (!movePress)
        {
            movePressed = false;
            return;
        }

        Vector3 moveDir = transform.TransformPoint(OVRInput.GetLocalControllerPosition(activeMovementController)) - startMovementPosition;
        moveDir.Normalize();
        float moveInt = (cameraRig.centerEyeAnchor.position - startMovementPosition).magnitude;

        player.Move(-moveDir * moveInt * playerMovSpeed * Time.deltaTime);
    }
}
