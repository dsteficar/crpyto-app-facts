namespace WebApplicationUI.Pages.Account
{
    public partial class ChangePasswordPage
    {
        private int Step { get; set; } = 1;
        private string OldPassword { get; set; }
        private string NewPassword { get; set; }
        private string ConfirmPassword { get; set; }

        private void GoToNextStep()
        {
            if (string.IsNullOrWhiteSpace(OldPassword))
            {
                Console.WriteLine("Old password is required.");
                return;
            }
            Step = 2;
        }

        private void GoToPreviousStep()
        {
            if (Step == 2)
            {
                Step = 1;
            }
            else
            {
                navManager.NavigateTo("my-account");
            }
        }

        private void SubmitNewPassword()
        {
            if (string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Console.WriteLine("Both fields are required.");
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                Console.WriteLine("Passwords do not match.");
                return;
            }

            Console.WriteLine($"OldPassword: {OldPassword}, NewPassword: {NewPassword}");
            // Submit password change to the API
        }
    }
}
