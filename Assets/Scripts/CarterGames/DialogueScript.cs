using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Styles
{
    Default,
    TypeWriter,
    Fade,
};

public class DialogueScript : MonoBehaviour
{
    // The Active Text File - This is used to populate the list when updated
    [Header("Current Dialouge File")]
    [Tooltip("This is the current dialouge text file selected by the script, if this isn't the file you called then something has gone wrong.")]
    //public TextAsset InputText;
    public DialogueFile File;

	// References to the displayed name and text area
	[Header("UI Element For Story Character Name")]
	[Tooltip("The UI Text element that is going to be used in your project to hold the Story Characters Name when they are talking.")]
	public Text NameTxt;

	[Header("UI Element that holds the character dialogue")]
	[Tooltip("The UI Text element that is going to hold the lines of dialouge for you story charcters.")]
	public Text DialTxt;

	// Int to check which element in the Dialogue list is next to be displayed
	public int DialStage = 0;

	// Checks is a courutine is running or not
	public bool IsCoRunning;

    public Styles DisplayStyle;

    public bool InputPressed;
    public bool RequireInput = true;
    public bool FileHasEnded = false;

    public float TypeWriterDelay;

    public Animation FadeInAnim;
    public Animation FadeOutAnim;

	[Header("Type Writer Settings")]
	public int TypeWriterCount = 1;

    public AudioManager AM;

    private void Update()
    {
        if ((RequireInput) && (InputPressed))
        {
            switch (DisplayStyle)
            {
                case Styles.Default:

                    DisplayNextLine();

                    break;
                case Styles.TypeWriter:

                    if (!IsCoRunning)
                    {
                        StartCoroutine(TypeWriter(TypeWriterDelay));
                    }

                    break;
                case Styles.Fade:

                    if (!IsCoRunning)
                    {
                        StartCoroutine(FadeTransition());
                    }

                    break;
                default:
                    break;
            }
        }

        if (DialStage == File.Names.Count)
        {
            FileHasEnded = true;
        }
    }


    /// <summary>
    /// Call this to change the active Dialogue File
    /// </summary>
    /// <param name="Input">The Dialogue File you want to input into the system</param>
    public void ChangeFile(DialogueFile Input)
    {
        File = Input;
        Reset();
        ResetDisplayText();
    }


    /// <summary>
    /// Displays the next line of dialogue from the inputted file to be displayed.
    /// </summary>
    private void DisplayNextLine()
    {
        if (DialStage < File.Names.Count)
        {
            NameTxt.text = File.Names[DialStage];
            DialTxt.text = File.Dialogue[DialStage];
            DialStage++;
            InputPressed = false;
        }
        else
        {
            NameTxt.text = "";
            DialTxt.text = "";
            FileHasEnded = true;
        }
    }

    /// <summary>
    /// Shows the next line of dialogue using a typewriter style display, filling out the line one letter at a time at the speed of the inputted delay
    /// </summary>
    /// <param name="Delay">The time between each letter been shown.</param>
    /// <returns></returns>
    private IEnumerator TypeWriter(float Delay)
    {
        IsCoRunning = true;

        string Sentence = "";

        if (DialStage < File.Names.Count)
        {
            if (File.Dialogue[DialStage] != null)
            {
                Sentence = File.Dialogue[DialStage].ToString().Substring(0, TypeWriterCount);
            }

            if (Sentence.Length == File.Dialogue[DialStage].Length)
            {
                Sentence = File.Dialogue[DialStage].ToString();
                DialTxt.text = Sentence;
                InputPressed = false;
                DialStage++;
                TypeWriterCount = 0;
            }
            else
            {
                NameTxt.text = File.Names[DialStage];
                DialTxt.text = Sentence;
                TypeWriterCount++;
            }
        }
        else
        {
            NameTxt.text = "";
            DialTxt.text = "";
            FileHasEnded = true;
        }

        AM.PlayFromTime("Dial", 1.8f, .5f, Random.Range(.75f, 1.1f));
        yield return new WaitForSeconds(Delay);
        
        IsCoRunning = false;
    }

    /// <summary>
    /// Shows the next line of dialouge ith a fade animation using the inutted animation files so it can be customised by the user
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeTransition()
    {
        IsCoRunning = true;
        FadeOutAnim.Play();
        yield return new WaitForSeconds(FadeOutAnim.clip.length);
        NameTxt.text = File.Names[DialStage];
        DialTxt.text = File.Dialogue[DialStage];
        FadeInAnim.Play();
        yield return new WaitForSeconds(FadeInAnim.clip.length);
        IsCoRunning = false;
    }

    /// <summary>
    /// Call this to register that an input has been pressed that updates the dialogue
    /// </summary>
    public void Input()
    {
        if (!InputPressed) { InputPressed = true; }
    }

    /// <summary>
    /// Call this to reset the System for a new file, this is called by default when a new file is inputted into the system
    /// </summary>
    public void Reset()
    {
        if (FileHasEnded) { FileHasEnded = false; }
        DialStage = 0;
    }


    public void ResetDisplayText()
    {
        NameTxt.text = "";
        DialTxt.text = "";
    }
}
