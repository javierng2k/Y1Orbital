using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }

    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FPSCamera.instance.isFPS) {
            FPSMove();
        } else {
            Move();
            Rotate();
        }

        //DebugingControls();
    }

    void DebugingControls() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayerStats.instance.LoseHealth(10);
        }
    }

    void Rotate() {
        
        Vector3 direction = PlayerMouseHandler.instance.GetMouseDir().normalized;
        direction = new Vector3(direction.x, transform.position.y, direction.z);
        playerModel.transform.rotation = Quaternion.LookRotation(-Vector3.Cross(transform.up, direction), transform.up);
    }

    // Handles the 4 direction input for movement when not FPS
    void Move() {
        
        if (FPSCameraShift.instance.startShift) {return;}
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_UP))) {
            moveDir += Vector3.forward;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_DOWN))) {
            moveDir -= Vector3.forward;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_LEFT))) {
            moveDir -= Vector3.right;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_RIGHT))) {
            moveDir += Vector3.right;
        }

        transform.position += moveDir.normalized  * PlayerStats.instance.GetSpd() * Time.deltaTime;
    }

    // Handles the 4 direction input for movement when FPS
    void FPSMove() {
        
        if (FPSCameraShift.instance.startShift) {return;}
        Vector3 moveDir = Vector3.zero;
        Vector3 forward = Camera.main.transform.forward;
        forward = new Vector3(forward.x, transform.position.y, forward.z);
        Vector3 right = Camera.main.transform.right;
        right = new Vector3(right.x, transform.position.y, right.z);

        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_UP))) {
            moveDir += forward;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_DOWN))) {
            moveDir -= forward;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_LEFT))) {
            moveDir -= right;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_RIGHT))) {
            moveDir += right;
        }
        transform.position += moveDir.normalized * PlayerStats.instance.GetSpd() * Time.deltaTime;
    }
}
