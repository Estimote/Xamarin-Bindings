using System;
using Foundation;

namespace Estimote.iOS.Proximity
{
    [BaseType(typeof(NSObject))]
    interface ESTCloudCredentials
    {
        [Export("initWithAppID:appToken:")]
        IntPtr Constructor(string appId, string appToken);

        [Export("appID")]
        string AppId { get; }

        [Export("appToken")]
        string AppToken { get; }
    }

    [BaseType(typeof(NSObject))]
    interface ESTProximityDeviceAttachment
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
    interface ESTProximityObserver
    {
        [Export("initWithCredentials:errorBlock:")]
        IntPtr Constructor(ESTCloudCredentials credentials,
                           ErrorHandler errorHandler);

        [Export("startObservingZones:")]
        void StartObservingZones(ESTProximityZone [] zones);

        [Export("stopObservingZones")]
        void StopObservingZones();
    }

    [BaseType(typeof(NSObject))]
    interface ESTProximityRange
    {
        [Export("initWithDesiredMeanTriggerDistance:")]
        IntPtr Constructor(double desiredMeanTriggerDistance);
    }

    delegate void OnEnterAction(
        ESTProximityDeviceAttachment triggeringDeviceAttachment);

    delegate void OnExitAction(
        ESTProximityDeviceAttachment triggeringDeviceAttachment);

    delegate void OnChangeAction(NSSet attachmentsCurrentlyInside);

    [BaseType(typeof(NSObject))]
    interface ESTProximityZone
    {
        [Export("initWithRange:attachmentKey:attachmentValue:")]
        IntPtr Constructor(ESTProximityRange range,
                           string attachmentKey, string attachmentValue);

        [Export("onEnterAction")]
        OnEnterAction OnEnterAction { get; set; }

        [Export("onExitAction")]
        OnExitAction OnExitAction { get; set; }

        [Export("onChangeAction")]
        OnChangeAction OnChangeAction { get; set; }
    }
}
