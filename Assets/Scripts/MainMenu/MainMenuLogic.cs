using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic : MonoBehaviour
{
    /*[SerializeField]
    [Tooltip("Prefab of the Start screen")]
    private GameObject m_prefabStartScreen;
    
    [SerializeField]
    [Tooltip("Prefab of the Intro screen")]
    private GameObject m_prefabIntroScreen;

    [SerializeField]
    [Tooltip("Prefab of the Sign in screen")]
    private GameObject m_prefabSignInScreen;

    [SerializeField]
    [Tooltip("Prefab of the Sign up screen")]
    private GameObject m_prefabSignUpScreen;

    [SerializeField]
    [Tooltip("Prefab of the Create project screen")]
    private GameObject m_prefabCreateProjScreen;

    [SerializeField]
    [Tooltip("Prefab of the select template screen")]
    private GameObject m_prefabSelectTemplateScreen;*/

    [SerializeField]
    [Tooltip("All the UI prefab screens in the same order as the enum below")]
    private GameObject[] m_prefabScreens;

    //All Ui screens in the same order as the array above
    [Serializable]
    enum TypeOfScreens
    {
        TOS_StartScreen,
        TOS_IntroScreen,
        TOS_SignInScreen,
        TOS_SignUpScreen,
        TOS_CreateProjScreen,
        TOS_SelectTemplateScreen,
        TOS_NumOfScene
    };
    [SerializeField]
    [Tooltip("The UI screen to start in")]
    private TypeOfScreens m_startScreen;

    [SerializeField]
    [Tooltip("Gameobject of the bottom bar")]
    private GameObject m_BottomBar;

    [SerializeField]
    [Tooltip("Speed of screens transitioning in and out")]
    private float m_transitionSpeed = 5.0f;

    private GameObject currScene;

    // Start is called before the first frame update
    void Start()
    {
        //Check prefabs assign tallies with enum
        Debug.Assert(m_prefabScreens.Length == (int)TypeOfScreens.TOS_NumOfScene, "Prefab count does not tally with enums");
        //currScene = Instantiate(m_prefabScreens[(int)m_startScreen], this.transform);
        currScene = m_prefabScreens[(int)m_startScreen];
        currScene.SetActive(true);

        if((int)m_startScreen < (int)TypeOfScreens.TOS_CreateProjScreen)
        {
            //disable bottom bar for first few screens
            m_BottomBar.SetActive(false);
        }
        else
        {
            m_BottomBar.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Transition(GameObject _newScene)
    {
        _newScene.SetActive(true);
        _newScene.transform.localPosition = new Vector3(Screen.width * 0.5f, 0, 0);
        while(_newScene.transform.localPosition.x > 0.1f &&
                currScene.transform.localPosition.x >-Screen.width*0.5f)
        {
            //Move curent screen first
            Vector3 newPos = currScene.transform.localPosition;
            newPos.x -= m_transitionSpeed * Time.deltaTime;
            currScene.transform.localPosition = newPos;
            //Move new screen
            newPos = _newScene.transform.localPosition;
            newPos.x -= m_transitionSpeed * Time.deltaTime;
            _newScene.transform.localPosition = newPos;
            yield return new WaitForEndOfFrame();
        }
        currScene.transform.localPosition = new Vector3(-Screen.width * 0.5f, 0, 0);
        _newScene.transform.localPosition = Vector3.zero;
        currScene.SetActive(false);
        currScene = _newScene;
    }

    public void TransitionToStartScreen()
    {
        StartCoroutine(Transition(m_prefabScreens[(int)TypeOfScreens.TOS_StartScreen]));
        m_BottomBar.SetActive(false);
        /*GameObject newScene = m_prefabScreens[(int)TypeOfScreens.TOS_StartScreen];
        newScene.transform.position = new Vector3(Screen.width, 0, 0);
        Vector3 newPos = currScene.transform.position;
        currScene.transform.position = new Vector3(-Screen.width, 0, 0);*/

    }
    public void TransitionToIntroScreen()
    {
        StartCoroutine(Transition(m_prefabScreens[(int)TypeOfScreens.TOS_IntroScreen]));
        m_BottomBar.SetActive(false);
        /*GameObject newScene = m_prefabScreens[(int)TypeOfScreens.TOS_IntroScreen];
        newScene.SetActive(true);
        newScene.transform.localPosition = new Vector3(Screen.width*1.5f, 0, 0);
        //Vector3 newPos = currScene.transform.position;
        currScene.transform.localPosition = new Vector3(-Screen.width*1.5f, 0, 0);*/

    }
    public void TransitionToSignInScreen()
    {
        StartCoroutine(Transition(m_prefabScreens[(int)TypeOfScreens.TOS_SignInScreen]));
        m_BottomBar.SetActive(false);
    }
    public void TransitionToSignUpScreen()
    {
        StartCoroutine(Transition(m_prefabScreens[(int)TypeOfScreens.TOS_SignUpScreen]));
        m_BottomBar.SetActive(false);
    }
    public void TransitionToCreateProjScreen()
    {
        StartCoroutine(Transition(m_prefabScreens[(int)TypeOfScreens.TOS_CreateProjScreen]));
        m_BottomBar.SetActive(true);
    }
    public void TransitionToSelectTemplateScreen()
    {
        StartCoroutine(Transition(m_prefabScreens[(int)TypeOfScreens.TOS_SelectTemplateScreen]));
        m_BottomBar.SetActive(true);
    }
}
