using UnityEngine;
using extOSC;

public class OSCReceiverTest : MonoBehaviour
{
    [SerializeField] private int port = 9000;
    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject PrefabAssassin;
    [SerializeField] private GameObject PrefabGoblin; 
    [SerializeField] private GameObject PrefabGolem;
    [SerializeField] private GameObject PortailEffet;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float maxMonsterAlive = 15;
    [SerializeField] private MonsterManager monsterManager; // Monster manager
    private float timer = 0f;
    private OSCReceiver receiver;


    void Start()
    {
        monsterManager.nbMonstreApparu = 0;
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = port;


        // Code de chaque carte RFID

        receiver.Bind("/A19EBB5", Assassin);
        receiver.Bind("/9320076", Goblin);
        receiver.Bind("/8CEC54E", Golem);
     
        


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
            if(monsterManager.nbMonstreApparu < maxMonsterAlive)
            {
                Instantiate(PrefabAssassin, spawner.position, spawner.rotation);
                Debug.Log("A19EBB5");
                timer = cooldown;
                monsterManager.nbMonstreApparu += 1;
                Debug.Log(monsterManager.nbMonstreApparu);
                Instantiate(PortailEffet, spawner.position, spawner.rotation);

            }
        }
    }

    void Goblin(OSCMessage message)
    {
       if(timer <= 0)
        {
            if(monsterManager.nbMonstreApparu < maxMonsterAlive)
            {
                Instantiate(PrefabGoblin, spawner.position, spawner.rotation);
                Debug.Log("9320076"); 
                timer = cooldown;
                monsterManager.nbMonstreApparu += 1;
                Debug.Log(monsterManager.nbMonstreApparu);
                Instantiate(PortailEffet, spawner.position, spawner.rotation);
            }
        } 
    }

    void Golem(OSCMessage message)
    {
        if(timer <= 0)
        {
            if(monsterManager.nbMonstreApparu < maxMonsterAlive)
            {
                Instantiate(PrefabGolem, spawner.position, spawner.rotation);
                Debug.Log("8CEC54E");
                timer = cooldown;
                monsterManager.nbMonstreApparu += 1;
                Debug.Log(monsterManager.nbMonstreApparu);
                Instantiate(PortailEffet, spawner.position, spawner.rotation);
            }
        }
        
    }

    
}
