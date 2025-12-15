using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using extOSC;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
public class ChangementScene : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [SerializeField] private bool dictionaryCommandCreated;
    [SerializeField] public bool endgame;
    [SerializeField] private bool endgameTriggered = false;
    [SerializeField] public bool victory;
    [SerializeField] public bool defeat;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject opening;
    [SerializeField] private GameObject canvaVictoire;
    [SerializeField] private GameObject canvaDefeat;
    [SerializeField] private GameObject canvaEnding;
    [SerializeField] private string sceneJeu;
    [SerializeField] private string sceneDepart;


    void Start()
    {
        if (dictionaryCommandCreated != false)
        {
            Debug.Log(PhraseRecognitionSystem.isSupported);
        actions.Add("ouverture du portail", SceneDebut);
        actions.Add("ouverture", SceneDebut);
        actions.Add("portail", SceneDebut);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        }
        

        if (opening != null)
        {
            videoPlayer = opening.GetComponent<VideoPlayer>();
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        
        


        
    }

    void Update()
    {
        if(endgame && !endgameTriggered)
        {
            endgameTriggered = true;
            if (victory ==true)
            {
                canvaVictoire.SetActive(true);
            }
            if (defeat ==true)
            {
                canvaDefeat.SetActive(true);
            }
            
            Invoke("ShowEndingCanvas", 8f);
            Invoke("NewGame", 10);
        }
    }

    void ShowEndingCanvas()
    {
        canvaEnding.SetActive(true);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        vp.Stop();
        if(opening != null)
        {
            opening.SetActive(false);
        }
        
        if(healthBar != null)
        {
            healthBar.SetActive(true);
        }
        
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void SceneDebut()
    {
        Debug.Log("Début");
        SceneManager.LoadScene(sceneJeu);
    }

    public void NewGame()
    {
        Debug.Log("Début");
        SceneManager.LoadScene(sceneDepart);
    }

    
}