using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasMainController : MonoBehaviour
{
    private Animator m_mainAnimCont;

    [SerializeField]
    [Tooltip("Sort button from store page")]
    private Animator m_storeSortAnimCont;
    private bool m_sortIsOpen = false;

    [SerializeField]
    private GameObject m_goProfile;
    [SerializeField]
    private GameObject m_goFAQ;
    [SerializeField]
    private GameObject m_goAbout;
    [SerializeField]
    private GameObject m_goCreate;
    [SerializeField]
    private GameObject m_goCreateAR;
    [SerializeField]
    private GameObject m_goStore;
    [SerializeField]
    private GameObject m_goMainUI;

    // Start is called before the first frame update
    void Start()
    {
        m_mainAnimCont = GetComponent<Animator>();

        Debug.Assert(m_storeSortAnimCont != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateToCreateAR()
    {
        m_mainAnimCont.SetTrigger("createAROpen");
        m_mainAnimCont.SetTrigger("createClose");
    }
    public void SortButtonTrigger()
    {
        if(!m_sortIsOpen)
        {
            m_storeSortAnimCont.SetTrigger("SortOpen");
            m_sortIsOpen = true;
        }
        else
        {
            m_storeSortAnimCont.SetTrigger("SortClose");
            m_sortIsOpen = false;
        }
    }
    public void ProfileToFAQ()
    {
        m_goProfile.SetActive(false);
        m_goFAQ.SetActive(true);
    }
    public void FAQToAbout()
    {
        m_goFAQ.SetActive(false);
        m_goAbout.SetActive(true);
    }
    public void AboutToCreate()
    {
        m_goAbout.SetActive(false);
        m_goCreate.SetActive(true);
    }
    public void CToCAR()
    {
        m_goCreate.SetActive(false);
        m_goCreateAR.SetActive(true);
    }
    public void CreateARToStore()
    {
        m_goCreateAR.SetActive(false);
        m_goStore.SetActive(true);
    }
    public void StoreToMAinUI()
    {
        m_goStore.SetActive(false);
        m_goMainUI.SetActive(true);
    }
    public void MainUIToProfile()
    {
        m_goMainUI.SetActive(false);
        m_goProfile.SetActive(true);
    }
}
