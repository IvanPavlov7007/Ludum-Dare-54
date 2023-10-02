using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class JailerManager : Singleton<JailerManager>
{
    GameManager gm;

    #region properties


    [System.Serializable]
    public struct JailerBalaning
    {
        [Header("Initiation state")]
        public float minInitStateTime, maxInitStateTime;
        [Header("Walking state")]
        public float walkTime;
        [Range(0f, 1f)]
        public float enteringProbability;
        [Header("Enter and Stay state")]
        public float doorOpenTime;
        public float stayTime;
    }
    public class BalanceInterpolator
    {
        JailerBalaning early; JailerBalaning late;

        float progress;
        public void interpolateBalance( JailerManager context)
        {
            progress = GameManager.Instance.gameProgress;
            context.currentBalancing.minInitStateTime = minInitStateTime();
            context.currentBalancing.maxInitStateTime = maxInitStateTime();
            context.currentBalancing.walkTime = walkTime();
            context.currentBalancing.enteringProbability = enteringProbability();
            context.currentBalancing.doorOpenTime = doorOpenTime();
            context.currentBalancing.stayTime = stayTime();
        }
        public BalanceInterpolator(JailerBalaning early, JailerBalaning late)
        {
            this.early = early; this.late = late;
        }
        float minInitStateTime()
        {
            return Mathf.Lerp(early.minInitStateTime, late.minInitStateTime, progress);
        }

        float maxInitStateTime()
        {
            return Mathf.Lerp(early.maxInitStateTime, late.maxInitStateTime, progress);
        }

        float walkTime()
        {
            return Mathf.Lerp(early.walkTime, late.walkTime, progress);
        }

        float enteringProbability()
        {
            return Mathf.Lerp(early.enteringProbability, late.enteringProbability, progress);
        }

        float doorOpenTime()
        {
            return Mathf.Lerp(early.doorOpenTime, late.doorOpenTime, progress);
        }

        float stayTime()
        {
            return Mathf.Lerp(early.stayTime, late.stayTime, progress);
        }
    }
    public JailerBalaning earlyGame, lateGame;
    public JailerBalaning currentBalancing;

    BalanceInterpolator balanceInterpolator;

    [Header("Audio")]
    public AudioSource footStepsSource;
    public AudioClip footstepsClip;
    public AudioSource doorSource;
    public AudioClip doorLockClip;
    [Header("Interactions")]
    public string DialogWhenCatched;
    #endregion

    private void Awake()
    {
        balanceInterpolator = new BalanceInterpolator(earlyGame, lateGame);
        currentBalancing = earlyGame;

        initiationState = new InitiationState();
        footstepsState = new FootstepsState();
        openingClosingDoorState = new OpeningClosingDoorState();
        stayingInRoomState = new StayingInRoomState();
        punishmentState = new PunishmentState();

        setState(initiationState);
    }

    void Start()
    {
        GameManager gm = GameManager.Instance;
        HideJailer();
    }

    JailState currentState;
    private void setState(JailState state)
    {
        currentState = state;
        currentState.SetupState(this);
    }

    #region states
    InitiationState initiationState;
    FootstepsState footstepsState;
    OpeningClosingDoorState openingClosingDoorState;
    StayingInRoomState stayingInRoomState;
    PunishmentState punishmentState;

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
            float randomTimeToCome = Random.Range(context.currentBalancing.minInitStateTime, context.currentBalancing.maxInitStateTime);
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
            timer = new Timer(context.currentBalancing.walkTime);
            openTheDoor = Random.Range(0f, 1f) <= context.currentBalancing.enteringProbability;
            if (approaching)
                context.footStepsSource.time = 0f;
            else
                context.footStepsSource.time = context.currentBalancing.walkTime;

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
            timer = new Timer(context.currentBalancing.doorOpenTime);
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
            timer = new Timer(context.currentBalancing.stayTime);
        }

        public override void UpdateState(JailerManager context)
        {
            Bed bed = GameManager.Instance.bed;
            Hole hole = GameManager.Instance.hole;

            if( bed.isOpen && hole.health.amount < hole.health.maxAmount)
            {
                context.setState(context.punishmentState);
                return;
            }

            timer.UpdateTime(Time.deltaTime);
            if(timer.TimeOut)
            {
                context.HideJailer();
                context.openingClosingDoorState.enterTheRoom = false;
                context.setState(context.openingClosingDoorState);
            }
        }
    }

    class PunishmentState : JailState
    {

        public override void SetupState(JailerManager context)
        {
            Jailer jailer = GameManager.Instance.jailer;
            jailer.AttackPlayer();
        }

        public override void UpdateState(JailerManager context)
        {
            //put it to the GameManager
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    #endregion

    void Update()
    {
        balanceInterpolator.interpolateBalance(this);
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
