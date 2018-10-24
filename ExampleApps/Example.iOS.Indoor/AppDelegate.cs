using Foundation;
using UIKit;

using System.Diagnostics;

using Estimote.iOS.Indoor;

namespace Example.iOS.Indoor
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }



        EILIndoorLocationManager locationManager;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // get your app ID and token on:
            // https://cloud.estimote.com/#/apps/add/your-own-app
            ESTConfig.SetupAppIdAndToken("app ID", "app token");

            locationManager = new EILIndoorLocationManager();

            // C# style delegates, i.e., events & event handlers
            locationManager.DidUpdatePosition += (sender, e) =>
            {
                Debug.WriteLine($"DidUpdatePosition: {e.Position.X}, {e.Position.Y}");
            };
            locationManager.DidFailToUpdatePosition += (sender, e) =>
            {
                Debug.WriteLine($"DidFailToUpdatePosition: {e.Error}");
            };

            // For Obj-C style delegates, you'd want to have a class that derives from EILIndoorLocationManagerDelegate, implements the DidUpdatePosition and DidFailToUpdatePosition methods, and do:
            // locationManager.Delegate = myDelegateObject;
            //
            // Or, implement the DidUpdatePosition and DidFailToUpdatePosition methods on any class, and do:
            // locationManager.WeakDelegate = myDelegateObject;
            //
            // The latter is helpful in some cases, because Xamarin Delegates are implemented as classes, not interfaces, and thus are subject to the "single-class inheritance" rule.
            // 
            // For example, you can't have AppDelegate be both UIApplicationDelegate and EILIndoorLocationManagerDelegate.
            // But you can implement the EILIndoorLocationManagerDelegate methods in your AppDelegate, and assign it as a WeakDelegate.

            Debug.WriteLine("Fetching location…");
            new EILRequestFetchLocation("my-test-location").SendRequest((location, error) =>
            {
                if (location != null)
                {
                    Debug.WriteLine("Fetching location success! Starting position updates");
                    locationManager.StartPositionUpdatesForLocation(location);
                }
                else
                {
                    Debug.WriteLine($"Can't fetch location: {error}");
                }
            });

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}
