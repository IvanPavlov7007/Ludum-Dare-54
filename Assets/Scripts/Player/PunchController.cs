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
    public float impulse = 1000f;
    public float punchRadius = 0.5f;
    public float punchLength = 1f;
    public float cooldownTime = 0.5f;
    public float punchDuration = 0.2f;
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
        cooldownTimer = new Timer(cooldownTime, true);
        punchTimer = new Timer(punchDuration);
    }

    Timer cooldownTimer;
    Timer punchTimer;
    bool punched;
    public void OnPunch(InputValue value)
    {
        if (value.isPressed)
        {
            if (cooldownTimer.TimeOut)
            {
                cooldownTimer.Reset();
                punch();
            }
        }
    }

    private void Update()
    {
        cooldownTimer.UpdateTime(Time.deltaTime);
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
            particles.transform.forward = hit.normal;
            punchSoundSource.transform.position = hit.point;
            punchSoundSource.PlayOneShot(hitClip);
            //particles.transform.localScale *= Mathf.Max(hit.distance / punchLength, 0.2f);
            float strength_multiplier = Mathf.Max((punchLength - hit.distance)/ punchLength, 0.1f);

            Destroy(particles, 1f);
            Punchable obj = hit.collider.GetComponentInParent<Punchable>();
            if(obj != null)
                obj.Punch(hit.point, direction.normalized, impulse * strength_multiplier);
        }
    }
}
