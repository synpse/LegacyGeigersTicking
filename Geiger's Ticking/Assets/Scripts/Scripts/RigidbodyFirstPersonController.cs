using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RigidbodyFirstPersonController : MonoBehaviour
{
    [Serializable]
    public class MovementSettings
    {
        public float ForwardSpeed = 3f;   // Speed when walking forward
        public float BackwardSpeed = 2f;  // Speed when walking backwards
        public float StrafeSpeed = 2f;    // Speed when walking sideways
        public float RunMultiplier = 2f;   // Speed multiplier when sprinting
        public KeyCode RunKey = KeyCode.LeftShift;
        public float JumpForce = 40f;
        public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
        [HideInInspector] public float CurrentTargetSpeed = 8f;

#if !MOBILE_INPUT
        private bool m_Running;
#endif

        public void UpdateDesiredTargetSpeed(Vector2 input)
        {
            if (input == Vector2.zero) return;
            if (input.x > 0 || input.x < 0)
            {
                //strafe
                CurrentTargetSpeed = StrafeSpeed;
            }
            if (input.y < 0)
            {
                //backwards
                CurrentTargetSpeed = BackwardSpeed;
            }
            if (input.y > 0)
            {
                //forwards
                //handled last as if strafing and moving forward at the same time forwards speed should take precedence
                CurrentTargetSpeed = ForwardSpeed;
            }
#if !MOBILE_INPUT
            if (Input.GetKey(RunKey))
            {
                CurrentTargetSpeed *= RunMultiplier;
                m_Running = true;
            }
            else
            {
                m_Running = false;
            }
#endif
        }

#if !MOBILE_INPUT
        public bool Running
        {
            get { return m_Running; }
        }
#endif
    }

    [Serializable]
    public class AdvancedSettings
    {
        public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
        public float stickToGroundHelperDistance = 0.5f; // stops the character
        public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
        public bool airControl; // can the user control the direction that is being moved in the air
        [Tooltip("set it to 0.1 or more if you get stuck in wall")]
        public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
    }

    [Header("Components")]
    public Canvas pauseMenuCanvas;
    public Camera cam;
    public Light lantern;
    public Light lantern2;
    public MovementSettings movementSettings = new MovementSettings();
    public MouseLook mouseLook = new MouseLook();
    public AdvancedSettings advancedSettings = new AdvancedSettings();

    private Rigidbody m_RigidBody;
    private CapsuleCollider m_Capsule;
    private float m_YRotation;
    private Vector3 m_GroundContactNormal;
    private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;
    private bool crouching;
    private bool keydown;


    public Vector3 Velocity
    {
        get { return m_RigidBody.velocity; }
    }

    public bool Grounded
    {
        get { return m_IsGrounded; }
    }

    public bool Jumping
    {
        get { return m_Jumping; }
    }

    public bool Running
    {
        get
        {
#if !MOBILE_INPUT
            return movementSettings.Running;
#else
	            return false;
#endif
        }
    }

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
        mouseLook.Init(transform, cam.transform);
        // Start off
        lantern.gameObject.SetActive(!lantern.gameObject.activeInHierarchy);
        lantern2.gameObject.SetActive(!lantern2.gameObject.activeInHierarchy);
    }


    private void Update()
    {
        // If "ESC" is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // On keypress turn on or off depending on context
            // activeInHierarchy returns a bool with active or not active
            // If ON => OFF, if OFF => ON
            pauseMenuCanvas.gameObject.SetActive(!pauseMenuCanvas.gameObject.activeInHierarchy);
        }

        // If "\" is pressed and game is not paused
        if (Input.GetKeyDown(KeyCode.Backslash) && !pauseMenuCanvas.gameObject.activeInHierarchy)
        {
            // On keypress turn on or off depending on context
            // activeInHierarchy returns a bool with active or not active
            // If ON => OFF, if OFF => ON
            DeveloperConsole.Instance.consoleCanvas.gameObject.SetActive
            (!DeveloperConsole.Instance.consoleCanvas.gameObject.activeInHierarchy);
        }

        // If "F" is pressed
        if (Input.GetKeyDown(KeyCode.F) && !pauseMenuCanvas.gameObject.activeInHierarchy
            && !DeveloperConsole.Instance.consoleCanvas.gameObject.activeInHierarchy)
        {
            // On keypress turn on or off depending on context
            // activeInHierarchy returns a bool with active or not active
            // If ON => OFF, if OFF => ON
            lantern.gameObject.SetActive(!lantern.gameObject.activeInHierarchy);
            lantern2.gameObject.SetActive(!lantern2.gameObject.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            gameObject.GetComponent<CapsuleCollider>().height = gameObject.GetComponent<CapsuleCollider>().height / 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            gameObject.GetComponent<CapsuleCollider>().height = gameObject.GetComponent<CapsuleCollider>().height * 2;
        }

        // If console or pause is active
        if (DeveloperConsole.Instance.consoleCanvas.gameObject.activeInHierarchy 
            || pauseMenuCanvas.gameObject.activeInHierarchy)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        RotateView();

        if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump)
        {
            m_Jump = true;
        }
    }


    private void FixedUpdate()
    {
        // Added this for noclip
        if (m_RigidBody.isKinematic)
        {
            if (Input.GetKey(KeyCode.W))
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.Translate(Vector3.forward * 0.5f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.Translate(Vector3.back * 0.5f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.Translate(Vector3.left * 0.5f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.Translate(Vector3.right * 0.5f);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.Translate(Vector3.up * 0.25f);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.Translate(Vector3.down * 0.25f);
            }
        }
        else
        {
            GroundCheck();
            Vector2 input = GetInput();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
            {
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
                desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
                desiredMove.y = desiredMove.y * movementSettings.CurrentTargetSpeed;
                if (m_RigidBody.velocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
                {
                    m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                }
            }

            if (m_IsGrounded)
            {
                m_RigidBody.drag = 5f;

                if (m_Jump)
                {
                    m_RigidBody.drag = 0f;
                    m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                    m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                    m_Jumping = true;
                }

                if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
                {
                    m_RigidBody.Sleep();
                }
            }
            else
            {
                m_RigidBody.drag = 0f;
                if (m_PreviouslyGrounded && !m_Jumping)
                {
                    StickToGroundHelper();
                }
            }
            m_Jump = false;
        }
    }


    private float SlopeMultiplier()
    {
        float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
        return movementSettings.SlopeCurveModifier.Evaluate(angle);
    }


    private void StickToGroundHelper()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) +
                               advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
            {
                m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
            }
        }
    }


    private Vector2 GetInput()
    {

        Vector2 input = new Vector2
        {
            x = CrossPlatformInputManager.GetAxis("Horizontal"),
            y = CrossPlatformInputManager.GetAxis("Vertical")
        };
        movementSettings.UpdateDesiredTargetSpeed(input);
        return input;
    }


    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        // get the rotation before it's changed
        float oldYRotation = transform.eulerAngles.y;

        mouseLook.LookRotation(transform, cam.transform);

        if (m_IsGrounded || advancedSettings.airControl)
        {
            // Rotate the rigidbody velocity to match the new direction that the character is looking
            Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
            m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
        }
    }

    /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
    private void GroundCheck()
    {
        m_PreviouslyGrounded = m_IsGrounded;
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            m_IsGrounded = true;
            m_GroundContactNormal = hitInfo.normal;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundContactNormal = Vector3.up;
        }
        if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
        {
            m_Jumping = false;
        }
    }
}
