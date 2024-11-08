using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 0.5f;
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToCheatCodes();
    }
    void RespondToCheatCodes()
    {
        CheatCodeNextLevel();
        CheatCodeNoCollisions();
    }
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisable)
        {
            return;
        }
        switch(other.gameObject.tag)
        {
            case "Finish":
            {
                //Debug.Log("You have finished the game" + successClip.name + "");
                StartCoroutine(nextLevel());                   
                break;
            }
            case "Friendly":
            {
                //Debug.Log("You have collided with a friendly");
                break;
            }
            default:
            {
                // could have Invoke instead of 
                // Coroutine okay for doing one thing
                StartCoroutine(startCrashSequence());
                break;
            }
        }     
    }

    void ReloadLevel()
    {
        // gets the current scene 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        // gets the current scene 
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int totalScenes = SceneManager.sceneCount;
        if(nextSceneIndex == 1 + totalScenes)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    IEnumerator nextLevel()
    {
        GetComponent<Movement>().enabled = false;
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        successParticles.Play();
        yield return new WaitForSeconds(delayTime);
        LoadNextLevel();
        yield return null;
    }
    IEnumerator startCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        deathParticles.Play();
        GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(delayTime);
        // gets the current scene 
        ReloadLevel();
        yield return null;
    }
    void CheatCodeNextLevel()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            LoadNextLevel();
        }
    }
    void CheatCodeNoCollisions()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
}
