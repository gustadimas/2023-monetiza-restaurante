using UnityEngine;

public enum OpcoesBancada
{
    PAO, CARNE, QUEIJO, ALFACE, TOMATE, PRATO, LIXEIRA, FOGAO, CORTAR, ENTREGARPEDIDO, BANCADAVAZIA
}

public class Bancada : MonoBehaviour
{
    public OpcoesBancada tiposBancada;
    public bool colocarNaBancada = true;
    public static bool fritandoItem = false;
    private bool cortouItem = false;
    private float tempoFrituraCarne = 10.0f;
    public int tempoCorte = 4;

    private GameObject filhoFritura;
    public static GameObject objectInstanciado;
    public GameObject objetoNaBancada;
    public GameObject[] listaObjetos = new GameObject[7];

    Animator animCortar;

    void Start()
    {
        colocarNaBancada = true;
        animCortar = GetComponent<Animator>();
        print(tempoCorte);
    }


    void Update()
    {
        if (fritandoItem)
        {
            tempoFrituraCarne -= Time.deltaTime;

            print(tempoFrituraCarne);
            if(tempoFrituraCarne <= 0)
            {
                if (filhoFritura != null)
                {
                    filhoFritura.SetActive(true);
                    objetoNaBancada.transform.GetChild(0).gameObject.SetActive(false);

                    fritandoItem = false;
                    tempoFrituraCarne = 10.0f;

                    filhoFritura = null;
                }
            }
        }
    }

    public void FuncaoBancada(OpcoesBancada nomeAlimento) //Aqui verifica qual item foi solicitado se o jogador interagir e se o raycast identificar objeto com Script Bancada
    {
        switch (nomeAlimento)
        {
            case OpcoesBancada.PAO:
                if (Jogador.pegarItem)
                {
                    objectInstanciado = Instantiate(listaObjetos[(int)nomeAlimento], transform.position, transform.rotation);
                    Jogador.pegarItem = false;
                }
                break;

            case OpcoesBancada.CARNE:
                if (Jogador.pegarItem)
                {
                    Jogador.pegarItem = false;
                    objectInstanciado = Instantiate(listaObjetos[(int)nomeAlimento], transform.position, transform.rotation);
                }
                break;

            case OpcoesBancada.QUEIJO:
                if (Jogador.pegarItem)
                {
                    objectInstanciado = Instantiate(listaObjetos[(int)nomeAlimento], transform.position, transform.rotation);
                    Jogador.pegarItem = false;
                }
                break;

            case OpcoesBancada.ALFACE:
                if (Jogador.pegarItem)
                {
                    objectInstanciado = Instantiate(listaObjetos[(int)nomeAlimento], transform.position, transform.rotation);
                    Jogador.pegarItem = false;
                }
                break;

            case OpcoesBancada.TOMATE:
                if (Jogador.pegarItem)
                {
                    objectInstanciado = Instantiate(listaObjetos[(int)nomeAlimento], transform.position, transform.rotation);
                    Jogador.pegarItem = false;
                }
                break;

            case OpcoesBancada.PRATO:
                if (Jogador.pegarItem)
                {
                    objectInstanciado = Instantiate(listaObjetos[(int)nomeAlimento], transform.position, transform.rotation);
                    Jogador.pegarItem = false;
                }
                break;

            case OpcoesBancada.LIXEIRA:
                Destroy(objectInstanciado);
                Jogador.pegarItem = true;
                objectInstanciado = null;
                break;

            case OpcoesBancada.FOGAO:
                if (!Jogador.pegarItem && colocarNaBancada && objetoNaBancada == null && !objectInstanciado.transform.GetChild(1).gameObject == false)
                {
                    objetoNaBancada = objectInstanciado;
                    if (objetoNaBancada.tag == "carne")
                    {
                        Jogador.pegarItem = true;
                        objetoNaBancada.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                        colocarNaBancada = false;
                        objectInstanciado = null;
                        fritandoItem = true;

                        filhoFritura = objetoNaBancada.transform.GetChild(1).gameObject;
                    }
                }
                else if (Jogador.pegarItem && !colocarNaBancada && objetoNaBancada != null)
                {
                    if (fritandoItem == false)
                    {
                        objectInstanciado = objetoNaBancada;
                        Jogador.pegarItem = false;
                        colocarNaBancada = true;
                        objetoNaBancada = null;
                        tempoFrituraCarne = 10.0f;
                    }
                }
                break;

            case OpcoesBancada.BANCADAVAZIA:
                if (!Jogador.pegarItem && colocarNaBancada && objetoNaBancada == null)
                {
                    objetoNaBancada = objectInstanciado;
                    if (objetoNaBancada == objectInstanciado)
                    {
                        Jogador.pegarItem = true;
                        objetoNaBancada.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                        colocarNaBancada = false;
                        objectInstanciado = null;
                    }
                }
                else if (Jogador.pegarItem && !colocarNaBancada && objetoNaBancada != null)
                {
                    objectInstanciado = objetoNaBancada;
                    Jogador.pegarItem = false;
                    colocarNaBancada = true;
                    objetoNaBancada = null;
                }

                if (!Jogador.pegarItem && !colocarNaBancada && objetoNaBancada != null)
                {
                    if (objetoNaBancada.tag == "prato" && (objectInstanciado.tag == "carne" ||
                        objectInstanciado.tag == "pao" ||
                        objectInstanciado.tag == "queijo" ||
                        objectInstanciado.tag == "alface" ||
                        objectInstanciado.tag == "tomate"))
                    {
                        if (objetoNaBancada.GetComponent<Prato>().indexCount != 5)
                        {
                            objectInstanciado.transform.GetChild(0).gameObject.SetActive(true);
                            Jogador.pegarItem = true;
                            objetoNaBancada.GetComponent<Prato>().PegarAlimento(this);
                            colocarNaBancada = false;
                            objectInstanciado = null;
                        }
                    }
                }
                break;

            case OpcoesBancada.CORTAR:
                if (!Jogador.pegarItem && colocarNaBancada && objetoNaBancada == null)
                {
                    objetoNaBancada = objectInstanciado;

                    if (objetoNaBancada != null && objetoNaBancada.tag != "carne")
                    {
                        if (objetoNaBancada.tag == "tomateInteiro" || objetoNaBancada.tag == "queijoInteiro" || objetoNaBancada.tag == "alfaceInteiro")
                        {
                            Jogador.pegarItem = true;
                            objetoNaBancada.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                            colocarNaBancada = false;
                            objectInstanciado = null;
                        }
                    }
                    else if (objetoNaBancada != null && objetoNaBancada.tag == "carne")
                    {
                        objetoNaBancada = null;
                        Debug.LogWarning("Você não pode cortar carne aqui!");
                    }
                }
                else if (Jogador.pegarItem && !colocarNaBancada && objetoNaBancada != null)
                {
                    if (objetoNaBancada.transform.GetChild(1).gameObject.activeInHierarchy && cortouItem == true && tempoCorte == 0)
                    {
                        MovimentoJogador.desativarEntradas = false;
                        objectInstanciado = objetoNaBancada;
                        Jogador.pegarItem = false;
                        colocarNaBancada = true;
                        objetoNaBancada = null;
                        tempoCorte = 4;
                        cortouItem = false;
                    }

                    else if (cortouItem == false)
                    {
                        MovimentoJogador.desativarEntradas = true;
                        animCortar.SetTrigger("Cut");
                        tempoCorte--;
                        print(tempoCorte);
                    }

                    if (tempoCorte == 0)
                    {
                        objetoNaBancada.transform.GetChild(1).gameObject.SetActive(true);
                        objetoNaBancada.transform.GetChild(0).gameObject.SetActive(false);

                        if (objetoNaBancada.tag == "tomateInteiro")
                        {
                            objetoNaBancada.tag = "tomate";
                        }

                        if (objetoNaBancada.tag == "alfaceInteiro")
                        {
                            objetoNaBancada.tag = "alface";
                        }

                        if (objetoNaBancada.tag == "queijoInteiro")
                        {
                            objetoNaBancada.tag = "queijo";
                        }

                        cortouItem = true;
                    }
                }
                break;

            case OpcoesBancada.ENTREGARPEDIDO:
                objectInstanciado.GetComponent<Prato>().VerificarPedido();
                break;
        }
    }
}