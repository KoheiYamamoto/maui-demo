using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace maui_demo;

public partial class MainPage : ContentPage
{
	int count = 0;

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

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void OnAuthClicked(object sender, EventArgs e)
    {
		if (await CrossFingerprint.Current.IsAvailableAsync(true))
		{
			var request = new AuthenticationRequestConfiguration("Login!", "Access your account");
			var result = await CrossFingerprint.Current.AuthenticateAsync(request);
			if (result.Authenticated)
			{
				await DisplayAlert("Authenticated!", "Access granted", "OK");
			}
			else
			{
				await DisplayAlert("Not authenticated!", "Access denied", "OK");
			}
		}
        else
        {
            await DisplayAlert("Failure", "Biometrics not available", "OK");
        }
    }
}

