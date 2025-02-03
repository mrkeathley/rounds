using UnityEngine;

[CreateAssetMenu]
public class BooleanValue : ScriptableObject, ISerializationCallbackReceiver {
    
    public bool initialValue;
    
    [HideInInspector]
    public bool runtimeValue;
    
    public void OnBeforeSerialize() {
    }
    
    public void OnAfterDeserialize() {
        runtimeValue = initialValue;
    }
}
