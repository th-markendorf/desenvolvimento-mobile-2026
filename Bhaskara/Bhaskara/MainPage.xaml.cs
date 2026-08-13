namespace Bhaskara
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnCalcularRaizes_Clicked(object sender, EventArgs e)
        {

            if (A.Text == null || A.Text == "" ||
                B.Text == null || B.Text == "" ||
                C.Text == null || C.Text == "")
            {
                await DisplayAlertAsync("Aviso", "Nao sao permitidos campos vazios!", "OK");
                return; 
            }

            float a = 0.0f;
            float b = 0.0f;
            float c = 0.0f;

            try
            {
                a = Convert.ToSingle(A.Text);
                b = Convert.ToSingle(B.Text);
                c = Convert.ToSingle(C.Text);

            } catch(Exception ex)
            {
                await DisplayAlertAsync("ERRO", "Digite um numero valido!", "OK");
                return;
            }
            

            double delta = (b * b) - (4 * a * c);

            lbDelta.Text = String.Format("Delta = {0:f2}", delta);

            if (delta < 0)
            {
                lbResultado.Text = "Não existem raizes reais.";
                lbRaiz1.Text = "X1 = 0,00";
                lbRaiz2.Text = "X2 = 0,00";

            }
            else if(delta == 0) {
                double raiz = (-b) / (2 * a);
                lbResultado.Text = "Existe apenas uma raiz real:";
                lbRaiz1.Text = String.Format("X1 = {0:f2}", raiz);
                lbRaiz2.Text = String.Format("X2 = {0:f2}", 0.00);
            }
            else {
                double raiz1 = (-b + Math.Sqrt(delta)) / (2 * a);
                double raiz2 = (-b - Math.Sqrt(delta)) / (2 * a);
                lbResultado.Text = "Existem duas raizes reais:";
                lbRaiz1.Text = String.Format("X1 = {0:f2}", raiz1);
                lbRaiz2.Text = String.Format("X2 = {0:f2}", raiz2);
            }

        }
    }
}
