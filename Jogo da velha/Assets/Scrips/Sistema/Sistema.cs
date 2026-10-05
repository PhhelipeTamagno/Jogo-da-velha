using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sistema : MonoBehaviour
{
    [Header("Casas do Tabuleiro")]
    public Button[] casas = new Button[9];

    [Header("Texto das Casas")]
    public TMP_Text[] textosCasas = new TMP_Text[9];

    [Header("UI")]
    public TMP_Text textoTurno;
    public TMP_Text textoResultado;
    public Button botaoReiniciar;

    [Header("Configuração")]
    public string jogadorX = "X";
    public string jogadorO = "O";

    private string[] tabuleiro = new string[9];

    private string jogadorAtual = "X";
    private bool jogoTerminou = false;

    void Start()
    {
        IniciarJogo();

        for (int i = 0; i < casas.Length; i++)
        {
            int indice = i;
            casas[i].onClick.AddListener(() => Jogar(indice));
        }

        if (botaoReiniciar != null)
        {
            botaoReiniciar.onClick.AddListener(ReiniciarJogo);
        }
    }

    void IniciarJogo()
    {
        for (int i = 0; i < tabuleiro.Length; i++)
        {
            tabuleiro[i] = "";

            if (textosCasas != null && i < textosCasas.Length && textosCasas[i] != null)
            {
                textosCasas[i].text = "";
            }

            if (casas != null && i < casas.Length && casas[i] != null)
            {
                casas[i].interactable = true;
            }
        }

        jogadorAtual = "X";
        jogoTerminou = false;

        if (textoResultado != null)
            textoResultado.text = "";

        AtualizarTextoTurno();
    }

    void Jogar(int indice)
    {
        if (jogoTerminou)
            return;

        if (indice < 0 || indice >= tabuleiro.Length)
            return;

        // Impede jogar em uma casa ocupada
        if (tabuleiro[indice] != "")
            return;

        // Coloca X ou O
        tabuleiro[indice] = jogadorAtual;

        if (textosCasas != null && indice < textosCasas.Length && textosCasas[indice] != null)
        {
            textosCasas[indice].text = jogadorAtual;
        }

        // Desativa a casa
        if (casas != null && indice < casas.Length && casas[indice] != null)
        {
            casas[indice].interactable = false;
        }

        // Verifica vitória
        if (VerificarVitoria(jogadorAtual))
        {
            FinalizarJogo(jogadorAtual + " venceu!");
            return;
        }

        // Verifica empate
        if (VerificarEmpate())
        {
            FinalizarJogo("Empate!");
            return;
        }

        // Troca jogador
        if (jogadorAtual == "X")
            jogadorAtual = "O";
        else
            jogadorAtual = "X";

        AtualizarTextoTurno();
    }

    bool VerificarVitoria(string jogador)
    {
        // Linhas
        if (tabuleiro[0] == jogador &&
            tabuleiro[1] == jogador &&
            tabuleiro[2] == jogador)
            return true;

        if (tabuleiro[3] == jogador &&
            tabuleiro[4] == jogador &&
            tabuleiro[5] == jogador)
            return true;

        if (tabuleiro[6] == jogador &&
            tabuleiro[7] == jogador &&
            tabuleiro[8] == jogador)
            return true;

        // Colunas
        if (tabuleiro[0] == jogador &&
            tabuleiro[3] == jogador &&
            tabuleiro[6] == jogador)
            return true;

        if (tabuleiro[1] == jogador &&
            tabuleiro[4] == jogador &&
            tabuleiro[7] == jogador)
            return true;

        if (tabuleiro[2] == jogador &&
            tabuleiro[5] == jogador &&
            tabuleiro[8] == jogador)
            return true;

        // Diagonais
        if (tabuleiro[0] == jogador &&
            tabuleiro[4] == jogador &&
            tabuleiro[8] == jogador)
            return true;

        if (tabuleiro[2] == jogador &&
            tabuleiro[4] == jogador &&
            tabuleiro[6] == jogador)
            return true;

        return false;
    }

    bool VerificarEmpate()
    {
        for (int i = 0; i < tabuleiro.Length; i++)
        {
            if (tabuleiro[i] == "")
                return false;
        }

        return true;
    }

    void FinalizarJogo(string resultado)
    {
        jogoTerminou = true;

        if (textoResultado != null)
        {
            textoResultado.text = resultado;
        }

        if (textoTurno != null)
        {
            textoTurno.text = "";
        }

        // Desativa todas as casas
        for (int i = 0; i < casas.Length; i++)
        {
            if (casas[i] != null)
                casas[i].interactable = false;
        }
    }

    void AtualizarTextoTurno()
    {
        if (textoTurno != null)
        {
            textoTurno.text = "Vez do jogador: " + jogadorAtual;
        }
    }

    public void ReiniciarJogo()
    {
        IniciarJogo();
    }
}