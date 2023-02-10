using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrawing : MonoBehaviour
{
    [SerializeField]
    GameObject m_LinePrefab;

    [SerializeField]
    float m_SampleRate = 1.0f;

    [SerializeField]
    PunLogging m_logger;

    LineRenderer _currLine;
    Vector2 _lastPos;
    Vector2 _aspectRatio;


    // Start is called before the first frame update
    void Start()
    {
        //m_logger.AddLogMsg($"Camera deets: {Camera.main.pixelWidth} | {Camera.main.pixelHeight}");
        m_logger.AddLogMsg($"Screen deets: {Screen.width} | {Screen.height}");
        _aspectRatio = new Vector2(Camera.main.aspect, 1 / Camera.main.aspect);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            //get first touch instance
            Touch touch = Input.GetTouch(0);
            Vector2 newPos = touch.position;
            newPos = (newPos - new Vector2(0.5f *Screen.width, 0.5f * Screen.height))/* / Camera.main.orthographicSize*/;
            //if touch is at 'start' phase
            if (touch.phase == TouchPhase.Began)
            {
                m_logger.AddLogMsg($"Start: {newPos}");
                //instantiate line prefab
                _currLine = Instantiate(m_LinePrefab, transform).GetComponent<LineRenderer>();
                //set line to not use world space coordinate
                _currLine.useWorldSpace = false;
                //set first position of line to this position
                _currLine.SetPosition(0, newPos);
                //set last touch position to _lastPos
                _lastPos = newPos;
                //Debug.Log("Pos " + (_currLine.positionCount - 1) + ": " + _lastPos);
            }
            //if touch moved
            if (touch.phase == TouchPhase.Moved)
            {
                //Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distSqr = Vector2.SqrMagnitude(newPos - _lastPos);
                //Debug.Log("new Pos: " + newPos);
                //If distance to last position is less than specified m_SampleRate, dont add new point to the line
                if (distSqr < m_SampleRate * m_SampleRate)
                    return;
                //increment number of position for line
                _currLine.positionCount += 1;
                //if it is more, set the last position to the line to the current touch position
                _currLine.SetPosition(_currLine.positionCount - 1, newPos);
                //Debug.Log("Pos " + (_currLine.positionCount -1) + ": " + newPos);
                //set last position to new position
                _lastPos = newPos;
                //Debug.Log("new lastPos: " + _lastPos);
            }
            //if touch canceled or ended
            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                //set last position to current position
                _currLine.SetPosition(_currLine.positionCount - 1, newPos);
                m_logger.AddLogMsg($"End: {newPos}");
            }
        }
    }
}
