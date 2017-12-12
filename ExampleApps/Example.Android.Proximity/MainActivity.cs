using Android.App;
using Android.Widget;
using Android.OS;

using Android.Util;

using Estimote.Android.Cloud;
using Estimote.Android.Proximity;

namespace Example.Android.Proximity
{
    [Activity(Label = "Proximity", MainLauncher = true)]
    public class MainActivity : Activity
    {
        IProximityObserver observer;
        IProximityObserverHandler observationHandler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            // get your app ID and token on:
            // https://cloud.estimote.com/#/apps/add/your-own-app
            var creds = new EstimoteCloudCredentials("app ID", "app token");

            observer = new ProximityObserverFactory().Create(ApplicationContext, creds);

            var zone1 = observer
                .ZoneBuilder()
                .ForAttachmentKeyAndValue("beacon", "beetroot")
                .InNearRange()
                .WithOnEnterAction(new MyEnterHandler())
                .Create();

            observationHandler = observer
                .AddProximityZone(zone1)
                .WithBalancedPowerMode()
                .StartWithSimpleScanner();

            Log.Debug("app", "Proximity all ready to go!");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            observationHandler.Stop();
        }

        class MyEnterHandler : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1
        {
            public Java.Lang.Object Invoke(Java.Lang.Object p0)
            {
                Log.Debug("app", "MyEnterHandler");

                return null;
            }
        }
    }
}

