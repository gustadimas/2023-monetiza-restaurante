using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prato : MonoBehaviour
{
    public  GameObject[] listaAlimentos = new GameObject[5];
    [SerializeField] private GerenciadorJogo scriptGerenciador;
    public int indexCount, alimentosVerificados;
   
    void Start() 
    {
        scriptGerenciador = GameObject.Find("Gerenciador").GetComponent<GerenciadorJogo>();
    }

    public void PegarAlimento(Bancada bancada)
    {
        if (indexCount <= 4)
        {
            listaAlimentos[indexCount] = Bancada.objectInstanciado;
            Bancada.objectInstanciado.transform.parent = transform;
            Bancada.objectInstanciado.SetActive(false);
            print(listaAlimentos[indexCount]);
            indexCount++;

            if(bancada.objetoNaBancada.tag == "prato" && (Bancada.objectInstanciado.tag == "carne"))
            {
                transform.GetChild(3).gameObject.SetActive(true);
            }

            if (bancada.objetoNaBancada.tag == "prato" && (Bancada.objectInstanciado.tag == "alface"))
            {
                transform.GetChild(6).gameObject.SetActive(true);
            }

            if (bancada.objetoNaBancada.tag == "prato" && (Bancada.objectInstanciado.tag == "queijo"))
            {
                transform.GetChild(5).gameObject.SetActive(true);
            }

            if (bancada.objetoNaBancada.tag == "prato" && (Bancada.objectInstanciado.tag == "tomate"))
            {
                transform.GetChild(4).gameObject.SetActive(true);
            }
            if (bancada.objetoNaBancada.tag == "prato" && (Bancada.objectInstanciado.tag == "pao"))
            {
                transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        else
        {
            indexCount = 0;
        }
    }

    public void VerificarPedido()
    {
        foreach(Missao missao in scriptGerenciador.missoes)
        {
            for(int i = 0; i < listaAlimentos.Length; i++)
            {
                if(listaAlimentos[i].tag == missao.missoesSorteadas[i])
                {
                    alimentosVerificados++;
                    EntregarPedido(missao);
                }
            }
        }
    }

    public void EntregarPedido(Missao concluirMissao)
    {
        if(alimentosVerificados == 5)
        {
            concluirMissao.Concluir();
            Bancada.objectInstanciado = null;
            Jogador.pegarItem = true;
            Destroy(gameObject);
        }
    }
}