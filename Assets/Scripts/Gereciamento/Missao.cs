using System;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Missao : MonoBehaviour
{
    public bool missaoCompleta = false;
    public string[] itensMissao = { "pao", "carne", "queijo", "alface", "tomate" };
    public string[] missoesSorteadas = new string[5];
    public Action ConcluirMissao;
    public TextMeshProUGUI TextoMissao;

    void Awake()
    {
        for (int i = 0; i < itensMissao.Length; i++)
        {
            missoesSorteadas[i] = itensMissao[Random.Range(0, 5)];

            for (int j = 0; j < i; j++)
            {
                if (missoesSorteadas[i] == missoesSorteadas[j])
                {
                    i--;
                }
            }
        }

        AtualizarTextoMissao();
    }

    public void Concluir()
    {
        missaoCompleta = true;
        AtualizarTextoMissao();
        ConcluirMissao?.Invoke();
    }

    void AtualizarTextoMissao()
    {
        if (TextoMissao != null)
        {
            TextoMissao.text = "Missão Concluída";
            TextoMissao.color = Color.green;
        }
    }
}
