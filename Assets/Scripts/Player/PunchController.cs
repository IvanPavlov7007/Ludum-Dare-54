using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Timer
{
    public float maxTime;
    public float time { get; private set; }
    private Timer() { }
    public Timer(float maxTime, bool initiallyTimeOut = false)
    {
        this.maxTime = maxTime;
        if (initiallyTimeOut)
            time = maxTime;
    }
    public void UpdateTime(float delta)
    {
        time = Mathf.Min(time + Time.deltaTime, maxTime * 2f);
    }

    public void Reset()
    {
        time = 0f;
    }

    public bool TimeOut { get { return time > maxTime; } }
}

public class PunchController : MonoBehaviour
{
    public float kickRadius = 0.5f;
    public float kickLength = 1f;
    public float cooldownTime = 0.5f;
    public LayerMask layerMask;

    Camera cam;
    ArmsController armsController;
    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        armsController = GetComponentInChildren<ArmsController>();
    }

    private void Awake()
    {
        timer = new Timer(cooldownTime, true);
    }

    Timer timer;
    public void OnPunch(InputValue value)
    {
        if (value.isPressed)
        {
            if (timer.TimeOut)
            {
                timer.Reset();
                punch();
            }
        }
    }

    private void Update()
    {
        timer.UpdateTime(Time.deltaTime);
    }

    public void punch()
    {
        Debug.Log("Punch");
        armsController.Punch();

        Vector3 direction = cam.transform.forward;
        Vector3 postition = cam.transform.position;
        RaycastHit hit;
        if(Physics.SphereCast(postition, kickRadius,direction,out hit, kickLength,layerMask))
        {
            Punchable obj = hit.collider.GetComponentInParent<Punchable>();
            if(obj != null)
                obj.Punch();
        }
    }
}
