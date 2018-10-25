using Estimote.Android.Indoor;

using Android;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace Example.Android.Indoor
{
    [Activity(Label = "Indoor", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // get your app ID and token on:
        // https://cloud.estimote.com/#/apps/add/your-own-app
        private const string APP_ID = "app ID";
        private const string APP_TOKEN = "app token";

        private const string LOCATION_ID = "my-test-location";

        private bool locationUpdatesStarted = false;
        private IScanningIndoorLocationManager indoorManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        void StartLocationUpdates()
        {
            if (locationUpdatesStarted)
            {
                return;
            }
            else
            {
                locationUpdatesStarted = true;
            }

            if (indoorManager != null)
            {
                Log.Debug("app", "indoorManager already initialized, starting position updates");

                indoorManager.StartPositioning();

                return;
            }

            // starting with Android 8.0, the most reliable way to keep
            // Bluetooth scanning active when the user leaves the app is through
            // a foreground service, and for that to work we're required to show
            // a notification that informs the user about the activity
            //
            // read more about it on:
            // https://developer.android.com/guide/components/services.html#Foreground

            var channelId = "indoor_location";
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                // Android 8.0 and up require a channel for the notifications
                var channel = new NotificationChannel(channelId, "Bluetooth activity", NotificationImportance.Low);
                var notificationManager = this.GetSystemService(Context.NotificationService) as NotificationManager;
                notificationManager.CreateNotificationChannel(channel);
            }
            var notification = new NotificationCompat.Builder(this, channelId)
                    .SetSmallIcon(global::Android.Resource.Drawable.IcDialogInfo)
                    .SetContentTitle("Indoor Location")
                    .SetContentText("Indoor Location updates are running")
                    .Build();

            var creds = new EstimoteCloudCredentials(APP_ID, APP_TOKEN);

            var getLocationHandler = new GetLocationHandler();
            getLocationHandler.GetLocationSuccess += (location) =>
            {
                Log.Debug("app", $"Successfully fetched location from Estimote Cloud: {location}");
                Log.Debug("app", "Initializing indoorManager and starting position updates");

                indoorManager = new IndoorLocationManagerBuilder(this, location, creds)

                    // see the longer comment above about the foreground service and
                    // the notification
                    //
                    // if you only intend to use beacons while the app is open, you
                    // can safely remove this line
                    .WithScannerInForegroundService(notification)

                    .Build();

                indoorManager.SetOnPositionUpdateListener(new PositionUpdateHandler());
                indoorManager.StartPositioning();
            };
            getLocationHandler.GetLocationFailure += (error) =>
            {
                Log.Error("app", $"Failed to fetch the location from Estimote Cloud: {error}");
            };

            Log.Debug("app", $"Fetching location '{LOCATION_ID}' from Estimote Cloud...");
            new IndoorCloudManagerFactory()
                .Create(this, creds)
                .GetLocation(LOCATION_ID, getLocationHandler);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (indoorManager != null && locationUpdatesStarted)
            {
                locationUpdatesStarted = false;

                indoorManager.StopPositioning();
            }
        }

        class GetLocationHandler : Java.Lang.Object, ICloudCallback
        {
            public delegate void OnGetLocationSuccess(Location location);
            public event OnGetLocationSuccess GetLocationSuccess;

            public delegate void OnGetLocationFailure(EstimoteCloudException error);
            public event OnGetLocationFailure GetLocationFailure;

            public void Failure(EstimoteCloudException error)
            {
                GetLocationFailure(error);
            }

            public void Success(Java.Lang.Object location)
            {
                GetLocationSuccess((Location)location);
            }
        }

        class PositionUpdateHandler : Java.Lang.Object, IOnPositionUpdateListener
        {
            public void OnPositionOutsideLocation()
            {
                Log.Debug("app", "OnPositionOutsideLocation");
            }

            public void OnPositionUpdate(LocationPosition position)
            {
                Log.Debug("app", $"OnPositionUpdate: {position}");
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

                StartLocationUpdates();
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

                            StartLocationUpdates();
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
