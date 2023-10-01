using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class JailerManager : MonoBehaviourSingleton<JailerManager>
{
    GameManager gm;

    [Header("Initiation state")]
    public float minInitStateTime, maxInitStateTime;
    [Header("Walking state")]
    public float walkTime;
    public AudioSource footStepsSource;
    public AudioClip footstepsClip;
    [Range(0f,1f)]
    public float enteringProbability;
    [Header("Enter and Stay state")]
    public float doorOpenTime;
    public AudioSource doorSource;
    public AudioClip doorLockClip;
    public float stayTime;
    [Header("Interactions")]
    public string DialogWhenCatched;



    private void Awake()
    {
        initiationState = new InitiationState();
        footstepsState = new FootstepsState();
        openingClosingDoorState = new OpeningClosingDoorState();
        stayingInRoomState = new StayingInRoomState();

        setState(initiationState);
    }

    void Start()
    {
        GameManager gm = GameManager.Instance;
        HideJailer();
    }

    JailState currentState;
    InitiationState initiationState;
    FootstepsState footstepsState;
    OpeningClosingDoorState openingClosingDoorState;
    StayingInRoomState stayingInRoomState;

    private void setState(JailState state)
    {
        Debug.Log(state);
        currentState = state;
        currentState.SetupState(this);
    }

    abstract class JailState
    {
        public abstract void UpdateState(JailerManager context);
        public abstract void SetupState(JailerManager context);
    }

    class InitiationState : JailState
    {
        Timer initTimer;
        public override void SetupState(JailerManager context)
        {
            float randomTimeToCome = Random.Range(context.minInitStateTime, context.maxInitStateTime);
            initTimer = new Timer(randomTimeToCome, false);
        }

        public override void UpdateState(JailerManager context)
        {
            initTimer.UpdateTime(Time.deltaTime);
            if(initTimer.TimeOut)
            {
                context.footstepsState.approaching = true;
                context.setState(context.footstepsState);
            }
        }
    }

    class FootstepsState : JailState
    {
        public bool approaching;

        Timer timer;
        bool openTheDoor;

        public override void SetupState(JailerManager context)
        {
            timer = new Timer(context.walkTime);
            openTheDoor = Random.Range(0f, 1f) <= context.enteringProbability;
            if (approaching)
                context.footStepsSource.time = 0f;
            else
                context.footStepsSource.time = context.walkTime;

            context.footStepsSource.Play();
        }

        public override void UpdateState(JailerManager context)
        {

            timer.UpdateTime(Time.deltaTime);
            if (timer.TimeOut)
            {
                if (approaching)
                {
                    if (openTheDoor)
                    {
                        context.footStepsSource.Stop();
                        context.openingClosingDoorState.enterTheRoom = true;
                        context.setState(context.openingClosingDoorState);
                    }
                    else
                    {
                        approaching = false;
                        timer.Reset();
                    }
                }
                else
                {
                    context.setState(context.initiationState);
                }
            }
        }
    }


    class OpeningClosingDoorState : JailState
    {
        Timer timer;
        public bool enterTheRoom;
        public override void SetupState(JailerManager context)
        {
            timer = new Timer(context.doorOpenTime);
            if (enterTheRoom)
                GameManager.Instance.door.Open();
            else
                GameManager.Instance.door.Close();
        }

        public override void UpdateState(JailerManager context)
        {
            timer.UpdateTime(Time.deltaTime);
            if(timer.TimeOut)
            {
                if (enterTheRoom)
                    context.setState(context.stayingInRoomState);
                else
                {
                    context.footstepsState.approaching = false;
                    context.setState(context.footstepsState);
                }
            }
        }
    }

    class StayingInRoomState : JailState
    {
        Timer timer;
        public override void SetupState(JailerManager context)
        {
            context.ShowJailer();
            timer = new Timer(context.stayTime);
        }

        public override void UpdateState(JailerManager context)
        {
            //TODO Check loop here <--------------------------------------------------------------------

            timer.UpdateTime(Time.deltaTime);
            if(timer.TimeOut)
            {
                context.HideJailer();
                context.openingClosingDoorState.enterTheRoom = false;
                context.setState(context.openingClosingDoorState);
            }
        }
    }


    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ShowJailer()
    {
        GameManager.Instance.jailer.gameObject.SetActive(true);
    }

    public void HideJailer()
    {
        GameManager.Instance.jailer.gameObject.SetActive(false);
    }
}
