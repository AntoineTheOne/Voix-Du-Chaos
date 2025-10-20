using UnityEngine;
using extOSC;

public class OSCReceiverTest : MonoBehaviour
{
    [SerializeField] private int port = 9000;
    [SerializeField] private Transform spawner1;
    [SerializeField] private Transform spawner2;
    [SerializeField] private Transform spawner3;
    [SerializeField] private Transform spawner4;
    [SerializeField] private GameObject eau;
    [SerializeField] private GameObject feu;
    [SerializeField] private GameObject vent;
    [SerializeField] private GameObject terre;

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

    

    private bool CanTrigger(string cardCode)
    {

        if (lastCard == cardCode) return false;

        lastCard = cardCode;
        return true;
    }

    void OnCard_A19EBB5(OSCMessage message)
    {
        if (!CanTrigger("A19EBB5")) return;
        Debug.Log("A19EBB5 - Carte Reset");
    }

    void OnCard_9320076(OSCMessage message)
    {
        if (!CanTrigger("9320076")) return;
        Instantiate(eau, spawner1.position, spawner1.rotation);
        Debug.Log("9320076");
    }

    void OnCard_8CEC54E(OSCMessage message)
    {
        if (!CanTrigger("8CEC54E")) return;
        Instantiate(feu, spawner2.position, spawner2.rotation);
        Debug.Log("8CEC54E");
    }

    void OnCard_854E4F5(OSCMessage message)
    {
        if (!CanTrigger("854E4F5")) return;
        Instantiate(terre, spawner3.position, spawner3.rotation);
        Debug.Log("854E4F5");
    }

    void OnCard_88324E2(OSCMessage message)
    {
        if (!CanTrigger("88324E2")) return;
        Instantiate(vent, spawner4.position, spawner4.rotation);
        Debug.Log("88324E2");
    }
}
