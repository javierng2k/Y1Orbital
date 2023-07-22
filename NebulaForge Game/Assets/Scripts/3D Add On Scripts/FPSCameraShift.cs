using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraShift : MonoBehaviour
{
    public static FPSCameraShift instance;

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

    public float mvspd;
    public float rspd;
    public Transform player;
    public bool startShift;
    public Transform bossEnemy;
    public FPSCamera cam;
    public GameObject playerMouseIndicator;
    public GameObject topDownCanvas;
    public GameObject fpsCanvas;
    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        startShift = false;
    }

    // Update is called once per frame
    void Update()
    {
        DebugingControls();

        if (startShift) {
            RotateToFPS();
            MoveToPlayer();
        }
    }

    void MoveToPlayer() {
        Camera.main.transform.position += (player.position - Camera.main.transform.position).normalized * mvspd * Time.deltaTime;
        if (Vector3.Distance(Camera.main.transform.position, player.position) < 0.01f) {
            StopShift();
        }
    }

    void RotateToFPS() {
        Vector3 to = new Vector3(0, 0, 0);
        if (Vector3.Distance(Camera.main.transform.eulerAngles, to) > 0.01f) {
            Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.rotation.eulerAngles, to, Time.deltaTime * rspd);
        }
        else {
            Camera.main.transform.eulerAngles = to;
            StopShift();
        }
    }

    void DebugingControls() {
        if (Input.GetKeyDown(KeyCode.O)) {
            StartShift();
        }
    }

    public void StartShift() {
        startShift = true;
        playerMouseIndicator.SetActive(false);
        topDownCanvas.SetActive(false);
        fpsCanvas.SetActive(true);
    }

    void StopShift() {
        startShift = false;
        cam.isFPS = true;
        Cursor.lockState = CursorLockMode.Locked;
        playerModel.SetActive(false);
    }
}