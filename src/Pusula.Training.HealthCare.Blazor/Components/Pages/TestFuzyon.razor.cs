namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class TestFuzyon
    {
        private string ButtonMessage { get; set; } = "No action yet.";

        private void OnButtonClick()
        {
            ButtonMessage = "Syncfusion Button Clicked!";
        }
    }
}