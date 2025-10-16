using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpellSpawner : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [SerializeField] private GameObject bouleDeFeu;
    [SerializeField] private GameObject bouleDEau;
    [SerializeField] private GameObject bouleDeTerre;
    
    [SerializeField] private float projectileSpawnDistance = 2f;
    [SerializeField] private float wallSpawnDistance = 5f;
    [SerializeField] private float projectileSpeed = 10f;


    void Start()
    {
        actions.Add("boule de feu", Feu);
        actions.Add("boule d'eau", Eau);
        actions.Add("boule de terre", Terre);



        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }


    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }


    private void Feu()
    {
        Vector3 spawnPosition = transform.position + transform.forward * projectileSpawnDistance;
        GameObject projectile = Instantiate(bouleDeFeu, spawnPosition, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * projectileSpeed;
        }
    }

    private void Eau()
    {
        Vector3 spawnPosition = transform.position + transform.forward * projectileSpawnDistance;
        GameObject projectile = Instantiate(bouleDEau, spawnPosition, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * projectileSpeed;
        }
    }

    private void Terre()
    {
        Vector3 spawnPosition = transform.position + transform.forward * projectileSpawnDistance;
        GameObject projectile = Instantiate(bouleDeTerre, spawnPosition, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * projectileSpeed;
        }
    }
    
    
}
