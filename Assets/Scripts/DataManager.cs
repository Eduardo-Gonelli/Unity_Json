using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// @author: https://github.com/Eduardo-Gonelli
/// Last Update: 2025/09/10
/// </summary>

public class DataManager : MonoBehaviour
{
    // evento para carregar os dados
    public delegate void OnDataLoaded(string jsonData);
    public static event OnDataLoaded onDataLoaded;

    // url para receber os dados
    private string url_load_data = "http://localhost/senac/a6gsd/sistema/recuperar.php";
    // url para enviar os dados
    private string url_send_data = "http://localhost/senac/a6gsd/sistema/adicionar.php";
    public string json;
        
    void Start()
    {        
        DontDestroyOnLoad(this); // não destroi o objeto ao trocar de cena        
    }

    public void LoadData()
    {
        StartCoroutine("LoadDataFromJson");
        
    }

    IEnumerator LoadDataFromJson()
    {        
        WWWForm form = new WWWForm();
        // adiciona o campo da chave secreta
        form.AddField("chave_secreta", "123456");
        // prepara a requisição
        UnityWebRequest www = UnityWebRequest.Post(url_load_data, form);
        // ignora o certificado de segurança https (para http apenas se o servidor não suportar https)
        www.certificateHandler = new ByPassHTTPSCertificate();
        // envia a requisição e aguarda pela resposta        
        yield return www.SendWebRequest();
        // verifica se houve erro na requisição
        if(www.result != UnityWebRequest.Result.Success)
        {
            // exibe o erro
            Debug.Log(www.error);
        }
        else
        {
            // exemplo de https://stackoverflow.com/questions/66683347/parsing-json-from-api-url-in-unity-via-c-sharp por Art Zolina III
            // transforma o resultado em um objeto json
            json = www.downloadHandler.text;
            //Debug.Log(json);
            onDataLoaded?.Invoke(json);
        }
    }

    // prepara a inserção de dados
    public void InsertData(string apelido, int pontos)
    {
        StartCoroutine(InsertNewData(apelido, pontos));
    }

    // envia o formulário para o servidor
    IEnumerator InsertNewData(string apelido, int pontos)
    {
        // prepara o formulário
        WWWForm form = new WWWForm();
        form.AddField("apelido", apelido);        
        form.AddField("pontos", pontos);        
        // envia o formulário para o servidor
        using(UnityWebRequest www = UnityWebRequest.Post(url_send_data, form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Novos dados inseridos");
            }
        }
    }
}
