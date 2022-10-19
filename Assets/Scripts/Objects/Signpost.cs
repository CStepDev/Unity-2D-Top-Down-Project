using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signpost : Interactable
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;

    public override void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            contextClueOn.Raise();
            isActive = false;
            dialogueBox.SetActive(false);
        }
    }


    // Update is called once per frame
    public virtual void Update ()
    {
		if ((Input.GetButtonDown("Interact")) && (isActive))
        {
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);
            }
            else
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue;
            }
        }
	}
}
