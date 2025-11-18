using extOSC;
using UnityEngine;
 
public class SendOSC : MonoBehaviour
{
    public OSCTransmitter transmitter;
    public SpellSpawner spell;
    void Update()
    {
        OSCMessage message = new OSCMessage("/unity/value");
        message.AddValue(OSCValue.Float(Time.time)); // envoie un float
 
        Debug.Log(message);
        transmitter.Send(message);
    }
}
 