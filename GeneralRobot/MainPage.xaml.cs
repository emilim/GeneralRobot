using GeneralRobot.RobotClass;
using Vector;

namespace GeneralRobot;

public partial class MainPage : ContentPage
{
	RobotModel robot;

	public MainPage()
	{
		InitializeComponent();
		robot = new RobotModel();
	}
    
	private async void ConnectionClicked(object sender, EventArgs e)
	{
		await robot.connection("A1N3", "192.168.178.139", "00a15bec", "emilio.manzotti0006@gmail.com", "RealGotraBlu2006");

		if (robot.IsConnected)
            ConnectionBtn.Text = "Connected";
		else
            ConnectionBtn.Text = "Not Connected";

		SemanticScreenReader.Announce(ConnectionBtn.Text);
		graphics.Invalidate();
	}
}

