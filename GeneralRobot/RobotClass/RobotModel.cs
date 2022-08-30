using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector;
using System.Drawing;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;
using System.Reflection;

namespace GeneralRobot.RobotClass
{
    internal class RobotModel : Robot, IDrawable
    {
        Robot robot;
        Bitmap image;
        //property mauiImage
        Bitmap Image
        {
            get { return image; }
            set { image = value; }
        }

        public RobotModel()
        {
            robot = new Robot();
        }
        public async Task connection(string name, string ip, string serialNumber, string email, string password)
        {
            await robot.GrantApiAccessAsync("Vector-"+name, ip, serialNumber, email, password);
            await robot.ConnectAsync(name);
            robot.SuppressPersonalityAsync().ThrowFeedException();
            await robot.WaitTillPersonalitySuppressedAsync();
            await robot.Audio.SayTextAsync("all done");
            robot.Camera.CameraFeedAsync().ThrowFeedException();
            await Shell.Current.DisplayAlert("Connected", "You're connected", "OK");
            getCameraFeed();
        }
        void getCameraFeed()
        {
            robot.Camera.OnImageReceived += (os, oe) =>
            {
                image = (Bitmap)oe.Image;
            };
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.DrawImage((IImage)image, 0, 0, dirtyRect.Width, dirtyRect.Height);
        }
    }
}
