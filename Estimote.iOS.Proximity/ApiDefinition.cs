using System;
using Foundation;

namespace Estimote.iOS.Proximity
{
    [BaseType(typeof(NSObject), Name = "EPXCloudCredentials")]
    public interface CloudCredentials
    {
        [Export("initWithAppID:appToken:")]
        IntPtr Constructor(string appId, string appToken);

        [Export("appID")]
        string AppId { get; }

        [Export("appToken")]
        string AppToken { get; }
    }

    [BaseType(typeof(NSObject), Name = "EPXProximityZoneContext")]
    public interface ProximityZoneContext
    {
        [Export("deviceIdentifier")]
        string DeviceIdentifier { get; }

        [Export("tag")]
        string Tag { get; }

        [Export("attachments")]
        NSDictionary Attachments { get; }
    }

    public delegate void ErrorHandler(NSError error);

    [BaseType(typeof(NSObject), Name = "EPXProximityObserver")]
    public interface ProximityObserver
    {
        [Export("initWithCredentials:onError:")]
        IntPtr Constructor(CloudCredentials credentials,
                           ErrorHandler errorHandler);

        [Export("startObservingZones:")]
        void StartObservingZones(ProximityZone[] zones);

        [Export("stopObservingZones")]
        void StopObservingZones();
    }

    [BaseType(typeof(NSObject), Name = "EPXProximityRange")]
    public interface ProximityRange
    {
        [Export("initWithDesiredMeanTriggerDistance:")]
        IntPtr Constructor(double desiredMeanTriggerDistance);
    }

    public delegate void OnEnter(ProximityZoneContext context);

    public delegate void OnExit(ProximityZoneContext context);

    public delegate void OnContextChange(NSSet contexts);

    [BaseType(typeof(NSObject), Name = "EPXProximityZone")]
    public interface ProximityZone
    {
        [Export("initWithTag:range:")]
        IntPtr Constructor(string tag, ProximityRange range);

        [Export("onEnter")]
        OnEnter OnEnter { get; set; }

        [Export("onExit")]
        OnExit OnExit { get; set; }

        [Export("onContextChange")]
        OnContextChange OnContextChange { get; set; }
    }
}
