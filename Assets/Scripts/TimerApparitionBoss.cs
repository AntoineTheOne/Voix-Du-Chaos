using UnityEngine;

public class TimerApparitionBoss : MonoBehaviour
{
    [SerializeField] private float timerBoss = 300f;
    [SerializeField] private GameObject prefabBoss;
    [SerializeField] private GameObject spawnerBoss;
    [SerializeField] private SkyboxFade skyboxFadeScript;
    private bool bossSpawned = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (bossSpawned) return;
        if (timerBoss > 0)
        {
            timerBoss -= Time.deltaTime;
        }
        if(timerBoss <= 0 && bossSpawned == false)
        {
            Instantiate(prefabBoss, spawnerBoss.transform.position, spawnerBoss.transform.rotation);
            bossSpawned = true;

            if(skyboxFadeScript != null){
                skyboxFadeScript.bossPhase = true;
            }
            
        }
    }


}
