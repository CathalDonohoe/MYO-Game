using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;   // grammar recogniser


/*
 *  Uses English US in the settings - Keyboard (on the taskbar), Region, Preferred Language and Speech in Settings
 */

public class MenuGrammar : MonoBehaviour
{
    //Declaration of Variables

    private bool muted;

    //word used for input
    private string phraseWord = "";

    private GrammarRecognizer gr;

    //sets grammar to null when script used
    private void Awake(){
        phraseWord = "";
    }

    //loads the xml to be used in the script
    private void Start()
    {
        
        muted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = muted;

        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, 
                                                "MenuGrammar.xml"), 
                                    ConfidenceLevel.Low);
        Debug.Log("Grammar loaded!");
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        if (gr.IsRunning) Debug.Log("Recogniser running");
    }

    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder message = new StringBuilder();
        Debug.Log("Recognised a phrase");
        // read the semantic meanings from the args passed in.
        SemanticMeaning[] meanings = args.semanticMeanings;
        //takes in the inputed prhase by the user and sets it to the phraseword value
        foreach(SemanticMeaning meaning in meanings)
        {
            string keyString = meaning.key.Trim();
            string valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Value: " + valueString + " ");
            phraseWord = valueString;
        }
        // use a string builder to create the string and out put to the user
        Debug.Log(message);
    }

    //unloads grammar when application closed
    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }

    //this switch compares the inputted values of phraseword and matched them to a rule in the xml file
    private void Update()
    {
        switch (phraseWord)
        {
            case "play":
                phraseWord="";
                Play();
                break;
            
            case "quit":
                phraseWord="";
                Quit();
                break;

            case "mute":
                phraseWord="";
                Mute();
                break;

            case "unmute":
                phraseWord="";
                Mute();
                break;
        }
    }

    //loads the main game
    public void Play()
    {
        MenuManager.OpenInGameSpeech();
    }

    //quits the application
    public void Quit()
    {
        Application.Quit();
    }

    public void Mute()
    {
        ToggleMute();
    }

    public void ToggleMute()
    {
        muted = !muted;

        AudioListener.pause = muted;
        PlayerPrefs.SetInt("MUTED", muted ? 1 : 0);
    }
}
