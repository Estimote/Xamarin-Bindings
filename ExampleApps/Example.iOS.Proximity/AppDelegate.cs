using Foundation;
using UIKit;

using System.Diagnostics;

using Estimote.iOS.Proximity;

namespace Example.iOS.Proximity
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





        EPXProximityObserver observer;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // get your app ID and token on:
            // https://cloud.estimote.com/#/apps/add/your-own-app
            var creds = new EPXCloudCredentials("app ID", "app token");
            observer = new EPXProximityObserver(creds, (error) => {
                Debug.WriteLine($"error = {error}");
            });

            var range = new EPXProximityRange(1.0);

            var zone1 = new EPXProximityZone(range, "beacon", "beetroot");
            zone1.OnEnterAction = (triggeringDeviceAttachment) => {
                Debug.WriteLine("zone1 enter");
            };
            zone1.OnExitAction = (triggeringDeviceAttachment) => {
                Debug.WriteLine("zone1 exit");
            };

            var zone2 = new EPXProximityZone(range, "beacon", "lemon");
            zone2.OnEnterAction = (triggeringDeviceAttachment) => {
                Debug.WriteLine("zone2 enter");
            };
            zone2.OnExitAction = (triggeringDeviceAttachment) => {
                Debug.WriteLine("zone2 exit");
            };

            observer.StartObservingZones(new EPXProximityZone[] { zone1, zone2 });

            Debug.WriteLine("Proximity all ready to go!");

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

