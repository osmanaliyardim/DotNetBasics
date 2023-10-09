using HelloWorldStandard;

namespace HelloWorldMAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        SalutingStandardClass salute = new SalutingStandardClass();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            salutationLabel.Text = salute.SaluteMe("EPAM .NET Basic Mentoring");

            SemanticScreenReader.Announce(CounterBtn.Text);
            SemanticScreenReader.Announce(salutationLabel.Text);
        }
    }
}