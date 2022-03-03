 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot;

    [SerializeField] private bool invert;

    [SerializeField] private float sensivity = 5f;

    [SerializeField] private float roll_Angle = 0;

    [SerializeField] private Vector2 default_Look_Limits = new Vector2(-70f,80f);

    private Vector2 look_Angle;
    private Vector2 Current_Mouse_Look;

    private float current_Roll_Angle;

    // Start is called before the first frame update
    void Start()
    {
        //locking the mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Toggle mose lock state using escape key
        LockAndUnlockCursor();

        //moving around with mouse if only mouse is locked
        if ((Cursor.lockState == CursorLockMode.Locked && !GameManager.isPlayerDead) || UI_Handler.isAndroidActive) 
        {
            LookAround();
        }

    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        //Getting mouse movement
        Current_Mouse_Look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        //Override input if in android mode
        if(UI_Handler.isAndroidActive)
        {
            Current_Mouse_Look = new Vector2(SimpleInput.GetAxis("mouseY"), SimpleInput.GetAxis("mouseX"));        
        }

        // setting the look sensivity
        look_Angle.x += Current_Mouse_Look.x * sensivity * (invert ? 1f : -1f);
        look_Angle.y += Current_Mouse_Look.y * sensivity;

        //locking the look on up and down to the limits
        look_Angle.x = Mathf.Clamp(look_Angle.x, default_Look_Limits.x, default_Look_Limits.y);

        //setting the roll angle to rotate the player and the look root  but with smooth
        current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw("Mouse X") * roll_Angle, Time.deltaTime * roll_Angle);

        //rotating the player and the look root
        lookRoot.localRotation = Quaternion.Euler(look_Angle.x, 0f, current_Roll_Angle);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angle.y, 0f);
    }
}
