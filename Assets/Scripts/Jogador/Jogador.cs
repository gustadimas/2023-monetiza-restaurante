using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField]private GameObject objetoInterativo;
    [SerializeField] private OpcoesBancada solicitaItens;
    [SerializeField]private Bancada scriptBancada;
    public static bool pegarItem;
    public GameObject objetoIndicador;
    public GameObject maoJogador;

    
    void Start()
    {
        pegarItem = true;
        maoJogador = GameObject.Find("mao");
        scriptBancada = null;
    }

    void Update()
    {
        SetaIndicadorNoAlvo();
        if(scriptBancada != null)
            PegaObjetoInstanciado();
        print(pegarItem);

        objetoIndicador.transform.eulerAngles = new Vector3(0, 360f, 0);
    }

    public void RetornaBancadas() //Esse Metodo está sendo usado pelo botão de "Interagir"
    {
        if(objetoInterativo != null)
        {
            solicitaItens = scriptBancada.tiposBancada; //Variavel solicitaItens é um Enum do tipo OpcoesBancada que é lá do codigo de Bancadas e está retornando o nome especifico do enum
            scriptBancada.FuncaoBancada(solicitaItens);
        }
    }

    public void SetaIndicadorNoAlvo()
    {
        Ray raio = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        float distanciaMaxima = 1f;

        if (Physics.Raycast(raio, out hit, distanciaMaxima))
        {
            objetoInterativo = hit.collider?.gameObject;
            scriptBancada = objetoInterativo.GetComponent<Bancada>();
            
            if(objetoInterativo != null && objetoInterativo.tag == "Bancadas")
            {
                objetoIndicador.SetActive(true);
                objetoIndicador.transform.position = new Vector3(objetoInterativo.transform.position.x,objetoInterativo.transform.position.y + 3, objetoInterativo.transform.position.z);
            }         
        }
        else
        {
            objetoInterativo = null;

            if(objetoInterativo == null)
            {
                objetoIndicador.SetActive(false);
            }
        } 
    }

    public void PegaObjetoInstanciado()
    {
        if(Bancada.objectInstanciado != null)
        {
            print("Meu Objeto é:" + Bancada.objectInstanciado);
            if(!pegarItem)
            {
                Bancada.objectInstanciado.transform.position = maoJogador.transform.position;
                print("Estou segurando algo");
            }
        }
    }   
}