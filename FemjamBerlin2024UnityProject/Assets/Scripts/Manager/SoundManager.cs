using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public AudioClip bossTheme;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        DontDestroyOnLoad(this);
        gameObject.GetComponent<AudioSource>().resource= bossTheme;
        gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
