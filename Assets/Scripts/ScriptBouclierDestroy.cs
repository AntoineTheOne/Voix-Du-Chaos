using UnityEngine;

public class ScriptBouclierDestroy : MonoBehaviour
{
    [SerializeField] private GameObject prefabBrulure;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Vector3 pos = other.transform.position;
            Quaternion rot = other.transform.rotation;
            Destroy(other.gameObject);

            if (prefabBrulure != null)
        {
            Instantiate(prefabBrulure, pos, rot);
        }
        }
    }

}
