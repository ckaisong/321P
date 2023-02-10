using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadIMG : MonoBehaviour
{
    /*[SerializeField]
    private TextMeshProUGUI m_displayText;*/

    [SerializeField]
    private RawImage m_RawImg;
    // Start is called before the first frame update
    void Start()
    {
        EventTrigger imgTrig = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry imgTrigEntry = new EventTrigger.Entry();
        imgTrigEntry.eventID = EventTriggerType.PointerClick;
        imgTrigEntry.callback.AddListener((data) =>
        {
            NativeGallery.GetImageFromGallery(IMGPath, "Select image to load");
        });
        imgTrig.triggers.Add(imgTrigEntry);
    }

    void IMGPath(string _path)
    {
        //m_displayText.text = _path;
        byte[] imgArray  = File.ReadAllBytes(_path);
        Texture2D imgTexture = new Texture2D(1, 1);
        bool isLoaded = imgTexture.LoadImage(imgArray);
        if(isLoaded)
        {
            m_RawImg.texture = imgTexture;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
