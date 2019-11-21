using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//产生水果和zd
public class Spawner : MonoBehaviour
{
    public GameObject[] Fruits;//水果预设
    public GameObject Bomb;//炸弹预设
    public AudioSource audioSource;
    float spwanTime = 3f;

    bool isPlaying = true;
    void Update()
    {
        if (!isPlaying)
        {
            return;
        }
        spwanTime -= Time.deltaTime;
        if(spwanTime < 0)
        {
            int fruitCount = Random.Range(1, 5);
            for (int i = 0; i < fruitCount; i++)
            {
                //产生水果
                onSpawn(true);
            }
            int bombNum = Random.Range(0, 100);
            if (bombNum > 70)
            {
                onSpawn(false);
            }
            spwanTime = 3f;
        }    
    }
    private float tmpZ = 0f;
    private void onSpawn(bool isFruit)
    {
        //音乐
        this.audioSource.Play();
        //x : -8.4,8.4
        //y : trans.po.y
        float x = Random.Range(-8.4f, 8.4f);
        float y = transform.position.y;
        float z = tmpZ;
        
        tmpZ -= 2f;
        if (tmpZ <= -10)//水果不在同一z轴，避免碰撞
        {
            tmpZ = 0f;
        }

        //实例化
        int fruitIndex = Random.Range(0, Fruits.Length);
        GameObject go; 
        if (isFruit)
        {
            go = Instantiate<GameObject>(Fruits[fruitIndex],new Vector3(x, y, z),Random.rotation) as GameObject;
        }
        else
        {
            go = Instantiate<GameObject>(Bomb, new Vector3(x, y, z), Random.rotation) as GameObject;
        }

        //水果速度
        Vector3 velocity = new Vector3(-x * Random.Range(0.2f, 0.8f), -Physics.gravity.y * Random.Range(1.0f, 1.5f), 0);
        
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision other)//物体碰撞
    {
        Destroy(other.gameObject);
    }
}
