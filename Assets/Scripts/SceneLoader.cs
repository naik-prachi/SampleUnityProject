using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void level_one() {
        SceneManager.LoadScene("Level1");
        Debug.Log("Level1");
    }

    public void level_two() {
        SceneManager.LoadScene("Level2");
        Debug.Log("Level2");
    }

    public void level_three() {
        SceneManager.LoadScene("Level3");
        Debug.Log("Level3");
    }

    public void level_four() {
        SceneManager.LoadScene("Level4");
        Debug.Log("Level4");
    }

    public void level_five() {
        SceneManager.LoadScene("Level5");
        Debug.Log("Level5");
    }

    public void game_over() {
        SceneManagement.LoadScene("gameover");
        Debug.Log("Game Over");
    }
}
