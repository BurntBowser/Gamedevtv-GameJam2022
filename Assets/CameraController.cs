using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 30f;
    [SerializeField] float scrollSpeed = 5f;
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 60f;
    [SerializeField] Vector2 minXZ;
    [SerializeField] Vector2 maxXZ;

    // Update is called once per frame
    public void Update()
    {
        
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        ZoomControl();

    }

    void ZoomControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x,minXZ.x,maxXZ.x);
        pos.z = Mathf.Clamp(pos.z,minXZ.y,maxXZ.y);

        transform.position = pos;
    }
}
