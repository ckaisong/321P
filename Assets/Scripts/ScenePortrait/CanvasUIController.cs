using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ModelsPanelGO;
    [SerializeField]
    private GameObject m_FiltersPanelGO;

    private bool m_bModelsPanelIsOpen = false;
    private bool m_bFiltersPanelIsOpen = false;
    private Animator m_animator;

    private bool m_sortPanelIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(m_ModelsPanelGO != null);
        Debug.Assert(m_FiltersPanelGO != null);

        m_animator = GetComponent<Animator>();

        //add EventTrigger component
        EventTrigger m_ETtrigger = gameObject.AddComponent<EventTrigger>();
        //create Entry object to specify event specification
        EventTrigger.Entry COCevent = new EventTrigger.Entry();
        //set event to listen to PointerClick event
        COCevent.eventID = EventTriggerType.PointerClick;
        //add function to be triggered by event
        COCevent.callback.AddListener((data)=> { ClickOutsideCheck((PointerEventData)data); });
        //add event to EventTrigger component
        m_ETtrigger.triggers.Add(COCevent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* Function to check if the pointer click event is within the panels of interest
     * Calls FindChildRecursive()
     */
    public void ClickOutsideCheck(PointerEventData _ped)
    {
        RaycastResult pointerRaycast = _ped.pointerCurrentRaycast;
        if(m_bModelsPanelIsOpen)
        {
            //if pressing models panel's child objects, exit function
            if (FindChildRecursive(m_ModelsPanelGO.transform, pointerRaycast.gameObject.name))
                return;
            //close sort panel if is open
            if(m_sortPanelIsOpen)
            {
                m_animator.SetTrigger("SPclose");
                m_sortPanelIsOpen = false;
            }
            //else close the panel
            m_bModelsPanelIsOpen =false;
            m_animator.SetTrigger("MPclose");
        }
        else if (m_bFiltersPanelIsOpen)
        {
            //if pressing models panel's child objects, exit function
            if (FindChildRecursive(m_FiltersPanelGO.transform, pointerRaycast.gameObject.name))
                return;
            //else close the panel
            m_bFiltersPanelIsOpen = false;
            m_animator.SetTrigger("FPclose");
        }
    }
    /* Recursive function to thoroughly find if parent object has child of specified name
     *      Parameters:
     *          - _theParent: transform of the parent to be searched on
     *          - _childName: the name of the child being searched for
     * Called by ClickOutsideCheck()
     */
    private bool FindChildRecursive(Transform _theParent, string _childName)
    {
        //loop through each child
        for (int c = 0; c < _theParent.childCount; ++c)
        {
            //store child in a variable
            Transform child = _theParent.GetChild(c);
            //check if this child is the child we are finding for
            if(child.gameObject.name == _childName)
                return true;
            //if not, if there's children under this child
            if (child.childCount > 0)
            {
                //recursive call this function to search through each children
                if(FindChildRecursive(child, _childName))
                {
                    //if this call returns true (a child is found), return true and abort further calculations
                    return true;
                }
            }
        }
        //if none found return false
        return false;
    }
    /* To trigger Models panel animation
     *  Parameter: 
     *      - _isOpen: whether this function is triggered to open(true) or close(false)
     */
    public void ModelsPanelTrigger(bool _isOpen)
    {
        //if function is called to open the panel
        if(_isOpen)
        {
            //set panel is open bool to true
            m_bModelsPanelIsOpen = true;
            //trigger animation
            m_animator.SetTrigger("MPopen");
        }
        else
        {
            //else if to close panel
            //set panel is open bool to false
            m_bModelsPanelIsOpen = false;
            //trigger animation
            m_animator.SetTrigger("MPclose");
        }
    }
    /* To trigger Filters panel animation
     *  Parameter: 
     *      - _isOpen: whether this function is triggered to open(true) or close(false)
     */
    public void FiltersPanelTrigger(bool _isOpen)
    {
        //if function is called to open the panel
        if (_isOpen)
        {
            //Dont trigger opening of filters panel if models panel is already open
            if (m_bModelsPanelIsOpen)
            {
                return;
            }
            //set panel is open bool to true
            m_bFiltersPanelIsOpen = true;
            //trigger animation
            m_animator.SetTrigger("FPopen");
        }
        else
        {
            //else if to close panel
            //set panel is open bool to false
            m_bFiltersPanelIsOpen = false;
            //trigger animation
            m_animator.SetTrigger("FPclose");
        }
    }
    /* To trigger sorting panel animation
     */
    public void SortPanelTrigger()
    {
        if (!m_sortPanelIsOpen)
        {
            m_animator.SetTrigger("SPopen");
            m_sortPanelIsOpen = true;
        }
        else
        {
            m_animator.SetTrigger("SPclose");
            m_sortPanelIsOpen = false;
        }
    }
}
