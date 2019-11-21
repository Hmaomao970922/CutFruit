using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public GameObject halfFruit;//一半水果
    public GameObject splash;
    public GameObject splashFlat;
    public GameObject firework;
    public AudioClip ac;
    private bool isDead = false;
    //被切了
    public void OnCut()
    {
        if (isDead)
        {
            return;
        }

        if (gameObject.name.Contains("Bomb"))
        {
            Instantiate(firework, transform.position, Quaternion.identity);

            UIScore._instance.Remove(20);
        }
        else
        {
            //生成一半的
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate<GameObject>(halfFruit, transform.position, Random.rotation);
                go.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 5f, ForceMode.Impulse);
            }

            //特效
            Instantiate(splash, transform.position, Quaternion.identity);
            Instantiate(splashFlat, transform.position, Quaternion.identity);

            UIScore._instance.Add(10);
        }
        AudioSource.PlayClipAtPoint(ac, transform.position);

        Destroy(gameObject);//销毁

        isDead = true;
    }
}
