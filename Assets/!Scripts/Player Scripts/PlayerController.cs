using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public Text SensitivityText;

    public Transform playerCamera;
    public Transform bowCam;
    public Transform meleeCam;
    [SerializeField] public float mouseSensitivity;
    [SerializeField] public float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float jumpForce = 50.0f;
    //[SerializeField] bool lockCursor = true;
    [SerializeField] float runSpeed = 45.0f;
    //[SerializeField] Transform playerBody;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    float currSpeed = 0.0f;
    float BowCamClamp = 89.0f;
    float MeleeCamClamp = 11.25f;
    float CurrentClamp;
    public CharacterController controller = null;

    //private float clampDegreeUp = 75;
    //private float clampDegreeDown = -90f;
    //float xRotation;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = GlobalData.sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        //SensitivityText.text = "" + GlobalData.sensitivity;
        //SensitivityText.text = GlobalData.sensitivity.ToString("f2"); 
        UpdateMouseLook();
        UpdateMovement();
        if(GetComponent<SwitchArms>().BowArms.activeInHierarchy)
        {
            playerCamera = bowCam;
            CurrentClamp = BowCamClamp;
        }
        if(GetComponent<SwitchArms>().MeleeArms.activeInHierarchy)
        {
            playerCamera = meleeCam;
            CurrentClamp = MeleeCamClamp;
        }
    }

    public void SensitivitySlider(float value)
    {
        mouseSensitivity = value;
        GlobalData.sensitivity = mouseSensitivity;
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        
        cameraPitch = Mathf.Clamp(cameraPitch, -CurrentClamp, CurrentClamp);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
            if (Input.GetButtonDown("Jump"))
            {
                velocityY = jumpForce;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currSpeed = walkSpeed + runSpeed;
        }
        else
        {
            currSpeed = walkSpeed;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);


    }
    public bool IsQuiet()
    {
        if (!controller.isGrounded && controller.velocity.magnitude <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsJumping()
    {
        return controller.isGrounded;
    }

    public bool IsWalking()
    {
        //!m_Jumping??? ???&&??? ???m_IsWalking??? ???&&??? ???m_CharacterController.velocity.magnitude??? ???>??? ???0
        if (!controller.isGrounded && currSpeed > 0 && controller.velocity.magnitude > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRunning()
    {
        if (!controller.isGrounded && !(currSpeed > 0) && controller.velocity.magnitude > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
