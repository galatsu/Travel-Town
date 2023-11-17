using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInScript : MonoBehaviour
{
    public string nextScene;
    public Image fadePanel;
    public float transitionTime = 1f;
    

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void Transition()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float t = transitionTime;

        while (t > 0)
        {
            t -= Time.deltaTime;
            fadePanel.color = new Color(0, 0, 0, t / transitionTime);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float t = 0;

        while (t < transitionTime)
        {
            t += Time.deltaTime;
            fadePanel.color = new Color(0, 0, 0, t / transitionTime);
            yield return null;
        }
        gameObject.GetComponent<Button>().onClick.Invoke();

        SceneManager.LoadScene(nextScene);


    }

}
