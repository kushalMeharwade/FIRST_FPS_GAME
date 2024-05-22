using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void InitGameOverScreen()
    {
        this.gameObject.SetActive(true);
        GetComponent<Animator>().SetTrigger(TagManager.PLAY_ANIMATION_TRIGGER);
    }
}
