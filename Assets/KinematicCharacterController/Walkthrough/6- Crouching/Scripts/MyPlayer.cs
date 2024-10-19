using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

namespace KinematicCharacterController.Walkthrough.Crouching
{
    public class MyPlayer : MonoBehaviour
    {
        //public ExampleCharacterCamera OrbitCamera;
        public Transform CameraFollowPoint;
        public MyCharacterController Character;

        public float sensitivity = 2f;
        public float yRotationLimit = 88f;
        Vector2 rotation = Vector2.zero;
        Quaternion cameraRot = Quaternion.identity;


        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform
            //OrbitCamera.SetFollowTransform(CameraFollowPoint);

            //// Ignore the character's collider(s) for camera obstruction checks
            //OrbitCamera.IgnoredColliders.Clear();
            //OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                return;
            }

            rotation.x += Input.GetAxis(MouseXInput);
            rotation.y += Input.GetAxis(MouseYInput);
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
            cameraRot = xQuat * yQuat;
            Camera.main.transform.localRotation = cameraRot;

            Vector3 cameraPos = this.transform.position;
            cameraPos.y += 1.6f;
            Camera.main.transform.position = cameraPos;
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = cameraRot;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }
    }
}