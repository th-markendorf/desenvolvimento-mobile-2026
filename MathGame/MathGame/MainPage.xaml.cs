#pragma warning disable CA1416

namespace MathGame
{
    public partial class MainPage : ContentPage
    {
        int iLB1 = 0;
        int iLB3 = 0;
        int iLB2 = 0;

        float fR = 0.0f;

        //acertos e erros
        int iAcertouCount = 0;
        int iErrouCount = 0;

        //pontuacoes
        int iPontuacaoCount = 0;
        int iBonusTempoCount = 0;

        //rodada atual
        int iRodadaCount = 1;

        //variaveis do timer
        IDispatcherTimer timerQuestao;
        int iTempoRestante = 30;

        Random rand = new Random();

        public MainPage()
        {
            InitializeComponent();

            // configura o Timer uma única vez no construtor
            timerQuestao = Dispatcher.CreateTimer();
            timerQuestao.Interval = TimeSpan.FromSeconds(1);
            timerQuestao.Tick += TimerQuestao_Tick;

            GerarJogo(0);
        }

        // função que roda a cada 1 segundo
        private void TimerQuestao_Tick(object sender, EventArgs e)
        {
            iTempoRestante--;
            lbTimer.Text = $"Tempo: {iTempoRestante}s";

            if (iTempoRestante <= 10)
            {
                lbTimer.TextColor = Colors.Red;
            }

            if (iTempoRestante <= 0)
            {
                timerQuestao.Stop(); // para o timer

                iErrouCount++;
                lbErrou.Text = $"Erros: {iErrouCount}";
                iRodadaCount++;
                lbRodada.Text = $"Rodada: {iRodadaCount}";

                int dificuldade = pickerDificuldade.SelectedIndex;
                if (dificuldade == -1) dificuldade = 0;

                GerarJogo(dificuldade);
            }
        }

        private async void btnOk_Clicked(object sender, EventArgs e)
        {
            // para o timer imediatamente ao clicar
            timerQuestao.Stop();

            float fResult = 0.0f;

            try
            {
                fResult = Convert.ToSingle(txR.Text);
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("ERRO", "Digite um numero valido!", "OK");
                timerQuestao.Start(); // Retoma o timer se o usuário errou o número digitado
                return;
            }

            //acertou
            if (fResult == fR)
            {
                iAcertouCount++;
                int iPontuacao = 5;

                int dificuldadeAtual = pickerDificuldade.SelectedIndex;

                switch (dificuldadeAtual)
                {
                    case 1:
                        iPontuacao += 5;
                        break;
                    case 2:
                        iPontuacao += 10;
                        break;
                }

                //bonus por tempo
                iPontuacao += iTempoRestante;
                iBonusTempoCount += iTempoRestante;

                iPontuacaoCount += iPontuacao;
                lbPontuacaoFinal.Text = $"Pontuação Final: {iPontuacaoCount}";
                lbAcertou.Text = $"Acertos: {iAcertouCount}";

                imR.Source = "win.png";
            }
            //errou
            else
            {
                iErrouCount++;
                lbErrou.Text = $"Erros: {iErrouCount}";
                imR.Source = "loose.png";
            }

            await Task.Delay(2000);
            imR.Source = "question.png";

            iRodadaCount++;
            lbRodada.Text = $"Rodada: {iRodadaCount}";

            if (iRodadaCount > 10)
            {
                await DisplayAlertAsync("Fim de Jogo!", $"Você concluiu as 10 rodadas!" +
                    $"\nPontuação Final: {iPontuacaoCount}" +
                    $"\nAcertos: {iAcertouCount}" +
                    $"\nErros: {iErrouCount}" +
                    $"\nBônus por Velocidade: {iBonusTempoCount} pts", "Jogar Novamente");

                iRodadaCount = 1;
                iAcertouCount = 0;
                iErrouCount = 0;
                iPontuacaoCount = 0;
                iBonusTempoCount = 0;

                lbAcertou.Text = $"Acertos: {iAcertouCount}";
                lbErrou.Text = $"Erros: {iErrouCount}";
                lbPontuacaoFinal.Text = $"Pontuação Final: {iPontuacaoCount}";
                lbRodada.Text = $"Rodada: {iRodadaCount}";
            }

            int dificuldade = pickerDificuldade.SelectedIndex;
            if (dificuldade == -1) dificuldade = 0;

            GerarJogo(dificuldade);
        }

        private void pickerDificuldade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dificuldade = pickerDificuldade.SelectedIndex;

            if (dificuldade != -1)
            {
                GerarJogo(dificuldade);
            }
        }

        public void GerarJogo(int nivelDificuldade)
        {
            // Mata o timer anterior de forma física e imediata
            timerQuestao.Stop();

            switch (nivelDificuldade)
            {
                case 0:
                    iLB1 = rand.Next(1, 10);
                    iLB3 = rand.Next(1, 10);
                    break;
                case 1:
                    iLB1 = rand.Next(1, 50);
                    iLB3 = rand.Next(1, 50);
                    break;
                case 2:
                    iLB1 = rand.Next(1, 100);
                    iLB3 = rand.Next(1, 100);
                    break;
            }

            iLB2 = rand.Next(1, 5);

            lb1.Text = Convert.ToString(iLB1);
            lb3.Text = Convert.ToString(iLB3);

            switch (iLB2)
            {
                case 1:
                    fR = (iLB1 + iLB3);
                    lb2.Text = "+";
                    break;
                case 2:
                    fR = (iLB1 - iLB3);
                    lb2.Text = "-";
                    break;
                case 3:
                    fR = (iLB1 * iLB3);
                    lb2.Text = "*";
                    break;
                case 4:
                    fR = (Convert.ToSingle(iLB1) / Convert.ToSingle(iLB3));
                    lb2.Text = "÷";
                    break;
            }

            txR.Text = "";

            // reseta e dispara o relogio de forma limpa
            iTempoRestante = 30;
            lbTimer.Text = "Tempo: 30s";
            lbTimer.TextColor = Colors.White;
            timerQuestao.Start();
        }
    }
}