using UnityEngine;
using System.Collections;

public class ViewDrag : MonoBehaviour
{
    public float speed;
    bool isDown = false;
    Vector3 offset;
    Vector3 lastPos;
    Vector3 nowPos;
    Vector3 worldLowerLeft;
    Vector3 worldUpperRight;

    void Start()
    {
        speed = 1.0f;
        GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 30f);
        worldLowerLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        worldUpperRight = Camera.main.ViewportToWorldPoint(Vector3.one);
        isDown = false;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonDown(0) && !isDown)
            {
                isDown = true;
                lastPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0)) isDown = false;
            if (isDown)
            {
                Vector3 minCorner = Camera.main.ViewportToWorldPoint(Vector3.zero);
                Vector3 maxCorner = Camera.main.ViewportToWorldPoint(Vector3.one);
                Vector3 boundingBoxSize = (maxCorner - minCorner);
                float limXLow = worldLowerLeft.x + boundingBoxSize.x / 2;
                float limXHigh = worldUpperRight.x - boundingBoxSize.x / 2;
                float limYLow = worldLowerLeft.z + boundingBoxSize.z / 2;
                float limYHigh = worldUpperRight.z - boundingBoxSize.z / 2;
                nowPos = Input.mousePosition;
                offset = nowPos - lastPos;
                offset *= speed * Time.deltaTime;
                Vector3 camPos = transform.position;
                float x = Mathf.Clamp(camPos.x - offset.x, limXLow, limXHigh);
                float z = Mathf.Clamp(camPos.z - offset.y, limYLow, limYHigh);
                transform.position = new Vector3(x, camPos.y, z);
                lastPos = nowPos;
            }
        }
    }
}