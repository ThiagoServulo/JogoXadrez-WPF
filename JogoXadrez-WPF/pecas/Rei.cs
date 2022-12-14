namespace JogoXadrez_WPF
{
    /** ************************************************************************
    * \brief Informações sobre o rei.
    * \details A classe Rei armazena as informações referentes ao rei.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Rei : Peca
    {
        /// \brief Indica se o rei já recebu xeque.
        public bool RecebeuXeque;

        /** ************************************************************************
        * \brief Construtor da classe Rei.
        * \param tabuleiro Tabuleiro em que a peça será inserida.
        * \param corDaPeca Cor da peça.
        ***************************************************************************/
        public Rei(Tabuleiro tabuleiro, Cor corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Cor.Branco ? "rei_branco.png" : "rei_preto.png");
            RecebeuXeque = false;
        }

        /** ************************************************************************
        * \brief Verifica a possibilidade de roque.
        * \details Função responsável por verificar se existe uma torre disponível
        * para fazer a jogada especial de roque.
        * \param posicao Posição em que a torre se encontra.
        * \return 'true' - se a jogada de roque for possível, 'false' se não for
        * possível.
        ***************************************************************************/
        private bool VerificaTorreDisponivelParaRoque(Posicao posicao)
        {
            Peca peca = TabuleiroXadrez.AcessarPeca(posicao);
            return peca is Torre && peca.CorDaPeca == CorDaPeca && peca.QuantidadeDeMovimentos == 0;
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis do
        * rei.
        * \return Matriz de booleanos indicando as possíveis posições que o rei 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // direção norte (acima)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção nordeste (diagonal superior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção leste (direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção suldeste (diagonal inferior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção sul (abaixo)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção suldoeste (diagonal inferior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção oeste (esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção noroeste (diagonal superior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // #jogadaespecial roque
            if (QuantidadeDeMovimentos == 0 && !RecebeuXeque)
            {
                // #jogadaespecial roque pequeno
                Posicao posicaoTorre1 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 3);
                if (VerificaTorreDisponivelParaRoque(posicaoTorre1))
                {
                    // Verifica se as posições entre o Rei e a Torre estão vazias
                    Posicao posicao1 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    Posicao posicao2 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 2);
                    if (TabuleiroXadrez.AcessarPeca(posicao1) == null &&
                        TabuleiroXadrez.AcessarPeca(posicao2) == null)
                    {
                        matriz[PosicaoAtual.Linha, PosicaoAtual.Coluna + 2] = true;
                    }
                }

                // #jogadaespecial roque grande
                Posicao posicaoTorre2 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 4);
                if (VerificaTorreDisponivelParaRoque(posicaoTorre2))
                {
                    // Verifica se as posições entre o Rei e a Torre estão vazias
                    Posicao posicao1 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    Posicao posicao2 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 2);
                    Posicao posicao3 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 3);
                    if (TabuleiroXadrez.AcessarPeca(posicao1) == null &&
                        TabuleiroXadrez.AcessarPeca(posicao2) == null &&
                        TabuleiroXadrez.AcessarPeca(posicao3) == null)
                    {
                        matriz[PosicaoAtual.Linha, PosicaoAtual.Coluna - 2] = true;
                    }
                }
            }

            return matriz;
        }
    }
}