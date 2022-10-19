using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string loadScene;
    public Vector2 playerPosition;
    public VectorValue positionStorage;
    public VectorValue cameraMinPos;
    public VectorValue cameraMaxPos;
    public Vector2 cameraNewMinPos;
    public Vector2 cameraNewMaxPos;

    [Header("Scene Transition Effects")]
    public GameObject fadeInEffect;
    public GameObject fadeOutEffect;
    public float fadeWait;

    private IEnumerator fadeCoroutine; // I wish he'd make the jump to using these so it'd be managed easier


    public void ResetCameraBounds()
    {
        cameraMinPos.runTimeValue = cameraNewMinPos;
        cameraMaxPos.runTimeValue = cameraNewMaxPos;
    }


    public IEnumerator FadeCo()
    {
        if (fadeOutEffect != null)
        {
            Instantiate(fadeOutEffect, Vector3.zero, Quaternion.identity);
        }

        yield return new WaitForSeconds(fadeWait);
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(loadScene);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }


    private void Awake()
    {
        if (fadeInEffect != null)
        {
            GameObject panel = Instantiate(fadeInEffect, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1.0f);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag("Player")) && (!other.isTrigger))
        {
            positionStorage.runTimeValue = playerPosition;
            fadeCoroutine = FadeCo();
            StartCoroutine(fadeCoroutine);
            //SceneManager.LoadScene(loadScene);
        }
    }
}
