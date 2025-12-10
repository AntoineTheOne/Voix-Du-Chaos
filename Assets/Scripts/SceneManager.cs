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

    [SerializeField] public bool endgame;
    [SerializeField] public bool victory;
    [SerializeField] public bool defeat;
    [SerializeField] private GameObject canvaVictoire;
    [SerializeField] private GameObject canvaDefeat;
    [SerializeField] private GameObject canvaEnding;
    [SerializeField] private string sceneJeu;
    [SerializeField] private string sceneDepart;


    void Start()
    {
        Debug.Log(PhraseRecognitionSystem.isSupported);
        actions.Add("ouverture du portail", SceneDebut);
        actions.Add("ouverture", SceneDebut);
        actions.Add("portail", SceneDebut);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;


        
    }

    void Update()
    {
        if(endgame == true)
        {
            if (victory ==true)
            {
                canvaVictoire.SetActive(true);
            }
            if (defeat ==true)
            {
                canvaDefeat.SetActive(true);
            }
            canvaEnding.SetActive(true);
            Debug.Log("Fin");
            Invoke("NewGame", 5);
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished playing.");

        vp.Stop();
        
        this.gameObject.SetActive(false);
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