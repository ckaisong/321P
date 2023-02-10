using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    //Gyroscope m_gyro;

    float lastPos;
    // Start is called before the first frame update
    void Start()
    {
        /*m_gyro = Input.gyro;
        m_gyro.enabled = true;*/
    }
    private void OnGUI()
    {
        /*if (m_gyro != null)
        {
            GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + m_gyro.rotationRate);
            GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + m_gyro.attitude);
            GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + m_gyro.enabled);
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        /*Quaternion newRot = m_gyro.attitude;
        newRot.eulerAngles -= new Vector3(-90, 0.0f, 0.0f);
        newRot.eulerAngles *= -1.0f;
        transform.rotation = newRot;*/
        if (Input.touchCount > 0)
        {
            Touch touch1 = Input.touches[0];
            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    Vector2 touchPos = touch1.deltaPosition;
                    //Debug.Log($"TouchPos: {touchPos}");
                    float mult = 0.1f;
                    Quaternion newRot = transform.rotation;
                    newRot.w += Mathf.Cos(touchPos.y * mult * 0.5f);
                    newRot.x += Mathf.Sin(touchPos.y * mult * 0.5f);
                    transform.rotation = newRot;

                    /*Quaternion newQuat = transform.rotation;
                    newQuat.eulerAngles += new Vector3(touchPos.y, 0, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newQuat, 1.0f);
                    Debug.Log($"NewRot: {transform.rotation.eulerAngles}");*/

                    //transform.rotation = Quaternion.Euler(0, touchPos.y, 0);
                    //transform.rotation.eulerAngles += new Vector3(0, touchPos.y, 0);
                    break;
                case TouchPhase.Ended:
                    break;
            }

        }
    }
}
