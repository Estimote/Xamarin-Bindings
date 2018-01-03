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
    interface EPXProximityDeviceAttachment
    {
        [Export("initWithDeviceIdentifier:json:")]
        IntPtr Constructor(string deviceIdentifier, NSDictionary json);

        [Export("deviceIdentifier")]
        string DeviceIdentifier { get; }

        [Export("json")]
        NSDictionary Json { get; }
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

    delegate void OnEnterAction(
        EPXProximityDeviceAttachment triggeringDeviceAttachment);

    delegate void OnExitAction(
        EPXProximityDeviceAttachment triggeringDeviceAttachment);

    delegate void OnChangeAction(NSSet attachmentsCurrentlyInside);

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
