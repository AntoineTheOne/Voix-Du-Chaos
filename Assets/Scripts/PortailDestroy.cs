using UnityEngine;

public class PortailDestroy : MonoBehaviour
{
    [SerializeField] private float timer = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timer);
    }

    
}
