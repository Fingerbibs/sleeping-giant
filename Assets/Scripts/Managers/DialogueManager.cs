using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public float typingSpeed = 0.03f; // Speed of text appearance
    public TMP_Text dialogueText;
    public GameObject dialogueUI;
    public PlayerMovement playerController;

    private Queue<string> sentences;
    private bool dialogueActive = false;
    private bool isTyping = false;
    private string currentSentence;

    void Start(){
        sentences = new Queue<string>();
        dialogueUI.SetActive(false);
    }

    void Update(){
        if(dialogueActive && Input.GetButtonDown("Submit"))
            if (!isTyping){
                DisplayNextSentence();
            }
            else{
                // Instantly Finishes Typing
                dialogueText.text = currentSentence;
                isTyping = false;
                StopAllCoroutines();
            }
    }

    public void StartDialogue(Dialogue dialogue){
        playerController.EnableMovement(false);

        dialogueUI.SetActive(true);
        dialogueActive = true;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        currentSentence = sentence;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence){
        isTyping = true;

        AudioManager.Instance.PlayGuideVoice();

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        AudioManager.Instance.StopVoice();

        isTyping = false;
    }

    void EndDialogue(){
        dialogueUI.SetActive(false);
        dialogueActive = false;
        playerController.EnableMovement(true);
    }
}