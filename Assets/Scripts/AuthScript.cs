using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleJSON;


[Serializable]
public class TokenResponse {
    public string access_token;
}
[Serializable]
public class UserInfo {
    public string name;
}

/// <summary>
/// 
/// The script doesn't make sense, I just tried
/// 
/// </summary>
public class AuthScript : MonoBehaviour {
    [SerializeField]
    private string serverURL = "http://each.itsociety.su:5000/oauth2/authorize?response_type=code&client_id=RwFuaDC2TifPUf9jg0Nv71eQeLlVLorRzeXcADqjT0Ye3wWw&redirect_uri=http%3A%2F%2Feach.itsociety.su%3A4201%2Feach%2Fswagger-ui%2Foauth2-redirect.html&state=TW9uIEp1bCAzMCAyMDE4IDE2OjI5OjA4IEdNVCswMzAwIChNU0sp";

    public IEnumerator Login(string email, string password) {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "password");
        form.AddField("client_id", "");
        form.AddField("client_secret", "");
        form.AddField("username", email);
        form.AddField("password", password);
        form.AddField("scope", "*");

        WWW w = new WWW(serverURL + "oauth/token", form);

        yield return w;

        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
        }
        else {
            TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(w.text);

            if (tokenResponse == null) {
                Debug.Log("Convertion failed!");
            }
            else {
               
                PlayerPrefs.SetString("Token", tokenResponse.access_token);
                Debug.Log("Token was set!");
               
                StartCoroutine(GetUserInfo());
            }
        }
    }
    public IEnumerator GetUserInfo() {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + PlayerPrefs.GetString("Token"));
        WWW w = new WWW(serverURL + "api/user", null, headers);

        yield return w;

        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
        }
        else {
            UserInfo userInfo = JsonUtility.FromJson<UserInfo>(w.text);

            if (userInfo == null) {
                Debug.Log("Convertion failed!");
            }
            else {
                PlayerPrefs.SetString("UserName", userInfo.name);
                Debug.Log("Username was set!");
            }
        }
    }
}
