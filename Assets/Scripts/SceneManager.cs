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

    [SerializeField] private string sceneName;


    void Start()
    {
        Debug.Log(PhraseRecognitionSystem.isSupported);
        actions.Add("ouverture du portail", SceneDebut);
        actions.Add("ouverture", SceneDebut);
        actions.Add("portail", SceneDebut);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

         // Get the VideoPlayer component attached to this GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        // Subscribe to the loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;

    }

    // This method is called when the video finishes playing once
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished playing.");

        // Stop the video playback and clear resources
        vp.Stop();
        
        // Option B: Disable the entire GameObject (which also stops the video player)
        this.gameObject.SetActive(false);
        
        // Option C: Transition to the next action or load a new scene
        // For example: SceneManager.LoadScene("MainGameScene");
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void SceneDebut()
    {
        Debug.Log("csafv");
        SceneManager.LoadScene(sceneName);
    }
}