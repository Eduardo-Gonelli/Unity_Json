using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// @author: https://github.com/Eduardo-Gonelli
/// Last Update: 2022/09/18
/// </summary>

public class DataManager : MonoBehaviour
{
    // use the server path of your json file here
    private string url = "http://localhost/senac_aulas/senac_aulas_exemplos/grad_gsd/ex_json_php/data.json";
    // if you have a file that handle the json insert, use here
    private string url_send_data = "http://localhost/senac_aulas/senac_aulas_exemplos/grad_gsd/ex_json_php/json_adicionar.php";
    public string json;
    //public byte[] data;
    
    void Start()
    {
        // persists the data
        DontDestroyOnLoad(this);
        StartCoroutine("LoadDataFromJson");
    }

    IEnumerator LoadDataFromJson()
    {
        // load data from file
        UnityWebRequest www = UnityWebRequest.Get(url);
        // bypass https security (for http only if your server do not support https)
        www.certificateHandler = new ByPassHTTPSCertificate();
        // wait for page response
        yield return www.SendWebRequest();
        // if the page do not laod
        if(www.result != UnityWebRequest.Result.Success)
        {
            // handle the error
            Debug.Log(www.error);
        }
        else
        {
            // example from https://stackoverflow.com/questions/66683347/parsing-json-from-api-url-in-unity-via-c-sharp by Art Zolina III
            // get results as text
            json = www.downloadHandler.text;
            //Debug.Log(json);
            // results as binary data
            // data = www.downloadHandler.data;
            SceneManager.LoadScene("Main");
        }
    }

    // insert new player in json file
    public void InsertData(string name, string age, string score)
    {
        StartCoroutine(InsertNewData(name, age, score));
    }

    IEnumerator InsertNewData(string name, string age, string score)
    {
        // these form are send to the create json file by POST
        WWWForm form = new WWWForm();
        form.AddField("nome", name);
        form.AddField("idade", age);
        form.AddField("pontos", score);

        using(UnityWebRequest www = UnityWebRequest.Post(url_send_data, form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("New data inserted.");
            }
        }
    }
}
