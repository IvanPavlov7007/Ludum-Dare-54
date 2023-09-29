using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Pixelplacement;

public class DialogUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dislpayedText, yesText, noText;
    [SerializeField]
    GameObject dialogElements;

    PlayerMovement playerMovement;
    [SerializeField]
    float maxDialogDistance = 2f;

    AudioSource audioSource;

    [SerializeField]
    float timeNoSkip = 0.5f;

    [SerializeField]
    AudioClip clickSound;

    Dialog currentDialog;

    public static DialogUI instance;

    StateMachine layouts;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        layouts = GetComponentInChildren<StateMachine>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        playerMovement = PlayerMovement.Instance;
    }


    Vector3 playerInitPos;
    public void Activate(Dialog d)
    {
        if(d.disableMovement)
        {
            playerMovement.enabled = false;
            playerMovement.GetComponent<CharacterMovement>().direction = Vector3.zero;
        }

        playerInitPos = playerMovement.transform.position;
        currentDialog = d;
        //GameManager.instance.StopGame();
        skipFrame = true;
        //MusicManager.instance.DampenMusic();
        ShowDialog();
        next();
    }

    bool skipFrame = false;
    float elapsedTime = 0f;
    void Update()
    {
        if (Vector3.Distance(playerInitPos, playerMovement.transform.position) > maxDialogDistance)
            BreakDialog();


        if (!skipFrame && elapsedTime > timeNoSkip && currentDialog != null && Input.GetKeyDown(KeyCode.E))
        {
            next();
        }

        skipFrame = false;
        elapsedTime += Time.unscaledDeltaTime;
    }

    public void next()
    {
        var result = currentDialog.next();
        if(result.resultType == DialogResult.ResultType.NextLine)
        {
            displayPhrase(result.phrase);
        }

        if(result.resultType == DialogResult.ResultType.End)
        {
            dislpayedText.text = string.Empty;
            //GameManager.instance.ContinueGame();
            currentDialog = null;
            //MusicManager.instance.RestoreNormalVolime();

            CloseDialog();
        }

        audioSource.PlayOneShot(clickSound);
        //for not skipping frame unintentially
        elapsedTime = 0f;
    }

    public void displayPhrase(Phrase phrase)
    {
        GameObject g_layout;
        DialogLayout layout;
        if(phrase.icon != null)
        {
            if(phrase.fromTheRigt)
            {
                g_layout = layouts.ChangeState("NameTextImageRight");
            }
            else
                g_layout = layouts.ChangeState("NameTextImageLeft");
        }
        else if(phrase.name != string.Empty)
        {
            g_layout = layouts.ChangeState("NameText");
        }
        else
            g_layout = layouts.ChangeState("Text");

        if (g_layout == null)
            g_layout = layouts.currentState;

        layout = g_layout.GetComponent<DialogLayout>();

        layout.setIcon(phrase.icon);
        layout.setName(phrase.name,phrase.fromTheRigt);
        layout.setText(phrase.line, phrase.fromTheRigt);

    }


    // TODO: if game should be paused
    public void BreakDialog()
    {
        dislpayedText.text = string.Empty;
        //GameManager.instance.ContinueGame();
        currentDialog = null;
        //MusicManager.instance.RestoreNormalVolime();

        CloseDialog();
    }

    public void ShowDialog()
    {
        dialogElements.SetActive(true);
    }

    public void CloseDialog()
    {
        playerMovement.enabled = true;
        dialogElements.SetActive(false);
    }
}
