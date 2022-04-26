using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 controllerStartMovementPosition = new Vector3(0, 0, 0);
    private Vector3 playerStartMovementPosition = new Vector3(0, 0, 0);
    private Vector3 controllerDrag = new Vector3(0, 0, 0);
    public float moveSpeed;
    public float moveIntensity;
    public GameObject controllerPrefab;
    public GameObject controllerL;
    public GameObject controllerR;
    public GameObject LHAnchor;
    public GameObject RHAnchor;

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

    private void Update() //should it be lateupdate?
    {
        movePress = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.2f ^ OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.2f;
        zoomPress = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.2f && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.2f;

        if (zoomPress) Zoom();
        else zoomPressed = false;
        if (movePress) Move();
        
        else if (movePressed) movePressed = false;
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
        zoomIntensity = separation / initialControllerSeparation;


        Vector3 targetPos = startZoomPosition + centerEyeCamera.transform.forward * (zoomIntensity - 1) * zoomSensitivity;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, zoomSpeed * Time.deltaTime);


    }

    private void Move()
    {
        if (!movePressed)
        {
            activeMovementController = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0 ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;
            controllerStartMovementPosition = OVRInput.GetLocalControllerPosition(activeMovementController);
            playerStartMovementPosition = transform.position;



            movePressed = true;
            return;

        }




        controllerDrag += (OVRInput.GetLocalControllerPosition(activeMovementController) - controllerStartMovementPosition) - controllerDrag;
        Vector3 targetPos = playerStartMovementPosition - controllerDrag * moveIntensity;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
    }
}
