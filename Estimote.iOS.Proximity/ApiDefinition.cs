using System;
using Foundation;

namespace Estimote.iOS.Proximity
{
    [BaseType(typeof(NSObject))]
    interface EPXCloudCredentials
    {
        [Export("initWithAppID:appToken:")]
        IntPtr Constructor(string appId, string appToken);

        [Export("appID")]
        string AppId { get; }

        [Export("appToken")]
        string AppToken { get; }
    }

    [BaseType(typeof(NSObject))]
    interface EPXDeviceAttachment
    {
        [Export("initWithDeviceIdentifier:payload:")]
        IntPtr Constructor(string deviceIdentifier, NSDictionary payload);

        [Export("deviceIdentifier")]
        string DeviceIdentifier { get; }

        [Export("payload")]
        NSDictionary Payload { get; }
    }

    delegate void ErrorHandler(NSError error);

    [BaseType(typeof(NSObject))]
    interface EPXProximityObserver
    {
        [Export("initWithCredentials:errorBlock:")]
        IntPtr Constructor(EPXCloudCredentials credentials,
                           ErrorHandler errorHandler);

        [Export("startObservingZones:")]
        void StartObservingZones(EPXProximityZone [] zones);

        [Export("stopObservingZones")]
        void StopObservingZones();
    }

    [BaseType(typeof(NSObject))]
    interface EPXProximityRange
    {
        [Export("initWithDesiredMeanTriggerDistance:")]
        IntPtr Constructor(double desiredMeanTriggerDistance);
    }

    delegate void OnEnterAction(EPXDeviceAttachment attachment);

    delegate void OnExitAction(EPXDeviceAttachment attachment);

    delegate void OnChangeAction(NSSet attachments);

    [BaseType(typeof(NSObject))]
    interface EPXProximityZone
    {
        [Export("initWithRange:attachmentKey:attachmentValue:")]
        IntPtr Constructor(EPXProximityRange range,
                           string attachmentKey, string attachmentValue);

        [Export("onEnterAction")]
        OnEnterAction OnEnterAction { get; set; }

        [Export("onExitAction")]
        OnExitAction OnExitAction { get; set; }

        [Export("onChangeAction")]
        OnChangeAction OnChangeAction { get; set; }
    }
}
