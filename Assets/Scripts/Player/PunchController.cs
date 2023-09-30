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
    public float punchRadius = 0.5f;
    public float punchLength = 1f;
    public float cooldownTime = 0.5f;
    public LayerMask layerMask;
    public GameObject punchParticlesPrefab;

    Camera cam;
    ArmsController armsController;
    [SerializeField]
    AudioSource punchSoundSource;

    public AudioClip hitClip;
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
        if(Physics.SphereCast(postition, punchRadius,direction,out hit, punchLength,layerMask))
        {
            var particles = Instantiate(punchParticlesPrefab, hit.point, Quaternion.identity);
            punchSoundSource.transform.position = hit.point;
            punchSoundSource.PlayOneShot(hitClip);
            //particles.transform.localScale *= Mathf.Max(hit.distance / punchLength, 0.2f);
            Destroy(particles, 1f);
            Punchable obj = hit.collider.GetComponentInParent<Punchable>();
            if(obj != null)
                obj.Punch();
        }
    }
}
