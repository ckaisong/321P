using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vuforia;

public class CustomisationManager : DefaultObserverEventHandler
{
    [SerializeField]
    GameObject m_BtnPlace;
    [SerializeField]
    GameObject m_prefabEditPlot;
    [SerializeField]
    RawImage DispImg;

    ImageTargetBehaviour ImgTarget;
    GameObject m_goPlot;
    Camera m_MainCamera;

    GameObject m_goHolding;
    bool m_bPlaceable;

    // Start is called before the first frame update
    protected override void Start()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += VufInitialised;
        m_BtnPlace.SetActive(false);
        //GameObjectFactory.Instance.GetPrefab("hwhe");
        m_MainCamera = Camera.main;
        m_bPlaceable= false;
        EventTrigger etPlace= m_BtnPlace.AddComponent<EventTrigger>();
        EventTrigger.Entry triggEntry = new EventTrigger.Entry();
        triggEntry.eventID = EventTriggerType.PointerClick;
        triggEntry.callback.AddListener((data) => {
            if (!m_bPlaceable || m_goHolding == null)
                return;
            m_goHolding.transform.SetParent(m_goPlot.transform);
            m_goHolding = null;
            Debug.Log("Place object");
        });
        etPlace.triggers.Add(triggEntry);
        //Debug.Log("CustMan Started");
    }
    /* After Vuforia is initialised, **Do this**
     * 
     */
    private void VufInitialised(VuforiaInitError err)
    {
        if(err == VuforiaInitError.NONE)
        {
            //NativeGallery.GetImageFromGallery(ProcessIMG, "Select image to be tracked");
            /*ImgTarget =  VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(dataSetPath, targetName);
            ImgTarget.OnTargetStatusChanged += TargetStatusChanged;*/
        }
    }
    /* Process the image passed in, and create an Image Tracking instance with it
     * 
     */
    private void ProcessIMG(string _path)
    {

        byte[] imgArray = File.ReadAllBytes(_path);
        //Create a Texture2D for the image to assign to
        Texture2D texture = new Texture2D(2,2);
        //If image loads in successfully
        if (texture.LoadImage(imgArray))
        {
            //set display image for visual reference
            DispImg.transform.localScale = new Vector3(texture.width/texture.height, 1, 0);
            DispImg.texture = texture;

            //Calls VuforiaBehaviour to create an Image target instance with the loaded texture
            ImgTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(texture, 0.115f, "TissuePaper");
            //On status change
            ImgTarget.OnTargetStatusChanged += TargetStatusChanged;
            /*DefaultObserverEventHandler doeh = ImgTarget.AddComponent<DefaultObserverEventHandler>();
            doeh.StatusFilter = TrackingStatusFilter.Tracked;
            doeh.OnTargetFound = new UnityEngine.Events.UnityEvent();
            doeh.OnTargetFound.AddListener(TrackingFound);
            doeh.OnTargetLost = new UnityEngine.Events.UnityEvent();
            doeh.OnTargetLost.AddListener(TrackingLost);*/

            //Instantiate editting plot object for visual reference of AR orientation
            m_goPlot = Instantiate(m_prefabEditPlot, ImgTarget.transform);
            //spwn.transform.SetParent(ImgTarget.transform);
            //spwn.transform.parent = ImgTarget.transform;
            m_goPlot.transform.localPosition = Vector3.zero;

        }


    }

    public void HoldObject(string _nameOfPrefab)
    {
        if(m_goHolding != null)
        {
            Destroy(m_goHolding);
        }
        m_goHolding = Instantiate(GameObjectFactory.Instance.GetPrefab(_nameOfPrefab), m_MainCamera.transform);
        m_goHolding.transform.localPosition = new Vector3(0.0f, 0.0f, 0f);
        Quaternion newRot = Quaternion.identity;
        //newRot.eulerAngles = new Vector3(0f, -90f, 0f);
        m_goHolding.transform.rotation = newRot;
    }
    private void TargetStatusChanged(ObserverBehaviour _behaviour, TargetStatus _status)
    {
        Debug.Log($"Img status: {_status.Status}");
        /*switch (_status.Status)
        {
            case Status.NO_POSE:
                break;
            case Status.TRACKED: 
                break;
            case Status.LIMITED: 
                break;
            case Status.EXTENDED_TRACKED: 
                break;
        }*/
        if (_status.Status == Status.TRACKED)
        {
            m_BtnPlace.SetActive(true);
            //m_goPlot.SetActive(true);
            m_bPlaceable = true;
        }
        else
        {
            m_BtnPlace.SetActive(false);
            //m_goPlot.SetActive(false);
            m_bPlaceable = false;
        }
    }

    /*protected override */void TrackingFound()
    {
        //base.OnTrackingFound();
        Debug.Log("Img found");
        m_BtnPlace.SetActive(true);
        m_goPlot.SetActive(true);
        /*if(!m_bInit)
        {
            GameObject spwn = Instantiate(m_prefabScene);
            Debug.Log($"ImgTarget name: {ImgTarget.transform.name}, GO parent: {ImgTarget.gameObject.name}");
            spwn.transform.parent = ImgTarget.transform;
            spwn.transform.localPosition = Vector3.zero;
            spwn.SetActive(true);
            m_bInit= true;
        }*/
    }
    /*protected override */void TrackingLost()
    {
        Debug.Log("Img lost");
        m_BtnPlace.SetActive(false);
        m_goPlot.SetActive(false);
        //base.OnTrackingLost();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
