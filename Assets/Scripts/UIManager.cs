using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author: https://github.com/Eduardo-Gonelli
/// Last Update: 2024/09/11
/// </summary>

public class UIManager : MonoBehaviour
{
    DataManager dataManager;
    public TMPro.TextMeshProUGUI textMeshProUGUI;    
    public PlayerRootObject playersObj;
    // campos para receber input de texto para o player e para o score
    public TMPro.TMP_InputField inputFieldPlayer;
    public TMPro.TMP_InputField inputFieldScore;
    public GameObject contentScroll;
    
    void Start()
    {
        // retrieves the persistent DataManager object
        // localiza o data manager persistente
        dataManager = FindFirstObjectByType<DataManager>();
    }

    public void CarregarDados()
    {
        dataManager.LoadData();
    }

    public void SalvaNovosDados()
    {
        // chama o método InsertData passando o apelido do jogador e a pontuação
        if (inputFieldPlayer.text == "" || inputFieldScore.text == "")
        {
            Debug.Log("Os dados não foram preenchidos corretamente.");
            return;
        }
        dataManager.InsertData(inputFieldPlayer.text, int.Parse(inputFieldScore.text)); 
        // aguarda 1 segundo e carrega os dados novamente
        Invoke("CarregarDados", 1f);
    }


    private void OnEnable()
    {        
        // inscreve-se no evento
        DataManager.onDataLoaded += UpdateUI;
    }
    
    private void OnDisable()
    {        
        // cancela a inscrição no evento
        DataManager.onDataLoaded -= UpdateUI;
    }
    
    // esta função é chamada quando o evento é disparado
    void UpdateUI(string json)
    {        
        // O JsonUtility do Unity só suporta objetos como nós de nível superior,        
        // Então precisamos acessar o objeto raiz identificado com colchetes no arquivo JSON.
        // Exemplo de Bunny83: https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html        
        // o valor "players" liga cada jogador com PlayerRootObject.players
        playersObj = JsonUtility.FromJson<PlayerRootObject>("{\"players\":" + json + "}");
        // atualiza a ui com os dados novos
        // reseta o texto do textMeshProUGUI
        textMeshProUGUI.text = "";
        // limpa a lista de textos adicionados em todas as atualizações
        if (contentScroll.transform.childCount > 0)
        {
            // usa um loop invertido para garantir que todos os filhos sejam destruídos corretamente
            for (int i = contentScroll.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(contentScroll.transform.GetChild(i).gameObject);
            }
        }
        // cria um novo textMeshProUGUI para cada jogador
        for (int i = 0; i < playersObj.players.Length; i++)
        {
            
            TMPro.TextMeshProUGUI newTMP = Instantiate(textMeshProUGUI, contentScroll.transform);
            newTMP.text += "Apelido: " + 
                playersObj.players[i].apelido + 
                "\nPontuação: " + playersObj.players[i].pontos + 
                "\nData: " + playersObj.players[i].data +
                "\n\n";
        }
    }
}
