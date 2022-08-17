using System;
using System.Resources;

using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer.Widget;

using Xamarin.Forms;

// TODO: Define the default culture of your app.
// This improves lookup performance for the first resource to load.
// For more details, see https://docs.microsoft.com/dotnet/api/system.resources.neutralresourceslanguageattribute.
[assembly: NeutralResourcesLanguage("en-US")]

namespace HeartWatchface
{
    class TizenWidget : FormsWidgetBase
    {
        public override void OnCreate(Bundle content, int w, int h)
        {
            base.OnCreate(content, w, h);
            LoadApplication(new App());
        }
    }

    class TizenWidgetApplication : FormsWidgetApplication
    {
        public TizenWidgetApplication(Type type) : base(type)
        {
        }

        static void Main(string[] args)
        {
            using (var tizenWidgetApplication = new TizenWidgetApplication(typeof(TizenWidget)))
            {
                Forms.Init(tizenWidgetApplication);
                FormsCircularUI.Init();
                tizenWidgetApplication.Run(args);
            }
        }
    }
}
