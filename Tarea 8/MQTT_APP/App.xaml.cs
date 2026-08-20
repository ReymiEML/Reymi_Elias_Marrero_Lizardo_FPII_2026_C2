using Microsoft.Extensions.DependencyInjection;

namespace MQTT_APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

#if WINDOWS
            window.Width = 480;
            window.Height = 850;

            window.Created += (_, _) =>
            {
                var nativeWindow = window.Handler.PlatformView as Microsoft.UI.Xaml.Window;

                if (nativeWindow?.AppWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
                {
                    presenter.IsResizable = false;
                    presenter.IsMaximizable = false;
                }
            };
#endif

            return window;
        }
    }
}