using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Scene currentScene;
    private float CurrentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {

        currentScene = SceneManager.GetActiveScene();
        CurrentSceneIndex = currentScene.buildIndex;
        
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(currentScene.buildIndex+1);
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadTitle(){

        SceneManager.LoadScene("Splash");
    }

    public void playGameAgain(){
        SceneManager.LoadScene("Level 1");

    }
 
}
