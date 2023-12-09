using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class GerenciadorJogo : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> textosMissoes;
    [SerializeField] GameObject objetoMissaoPrefab;
    public List<Missao> missoes = new List<Missao>();
    int quantidadeMissaoPorRodada = 3;
    int quantidadeDeRodadas;
    int rodadaAtual = 0;

    private int rodadasConcluidas = 0;
    private int quantidadeMaximaRodadas = 3;

    void Start()
    {
        DefinirQuantidadeDeRodadas();
        TrocarRodada();
    }

    void DefinirQuantidadeDeRodadas()
    {
        quantidadeDeRodadas = Random.Range(3, 10);
    }

    void InstanciarMissoes()
    {
        for (int i = 0; i < quantidadeMissaoPorRodada; i++)
        {
            GameObject missaoObj = Instantiate(objetoMissaoPrefab, transform.position, Quaternion.identity);
            missaoObj.transform.SetParent(transform);

            TextMeshProUGUI textoMissao = textosMissoes[i];

            Missao missao = missaoObj.GetComponent<Missao>();
            missao.TextoMissao = textoMissao; 
            missao.ConcluirMissao += VerificarMissoesConcluidas;
            missoes.Add(missao);
        }
        AtualizarTextoMissoes();
    }

    void LimparMissoes()
    {
        foreach (Missao missao in missoes)
        {
            Destroy(missao.gameObject);
        }
        missoes.Clear();
    }

    void VerificarMissoesConcluidas()
    {
        print("Verificando missoes");
        foreach (Missao missao in missoes)
        {
            if (!missao.missaoCompleta)
            {
                return;
            }
        }

        rodadasConcluidas++;

        if (rodadasConcluidas < quantidadeMaximaRodadas)
        {
            TrocarRodada();
        }
        else
        {
            CarregarNovaCena();
        }
    }

    void AtualizarTextoMissoes()
    {
        for (int i = 0; i < missoes.Count; i++)
        {
            string texto = "Missão " + (i + 1) + ": ";
            for (int j = 0; j < missoes[i].missoesSorteadas.Length; j++)
            {
                texto += missoes[i].missoesSorteadas[j] + ", ";
            }
            textosMissoes[i].text = texto;
        }
    }

    void TrocarRodada()
    {
        rodadaAtual++;

        if (rodadaAtual > quantidadeMaximaRodadas)
        {
            CarregarNovaCena();
            return;
        }

        LimparMissoes();
        InstanciarMissoes();
    }

    void CarregarNovaCena()
    {
        SceneManager.LoadScene("GameOver");
    }
}