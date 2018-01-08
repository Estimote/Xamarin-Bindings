using Android.App;
using Android.Widget;
using Android.OS;

using Android.Util;

using Estimote.Android.Cloud;
using Estimote.Android.Proximity;

using Android;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace Example.Android.Proximity
{
    [Activity(Label = "Proximity", MainLauncher = true)]
    public class MainActivity : Activity
    {
        IProximityObserver observer;
        IProximityObserverHandler observationHandler;

        Notification notification;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // get your app ID and token on:
            // https://cloud.estimote.com/#/apps/add/your-own-app
            var creds = new EstimoteCloudCredentials("app ID", "app token");

            // starting with Android 8.0, the most reliable way to keep
            // Bluetooth scanning active is through a foreground service, and
            // for that to work we're required to show a notification that
            // informs the user about the activity
            //
            // read more about it on:
            // https://developer.android.com/guide/components/services.html#Foreground

            var channelId = "proximity_scanning";
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                // Android 8.0 and up require a channel for the notifications
                var channel = new NotificationChannel(channelId, "Bluetooth activity", NotificationImportance.Low);
                var notificationManager = this.GetSystemService(Context.NotificationService) as NotificationManager;
                notificationManager.CreateNotificationChannel(channel);
            }
            notification = new NotificationCompat.Builder(this, channelId)
                    .SetSmallIcon(Resource.Drawable.notification_icon_background)
                    .SetContentTitle("Proximity")
                    .SetContentText("Proximity demo is scanning for beacons")
                    .Build();

            observer = new ProximityObserverBuilder(ApplicationContext, creds)
                .WithBalancedPowerMode()
                .WithTelemetryReporting()
                .WithScannerInForegroundService(notification)
                .WithOnErrorAction(new MyErrorHandler())
                .Build();

            var zone1 = observer
                .ZoneBuilder()
                .ForAttachmentKeyAndValue("beacon", "beetroot")
                .InNearRange()
                .WithOnEnterAction(new MyEnterHandler())
                .Create();
            observer.AddProximityZone(zone1);

            // the actual observation starts further below in OnResume or
            // OnRequestPermissionsResult, once we obtain the location
            // permission from the user, or confirm that we already have it

            Log.Debug("app", "Proximity all ready to go!");
        }

        void startProximityObservation()
        {
            if (observationHandler != null)
            {
                // already observing!
                return;
            }

            Log.Debug("app", "Starting proximity observation");

            observationHandler = observer.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (observationHandler != null)
            {
                observationHandler.Stop();
            }
        }

        class MyEnterHandler : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1
        {
            public Java.Lang.Object Invoke(Java.Lang.Object p0)
            {
                Log.Debug("app", "MyEnterHandler");

                return null;
            }
        }

        class MyErrorHandler : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1
        {
            public Java.Lang.Object Invoke(Java.Lang.Object p0)
            {
                Log.Debug("app", "MyErrorHandler");
                Log.Debug("app", p0.ToString());

                return null;
            }
        }

        // check and request permission to access location data
        //
        // code ported from:
        // https://developer.android.com/training/permissions/requesting.html

        private const int MY_PERMISSIONS_REQUEST_COARSE_LOCATION = 1;

        protected override void OnResume()
        {
            base.OnResume();

            if (ContextCompat.CheckSelfPermission(
                this, Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(
                    this, Manifest.Permission.AccessCoarseLocation))
                {
                    Log.Debug("app", "ShouldShowRequestPermissionRationale");

                    // we should show an extra dialog here to explain why we
                    // need the permission, and then follow up with an actual
                    // call to RequestPermissions ... but in this example, let's
                    // just call RequestPermissions straight away
                }

                ActivityCompat.RequestPermissions(
                        this,
                        new string[] { Manifest.Permission.AccessCoarseLocation },
                        MY_PERMISSIONS_REQUEST_COARSE_LOCATION);
            }
            else
            {
                Log.Debug("app", "Already have the location permission");

                startProximityObservation();
            }
        }

        public override void OnRequestPermissionsResult(
            int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case MY_PERMISSIONS_REQUEST_COARSE_LOCATION:
                    {
                        if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                        {
                            Log.Debug("app", "Location permission granted");

                            startProximityObservation();
                        }
                        else
                        {
                            Log.Debug("app", "Location permission denied");
                        }
                        return;
                    }
            }
        }
    }
}

