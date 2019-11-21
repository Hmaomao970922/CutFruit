using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public AudioSource audioc;
    [SerializeField]
    private LineRenderer lineRenderer;

    private bool firstMouseDown = false;
    private bool mouseDown = false;
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMouseDown = true;
            mouseDown = true;

            audioc.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        onDrawLine();
        firstMouseDown = false;
    }

    private Vector3[] positions = new Vector3[10];//保存的坐标
    private int posCount = 0;//当前保存的坐标数
    private Vector3 head;//这一帧的鼠标位置(头)
    private Vector3 last;//上一帧的鼠标位置
    //画线
    private void onDrawLine()
    {
        if (firstMouseDown)
        {
            posCount = 0;
            head = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            last = head;
        }
        if (mouseDown)
        {
            head = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            if (Vector3.Distance (head, last) > 0.01f)
            {
                //距离比较远了,存数组
                savePosition(head);

                posCount++;
                
                OnRayCast(head);
            }
            last = head;
        }
        else
        {
            positions = new Vector3[10];
        }
        ChangePositions(positions);
    }
    private void savePosition(Vector3 pos)
    {
        pos.z = 0;
        if (posCount <= 9)
        {
            for (int i = posCount; i < 10; i++)
            {
                positions[i] = pos;
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                positions[i] = positions[i + 1];
            }
            positions[9] = pos;

        }
    }
    //射线
    private void OnRayCast(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        //检测物体
        RaycastHit[] hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].collider.gameObject.name);
            hits[i].collider.gameObject.SendMessage("OnCut", SendMessageOptions.DontRequireReceiver);
        }

    }
    private void ChangePositions(Vector3[] positions)
    {
        lineRenderer.SetPositions(positions);
    }

}
