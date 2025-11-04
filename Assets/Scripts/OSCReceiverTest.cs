using UnityEngine;
using extOSC;

public class OSCReceiverTest : MonoBehaviour
{
    [SerializeField] private int port = 9000;
    [SerializeField] private Transform spawner;
   
    [SerializeField] private GameObject eau;
    [SerializeField] private GameObject feu;
    [SerializeField] private GameObject vent;
    [SerializeField] private GameObject terre;
    [SerializeField] private float cooldown = 2f;

    private float timer = 0f;

    private OSCReceiver receiver;


    private string lastCard = "";

    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = port;


        // Code de chaque carte RFID

        receiver.Bind("/A19EBB5", OnCard_A19EBB5);
        receiver.Bind("/9320076", OnCard_9320076);
        receiver.Bind("/8CEC54E", OnCard_8CEC54E);
        receiver.Bind("/854E4F5", OnCard_854E4F5);
        receiver.Bind("/88324E2", OnCard_88324E2);


    }


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }



    void OnCard_A19EBB5(OSCMessage message)
    {      
      if(timer <= 0)
        {
            Instantiate(terre, spawner.position, spawner.rotation);
            Debug.Log("A19EBB5");
            timer = cooldown;
        }
        
    }

    void OnCard_9320076(OSCMessage message)
    {
       if(timer <= 0)
        {
           Instantiate(eau, spawner.position, spawner.rotation);
            Debug.Log("9320076"); 
            timer = cooldown;
        }
        
    }

    void OnCard_8CEC54E(OSCMessage message)
    {
        if(timer <= 0)
        {
            Instantiate(feu, spawner.position, spawner.rotation);
            Debug.Log("8CEC54E");
            timer = cooldown;
        }
        
    }

    void OnCard_854E4F5(OSCMessage message)
    {
        if(timer <= 0)
        {
            Instantiate(terre, spawner.position, spawner.rotation);
            Debug.Log("854E4F5");
            timer = cooldown;
        }
        
    }

    void OnCard_88324E2(OSCMessage message)
    {
        if(timer <= 0)
        {
            Instantiate(vent, spawner.position, spawner.rotation);
            Debug.Log("88324E2");
            timer = cooldown;
        }
        
    }
}
