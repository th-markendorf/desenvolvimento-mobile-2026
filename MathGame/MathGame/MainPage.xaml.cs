namespace MathGame
{
    public partial class MainPage : ContentPage
    {
        int iLB1 = 0;
        int iLB3 = 0;
        int iLB2 = 0;

        float fR = 0.0f;

        int iAcertouCount = 0;
        int iErrouCount = 0;

        Random rand = new Random();



        public MainPage()
        {
            InitializeComponent();

            GerarJogo(0); // iniciar o jogo primeira vez apos inicializar os componentes
        }

        private async void btnOk_Clicked(object sender, EventArgs e)
        {

            float fResult = 0.0f;

            try
            {
                fResult = Convert.ToSingle(txR.Text);
            } catch (Exception ex){

                await DisplayAlertAsync("ERRO", "Digite um numero valido!", "OK");
                return;
            }

            if (fResult == fR)
            {
                iAcertouCount++;

                lbAcertou.Text = $"Acertos: {iAcertouCount}";

                imR.Source = "win.png";

            }
            else
            {
                iErrouCount++;

                lbErrou.Text = $"Erros: {iErrouCount}";

                imR.Source = "loose.png";
            }

            //aguarda 2 segundos para o usuario  vizualizar o resultado
            await Task.Delay(2000);
            imR.Source = "question.png";


            //pega a dificuldade para passar no parametro da funcao
            int dificuldade = pickerDificuldade.SelectedIndex;

            //atribui dificuldade 1 se nenhuma for selecionada
            if(dificuldade == -1)
            {
                dificuldade = 0;
            }

            //gera um novo jogo
            GerarJogo(dificuldade);
        }


        private void pickerDificuldade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dificuldade = pickerDificuldade.SelectedIndex;

            if  (dificuldade != -1)
            {
                GerarJogo(dificuldade);
            }
        }

        public void GerarJogo(int nivelDificuldade)
        {
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
        }
    }
}
