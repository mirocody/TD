using UnityEngine;
using System.Collections;

public class ViewDrag : MonoBehaviour
{
    public float speed;
    bool isDown = false;
    Vector3 offset;
    Vector3 lastPos;
    Vector3 nowPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDown)
        {
            isDown = true;
            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) isDown = false;
        if (isDown)
        {
            nowPos = Input.mousePosition;
            offset = nowPos - lastPos;
            offset *= speed * Time.deltaTime;
            Vector3 camPos = transform.position;
            transform.position = new Vector3(camPos.x - offset.x, camPos.y-offset.y, camPos.z - offset.z);
            lastPos = nowPos;
        }
    }
}