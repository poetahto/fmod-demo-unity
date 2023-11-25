using FMODUnity;
using UnityEngine;

public class DynamicAudioTrigger : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;
    [SerializeField] private float value;
    
    private void OnTriggerEnter(Collider other)
    {
        emitter.SetParameter("Underground", value);
    }
}