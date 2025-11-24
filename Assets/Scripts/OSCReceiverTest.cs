using UnityEngine;
using extOSC;

public class OSCReceiverTest : MonoBehaviour
{
    [SerializeField] private int port = 9000;
    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject PrefabAssassin;
    [SerializeField] private GameObject PrefabGoblin; 
    [SerializeField] private GameObject PrefabGolem;
    [SerializeField] private GameObject PrefabMiniGolem;
    [SerializeField] private float cooldown = 2f;
    private float timer = 0f;
    private OSCReceiver receiver;


    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = port;


        // Code de chaque carte RFID

        receiver.Bind("/A19EBB5", Assassin);
        receiver.Bind("/9320076", Goblin);
        receiver.Bind("/8CEC54E", Golem);
        receiver.Bind("/854E4F5", MiniGolem);
        


    }


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }



    void Assassin(OSCMessage message)
    {      
      if(timer <= 0)
        {
            Instantiate(PrefabAssassin, spawner.position, spawner.rotation);
            Debug.Log("A19EBB5");
            timer = cooldown;
        }
        
    }

    void Goblin(OSCMessage message)
    {
       if(timer <= 0)
        {
           Instantiate(PrefabGoblin, spawner.position, spawner.rotation);
            Debug.Log("9320076"); 
            timer = cooldown;
        }
        
    }

    void Golem(OSCMessage message)
    {
        if(timer <= 0)
        {
            Instantiate(PrefabGolem, spawner.position, spawner.rotation);
            Debug.Log("8CEC54E");
            timer = cooldown;
        }
        
    }

    void MiniGolem(OSCMessage message)
    {
        if(timer <= 0)
        {
            Instantiate(PrefabMiniGolem, spawner.position, spawner.rotation);
            Debug.Log("854E4F5");
            timer = cooldown;
        }
        
    }

    
}
