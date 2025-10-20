using UnityEngine;
using extOSC;
public class OSCReceiverTest : MonoBehaviour
{
    [SerializeField] private int port = 9000;
    private OSCReceiver receiver;

    void Start()
    {
        // Création du récepteur OSC
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = port;

        // Exemple de codes RFID à écouter (ajuste selon tes cartes)
        receiver.Bind("/A19EBB5", OnCard_A19EBB5);
        receiver.Bind("/9320076", OnCard_9320076);
        receiver.Bind("/8CEC54E", OnCard_8CEC54E);
        receiver.Bind("/854E4F5", OnCard_854E4F5);
        receiver.Bind("/88324E2", OnCard_88324E2);

        Debug.Log("✅ extOSC prêt à recevoir sur le port " + port);
    }

    void OnCard_A19EBB5(OSCMessage message)
    {
        Debug.Log("Allo 1");
    }

    void OnCard_9320076(OSCMessage message)
    {
        Debug.Log("Allo 2");
    }

    void OnCard_8CEC54E(OSCMessage message)
    {
        Debug.Log("Allo 3");
    }

    void OnCard_854E4F5(OSCMessage message)
    {
        Debug.Log("Allo 4");
    }
    
     void OnCard_88324E2(OSCMessage message)
    {
        Debug.Log("Allo 5");
    }

    
}
