using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using extOSC;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpellSpawner : MonoBehaviour
{
    public OSCTransmitter transmitter;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [SerializeField] private GameObject bouleDeFeu;
    [SerializeField] private GameObject tornado;
    [SerializeField] private GameObject piqueTerre;
    [SerializeField] private GameObject bouclier;
    [SerializeField] private GameObject laser;
    [SerializeField] private float lifeTime = 5f;
    
    [SerializeField] private float projectileSpawnDistance = 2f;
    [SerializeField] private float wallSpawnDistance = 5f;
    [SerializeField] private float projectileSpeed = 10f;

    [SerializeField] private float index = 10f;
    private float timer = 0f;

    void Start()
    {
        Debug.Log(PhraseRecognitionSystem.isSupported);
        actions.Add("boule de feu", BouleFeu);
        actions.Add("tornado", Tornado);
        actions.Add("pique de terre", PiqueTerre);
        actions.Add("laser électrique", Laser);
        actions.Add("laser", Laser);
        actions.Add("bouclier", Bouclier);



        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    void Update()
    {
        OSCMessage message = new OSCMessage("/unity/value");
        message.AddValue(OSCValue.Float(index)); // envoie un float
 
        Debug.Log(message);
        transmitter.Send(message);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            index = 10f;
        }
        Debug.Log(timer);
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }


    private void BouleFeu()
    {
        Vector3 spawnPosition = transform.position + transform.forward * projectileSpawnDistance;
        GameObject projectile = Instantiate(bouleDeFeu, spawnPosition, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * projectileSpeed;
        }
        Destroy(projectile, lifeTime);
        index = 0.5f;
        timer = 1.5f;
    }

    private void Tornado()
    {
        Vector3 spawnPosition = transform.position + transform.forward * wallSpawnDistance;
        Quaternion wallRotation = Quaternion.LookRotation(transform.forward);
        GameObject projectile = Instantiate(tornado, spawnPosition, wallRotation);
        Destroy(projectile, lifeTime);
        index = 1.5f;
        timer = 1.5f;
    }

    private void PiqueTerre()
    {
       Vector3 spawnPosition = transform.position + transform.forward * wallSpawnDistance;
        Quaternion wallRotation = Quaternion.LookRotation(transform.forward);
        GameObject projectile = Instantiate(piqueTerre, spawnPosition, wallRotation);
        Destroy(projectile, lifeTime);
        index = 2.5f;
        timer = 1.5f;
    }
    private void Laser()
    {
       Vector3 spawnPosition = transform.position + transform.forward * wallSpawnDistance;
        Quaternion wallRotation = Quaternion.LookRotation(transform.forward);
        GameObject projectile = Instantiate(laser, spawnPosition, wallRotation);
        Destroy(projectile, lifeTime);
        index = 3.5f;
        timer = 1.5f;
    }
    private void Bouclier()
    {
        Vector3 spawnPosition = transform.position + transform.forward * wallSpawnDistance;
        Quaternion wallRotation = Quaternion.LookRotation(transform.forward);
        GameObject projectile = Instantiate(bouclier, spawnPosition, wallRotation);
        Destroy(projectile, lifeTime);
        index = 4.5f;
        timer = 1.5f;
    }
    
    
}
