using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class User : MonoBehaviour
{
	public string Username;
	public string Password;
	public string session_id;

	public static User Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	public void setUser(string Username, string Password)
	{
		this.Username = Username;
		this.Password = Password;
	}

	public void setSessionId(string session_id)
	{
		this.session_id = session_id;
	}
}
