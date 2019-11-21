using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIScore : MonoBehaviour
{
    //单例
    public static UIScore _instance = null;

    public Text txtscore;
    //当前分
    private int score = 0;
    //加分
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _instance = this;
    }
    public void Add(int score)
    {
        this.score += score;
        txtscore.text = this.score.ToString();
    }
    //扣分
    public void Remove(int score)
    {
        this.score -= score;
        txtscore.text = this.score.ToString();

        //gameover
        if (this.score <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            return;
        }
    }
}
