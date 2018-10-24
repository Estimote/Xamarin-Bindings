using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using ARKit;
using SpriteKit;

#pragma warning disable IDE1006

namespace Estimote.iOS.Indoor
{
    [BaseType(typeof(NSObject))]
    public interface ESTConfig
    {
        [Static]
        [Export("setupAppID:andAppToken:")]
        void SetupAppIdAndToken(string appId, string appToken);
    }

    // @protocol EILIndoorLocationManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    public interface EILIndoorLocationManagerDelegate
    {
        // @optional -(void)indoorLocationManager:(EILIndoorLocationManager * _Nonnull)manager didUpdatePosition:(EILOrientedPoint * _Nonnull)position withAccuracy:(EILPositionAccuracy)positionAccuracy inLocation:(EILLocation * _Nonnull)location;
        [Export("indoorLocationManager:didUpdatePosition:withAccuracy:inLocation:"), EventArgs("DidUpdatePosition")]
        void DidUpdatePosition(EILIndoorLocationManager manager, EILOrientedPoint position, EILPositionAccuracy positionAccuracy, EILLocation location);

        // @optional -(void)indoorLocationManager:(EILIndoorLocationManager * _Nonnull)manager didFailToUpdatePositionWithError:(NSError * _Nonnull)error;
        [Export("indoorLocationManager:didFailToUpdatePositionWithError:"), EventArgs("DidFailToUpdatePosition")]
        void DidFailToUpdatePosition(EILIndoorLocationManager manager, NSError error);
    }

    // @interface EILIndoorLocationManager : NSObject
    [BaseType(typeof(NSObject),
              Delegates = new string[] { "WeakDelegate" },
              Events = new Type[] { typeof(EILIndoorLocationManagerDelegate) })]
    public interface EILIndoorLocationManager
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        EILIndoorLocationManagerDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<EILIndoorLocationManagerDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // -(void)start;
        [Export("start")]
        void Start();

        // -(void)stop;
        [Export("stop")]
        void Stop();

        // @property (readonly, assign, nonatomic) BOOL isRunning;
        [Export("isRunning")]
        bool IsRunning { get; }

        // -(void)startMonitoringForLocation:(EILLocation * _Nonnull)location;
        [Export("startMonitoringForLocation:")]
        void StartMonitoringForLocation(EILLocation location);

        // -(void)stopMonitoringForLocation:(EILLocation * _Nonnull)location;
        [Export("stopMonitoringForLocation:")]
        void StopMonitoringForLocation(EILLocation location);

        // @property (readonly, nonatomic, strong) NSSet<EILLocation *> * _Nonnull monitoredLocations;
        [Export("monitoredLocations", ArgumentSemantic.Strong)]
        NSSet<EILLocation> MonitoredLocations { get; }

        // -(EILLocationState)stateForLocation:(EILLocation * _Nonnull)location;
        [Export("stateForLocation:")]
        EILLocationState StateForLocation(EILLocation location);

        // -(EILLocationState)stateForLocationWithIdentifier:(NSString * _Nonnull)locationIdentifier;
        [Export("stateForLocationWithIdentifier:")]
        EILLocationState StateForLocationWithIdentifier(string locationIdentifier);

        // @property (assign, nonatomic) EILIndoorLocationManagerMode mode;
        [Export("mode", ArgumentSemantic.Assign)]
        EILIndoorLocationManagerMode Mode { get; set; }

        // @property (assign, nonatomic) BOOL provideOrientationForLightMode;
        [Export("provideOrientationForLightMode")]
        bool ProvideOrientationForLightMode { get; set; }

        // +(NSArray<NSNumber *> * _Nonnull)supportedModes;
        [Static]
        [Export("supportedModes")]
        NSNumber[] SupportedModes { get; }

        // -(void)startPositionUpdatesForLocation:(EILLocation * _Nonnull)location;
        [Export("startPositionUpdatesForLocation:")]
        void StartPositionUpdatesForLocation(EILLocation location);

        // -(void)stopPositionUpdates;
        [Export("stopPositionUpdates")]
        void StopPositionUpdates();

        // @property (readonly, nonatomic, strong) ARSession * _Nonnull arSession __attribute__((availability(ios, introduced=11_0)));
        [Introduced(PlatformName.iOS, 11, 0)]
        [Export("arSession", ArgumentSemantic.Strong)]
        ARSession ArSession { get; }

        // -(EILPoint * _Nonnull)indoorLocationPointFromARKitPoint:(EILPoint * _Nonnull)arkitPoint __attribute__((availability(ios, introduced=11_0)));
        [Introduced(PlatformName.iOS, 11, 0)]
        [Export("indoorLocationPointFromARKitPoint:")]
        EILPoint IndoorLocationPointFromARKitPoint(EILPoint arkitPoint);

        // -(EILPoint * _Nonnull)ARKitPointFromIndoorLocationPoint:(EILPoint * _Nonnull)indoorLocationPoint __attribute__((availability(ios, introduced=11_0)));
        [Introduced(PlatformName.iOS, 11, 0)]
        [Export("ARKitPointFromIndoorLocationPoint:")]
        EILPoint ARKitPointFromIndoorLocationPoint(EILPoint indoorLocationPoint);
    }

    // @protocol EILBackgroundIndoorLocationManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    public interface EILBackgroundIndoorLocationManagerDelegate
    {
        // @optional -(void)backgroundIndoorLocationManager:(EILBackgroundIndoorLocationManager * _Nonnull)locationManager didUpdatePosition:(EILOrientedPoint * _Nonnull)position withAccuracy:(EILPositionAccuracy)positionAccuracy inLocation:(EILLocation * _Nonnull)location;
        [Export("backgroundIndoorLocationManager:didUpdatePosition:withAccuracy:inLocation:"), EventArgs("DidUpdatePosition")]
        void DidUpdatePosition(EILBackgroundIndoorLocationManager locationManager, EILOrientedPoint position, EILPositionAccuracy positionAccuracy, EILLocation location);

        // @optional -(void)backgroundIndoorLocationManager:(EILBackgroundIndoorLocationManager * _Nonnull)locationManager didFailToUpdatePositionWithError:(NSError * _Nonnull)error;
        [Export("backgroundIndoorLocationManager:didFailToUpdatePositionWithError:"), EventArgs("DidFailToUpdatePosition")]
        void DidFailToUpdatePosition(EILBackgroundIndoorLocationManager locationManager, NSError error);
    }

    // @interface EILBackgroundIndoorLocationManager : NSObject
    [BaseType(typeof(NSObject),
              Delegates = new string[] { "WeakDelegate" },
              Events = new Type[] { typeof(EILBackgroundIndoorLocationManagerDelegate) })]
    public interface EILBackgroundIndoorLocationManager
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        EILBackgroundIndoorLocationManagerDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<EILBackgroundIndoorLocationManagerDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // -(void)requestAlwaysAuthorization;
        [Export("requestAlwaysAuthorization")]
        void RequestAlwaysAuthorization();

        // -(void)start;
        [Export("start")]
        void Start();

        // -(void)stop;
        [Export("stop")]
        void Stop();

        // @property (readonly, assign, nonatomic) BOOL isRunning;
        [Export("isRunning")]
        bool IsRunning { get; }

        // -(void)startMonitoringForLocation:(EILLocation * _Nonnull)location;
        [Export("startMonitoringForLocation:")]
        void StartMonitoringForLocation(EILLocation location);

        // -(void)stopMonitoringForLocation:(EILLocation * _Nonnull)location;
        [Export("stopMonitoringForLocation:")]
        void StopMonitoringForLocation(EILLocation location);

        // @property (readonly, nonatomic, strong) NSSet<EILLocation *> * _Nonnull monitoredLocations;
        [Export("monitoredLocations", ArgumentSemantic.Strong)]
        NSSet<EILLocation> MonitoredLocations { get; }

        // -(EILLocationState)stateForLocation:(EILLocation * _Nonnull)location;
        [Export("stateForLocation:")]
        EILLocationState StateForLocation(EILLocation location);

        // -(EILLocationState)stateForLocationWithIdentifier:(NSString * _Nonnull)locationIdentifier;
        [Export("stateForLocationWithIdentifier:")]
        EILLocationState StateForLocationWithIdentifier(string locationIdentifier);

        // -(void)startPositionUpdatesForLocation:(EILLocation * _Nonnull)location;
        [Export("startPositionUpdatesForLocation:")]
        void StartPositionUpdatesForLocation(EILLocation location);

        // -(void)stopPositionUpdatesForLocation:(EILLocation * _Nonnull)location;
        [Export("stopPositionUpdatesForLocation:")]
        void StopPositionUpdatesForLocation(EILLocation location);

        // -(void)stopPositionUpdates;
        [Export("stopPositionUpdates")]
        void StopPositionUpdates();
    }

    // @interface EILIndoorLocationScene : SKScene
    [BaseType(typeof(SKScene))]
    public interface EILIndoorLocationScene
    {
        // @property (readonly, nonatomic, strong) EILLocation * _Nullable location;
        [NullAllowed, Export("location", ArgumentSemantic.Strong)]
        EILLocation Location { get; }

        // @property (assign, nonatomic) BOOL showBeacons;
        [Export("showBeacons")]
        bool ShowBeacons { get; set; }

        // @property (assign, nonatomic) BOOL showTrace;
        [Export("showTrace")]
        bool ShowTrace { get; set; }

        // @property (assign, nonatomic) BOOL rotateLocationOnPositionUpdate;
        [Export("rotateLocationOnPositionUpdate")]
        bool RotateLocationOnPositionUpdate { get; set; }

        // @property (readonly, assign, nonatomic) CGFloat locationScale;
        [Export("locationScale")]
        nfloat LocationScale { get; }

        // @property (assign, nonatomic) double moveAnimationDuration;
        [Export("moveAnimationDuration")]
        double MoveAnimationDuration { get; set; }

        // @property (copy, nonatomic) void (^ _Nullable)(NSString * _Nonnull) touchHandler;
        [NullAllowed, Export("touchHandler", ArgumentSemantic.Copy)]
        Action<NSString> TouchHandler { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable avatarImageName;
        [NullAllowed, Export("avatarImageName", ArgumentSemantic.Strong)]
        string AvatarImageName { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable backgroundImageName;
        [NullAllowed, Export("backgroundImageName", ArgumentSemantic.Strong)]
        string BackgroundImageName { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable centerUserButtonImageName;
        [NullAllowed, Export("centerUserButtonImageName", ArgumentSemantic.Strong)]
        string CenterUserButtonImageName { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable compassButtonImageName;
        [NullAllowed, Export("compassButtonImageName", ArgumentSemantic.Strong)]
        string CompassButtonImageName { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull locationBorderColor;
        [Export("locationBorderColor", ArgumentSemantic.Strong)]
        UIColor LocationBorderColor { get; set; }

        // @property (assign, nonatomic) CGFloat locationBorderThickness;
        [Export("locationBorderThickness")]
        nfloat LocationBorderThickness { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull windowColor;
        [Export("windowColor", ArgumentSemantic.Strong)]
        UIColor WindowColor { get; set; }

        // @property (assign, nonatomic) CGFloat windowThickness;
        [Export("windowThickness")]
        nfloat WindowThickness { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull doorColor;
        [Export("doorColor", ArgumentSemantic.Strong)]
        UIColor DoorColor { get; set; }

        // @property (assign, nonatomic) CGFloat doorThickness;
        [Export("doorThickness")]
        nfloat DoorThickness { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull traceColor;
        [Export("traceColor", ArgumentSemantic.Strong)]
        UIColor TraceColor { get; set; }

        // @property (assign, nonatomic) CGFloat traceThickness;
        [Export("traceThickness")]
        nfloat TraceThickness { get; set; }

        // @property (assign, nonatomic) CGSize beaconSize;
        [Export("beaconSize", ArgumentSemantic.Assign)]
        CGSize BeaconSize { get; set; }

        // -(void)drawLocation:(EILLocation * _Nonnull)location;
        [Export("drawLocation:")]
        void DrawLocation(EILLocation location);

        // -(void)updateUserPosition:(EILOrientedPoint * _Nullable)position withAccuracy:(CGFloat)accuracy;
        [Export("updateUserPosition:withAccuracy:")]
        void UpdateUserPosition([NullAllowed] EILOrientedPoint position, nfloat accuracy);

        // -(void)updateUserPosition:(EILOrientedPoint * _Nullable)position;
        [Export("updateUserPosition:")]
        void UpdateUserPosition([NullAllowed] EILOrientedPoint position);

        // -(void)centerOnUser;
        [Export("centerOnUser")]
        void CenterOnUser();

        // -(void)clearTrace;
        [Export("clearTrace")]
        void ClearTrace();
    }

    // @protocol EILIndoorLocationViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    public interface EILIndoorLocationViewDelegate
    {
        // @optional -(void)indoorLocationView:(EILIndoorLocationView * _Nonnull)indoorLocationView didSelectObjectWithIdentifier:(NSString * _Nonnull)identifier;
        [Export("indoorLocationView:didSelectObjectWithIdentifier:")]
        void DidSelectObject(EILIndoorLocationView indoorLocationView, string identifier);
    }

    // @interface EILIndoorLocationView : UIView
    [BaseType(typeof(UIView))]
    public interface EILIndoorLocationView
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        EILIndoorLocationViewDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<EILIndoorLocationViewDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, nonatomic, strong) EILLocation * _Nullable location;
        [NullAllowed, Export("location", ArgumentSemantic.Strong)]
        EILLocation Location { get; }

        // @property (assign, nonatomic) BOOL showTrace;
        [Export("showTrace")]
        bool ShowTrace { get; set; }

        // @property (assign, nonatomic) BOOL rotateOnPositionUpdate;
        [Export("rotateOnPositionUpdate")]
        bool RotateOnPositionUpdate { get; set; }

        // @property (readonly, assign, nonatomic) BOOL locationDrawn;
        [Export("locationDrawn")]
        bool LocationDrawn { get; }

        // @property (nonatomic, strong) UIView * _Nullable positionView;
        [NullAllowed, Export("positionView", ArgumentSemantic.Strong)]
        UIView PositionView { get; set; }

        // @property (nonatomic, strong) UIImage * _Nullable positionImage;
        [NullAllowed, Export("positionImage", ArgumentSemantic.Strong)]
        UIImage PositionImage { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull locationBorderColor;
        [Export("locationBorderColor", ArgumentSemantic.Strong)]
        UIColor LocationBorderColor { get; set; }

        // @property (assign, nonatomic) NSInteger locationBorderThickness;
        [Export("locationBorderThickness")]
        nint LocationBorderThickness { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull doorColor;
        [Export("doorColor", ArgumentSemantic.Strong)]
        UIColor DoorColor { get; set; }

        // @property (assign, nonatomic) NSInteger doorThickness;
        [Export("doorThickness")]
        nint DoorThickness { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull windowColor;
        [Export("windowColor", ArgumentSemantic.Strong)]
        UIColor WindowColor { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull windowBackgroundColor;
        [Export("windowBackgroundColor", ArgumentSemantic.Strong)]
        UIColor WindowBackgroundColor { get; set; }

        // @property (assign, nonatomic) NSInteger windowThickness;
        [Export("windowThickness")]
        nint WindowThickness { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull traceColor;
        [Export("traceColor", ArgumentSemantic.Strong)]
        UIColor TraceColor { get; set; }

        // @property (assign, nonatomic) NSInteger traceThickness;
        [Export("traceThickness")]
        nint TraceThickness { get; set; }

        // @property (nonatomic, strong) UIColor * _Nonnull wallLengthLabelsColor;
        [Export("wallLengthLabelsColor", ArgumentSemantic.Strong)]
        UIColor WallLengthLabelsColor { get; set; }

        // @property (assign, nonatomic) BOOL showWallLengthLabels;
        [Export("showWallLengthLabels")]
        bool ShowWallLengthLabels { get; set; }

        // @property (assign, nonatomic) NSInteger wallLengthLabelFontSize;
        [Export("wallLengthLabelFontSize")]
        nint WallLengthLabelFontSize { get; set; }

        // @property (assign, nonatomic) BOOL showBeacons;
        [Export("showBeacons")]
        bool ShowBeacons { get; set; }

        // -(void)drawLocation:(EILLocation * _Nonnull)location;
        [Export("drawLocation:")]
        void DrawLocation(EILLocation location);

        // -(void)drawLocation:(EILLocation * _Nonnull)location inRegionOfInterest:(CGRect)regionOfInterest;
        [Export("drawLocation:inRegionOfInterest:")]
        void DrawLocation(EILLocation location, CGRect regionOfInterest);

        // -(void)updatePosition:(EILOrientedPoint * _Nullable)position;
        [Export("updatePosition:")]
        void UpdatePosition([NullAllowed] EILOrientedPoint position);

        // -(void)clearTrace;
        [Export("clearTrace")]
        void ClearTrace();

        // -(void)drawObjectInBackground:(UIView * _Nonnull)object withPosition:(EILOrientedPoint * _Nonnull)position identifier:(NSString * _Nonnull)identifier;
        [Export("drawObjectInBackground:withPosition:identifier:")]
        void DrawObjectInBackground(UIView @object, EILOrientedPoint position, string identifier);

        // -(void)drawObjectInForeground:(UIView * _Nonnull)object withPosition:(EILOrientedPoint * _Nonnull)position identifier:(NSString * _Nonnull)identifier;
        [Export("drawObjectInForeground:withPosition:identifier:")]
        void DrawObjectInForeground(UIView @object, EILOrientedPoint position, string identifier);

        // -(UIView * _Nullable)objectWithidentifier:(NSString * _Nonnull)identifier;
        [Export("objectWithidentifier:")]
        [return: NullAllowed]
        UIView ObjectWithIdentifier(string identifier);

        // -(void)moveObjectWithIdentifier:(NSString * _Nonnull)identifier toPosition:(EILOrientedPoint * _Nonnull)position animated:(BOOL)animated;
        [Export("moveObjectWithIdentifier:toPosition:animated:")]
        void MoveObjectWithIdentifier(string identifier, EILOrientedPoint position, bool animated);

        // -(void)removeObjectWithIdentifier:(NSString * _Nonnull)identifier;
        [Export("removeObjectWithIdentifier:")]
        void RemoveObjectWithIdentifier(string identifier);

        // -(CGFloat)calculatePictureCoordinateForRealX:(double)realX;
        [Export("calculatePictureCoordinateForRealX:")]
        nfloat CalculatePictureCoordinateForRealX(double realX);

        // -(CGFloat)calculatePictureCoordinateForRealY:(double)realY;
        [Export("calculatePictureCoordinateForRealY:")]
        nfloat CalculatePictureCoordinateForRealY(double realY);

        // -(CGPoint)calculatePicturePointFromRealPoint:(EILPoint * _Nonnull)realPoint;
        [Export("calculatePicturePointFromRealPoint:")]
        CGPoint CalculatePicturePointFromRealPoint(EILPoint realPoint);

        // -(double)calculateRealCoordinateForPictureX:(CGFloat)pictureX;
        [Export("calculateRealCoordinateForPictureX:")]
        double CalculateRealCoordinateForPictureX(nfloat pictureX);

        // -(double)calculateRealCoordinateForPictureY:(CGFloat)pictureY;
        [Export("calculateRealCoordinateForPictureY:")]
        double CalculateRealCoordinateForPictureY(nfloat pictureY);

        // -(EILPoint * _Nonnull)calculateRealPointFromPicturePoint:(CGPoint)picturePoint;
        [Export("calculateRealPointFromPicturePoint:")]
        EILPoint CalculateRealPointFromPicturePoint(CGPoint picturePoint);
    }

    // @interface EILPoint : NSObject <NSCoding, NSCopying>
    [BaseType(typeof(NSObject))]
    public interface EILPoint : INSCoding, INSCopying
    {
        // @property (readonly, assign, nonatomic) double x;
        [Export("x")]
        double X { get; }

        // @property (readonly, assign, nonatomic) double y;
        [Export("y")]
        double Y { get; }

        // -(instancetype _Nonnull)initWithX:(double)x y:(double)y;
        [Export("initWithX:y:")]
        IntPtr Constructor(double x, double y);

        // -(instancetype _Nonnull)pointTranslatedBydX:(double)dX dY:(double)dY;
        [Export("pointTranslatedBydX:dY:")]
        EILPoint PointTranslatedBy(double dX, double dY);

        // -(double)distanceToPoint:(EILPoint * _Nonnull)point;
        [Export("distanceToPoint:")]
        double DistanceToPoint(EILPoint point);

        // -(double)length;
        [Export("length")]
        double Length { get; }
    }

    // @interface EILOrientedPoint : EILPoint <NSCoding>
    [BaseType(typeof(EILPoint))]
    public interface EILOrientedPoint : INSCoding
    {
        // @property (assign, nonatomic) double orientation;
        [Export("orientation")]
        double Orientation { get; set; }

        // -(instancetype _Nonnull)initWithX:(double)x y:(double)y orientation:(double)orientation;
        [Export("initWithX:y:orientation:")]
        IntPtr Constructor(double x, double y, double orientation);

        // -(instancetype _Nonnull)initWithX:(double)x y:(double)y;
        [Export("initWithX:y:")]
        IntPtr Constructor(double x, double y);

        // -(instancetype _Nonnull)pointTranslatedBydX:(double)dX dY:(double)dY;
        [Export("pointTranslatedBydX:dY:")]
        EILOrientedPoint PointTranslatedBy(double dX, double dY);
    }

    // @interface EILOrientedLineSegment : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    public interface EILOrientedLineSegment : INSCoding
    {
        // @property (readonly, nonatomic, strong) EILPoint * _Nonnull point1;
        [Export("point1", ArgumentSemantic.Strong)]
        EILPoint Point1 { get; }

        // @property (readonly, nonatomic, strong) EILPoint * _Nonnull point2;
        [Export("point2", ArgumentSemantic.Strong)]
        EILPoint Point2 { get; }

        // @property (readonly, assign, nonatomic) double orientation;
        [Export("orientation")]
        double Orientation { get; }

        // -(instancetype _Nonnull)initWithPoint1:(EILPoint * _Nonnull)point1 point2:(EILPoint * _Nonnull)point2 orientation:(double)orientation;
        [Export("initWithPoint1:point2:orientation:")]
        IntPtr Constructor(EILPoint point1, EILPoint point2, double orientation);

        // -(EILOrientedPoint * _Nonnull)centerPoint;
        [Export("centerPoint")]
        EILOrientedPoint CenterPoint { get; }

        // -(EILOrientedPoint * _Nonnull)leftPoint;
        [Export("leftPoint")]
        EILOrientedPoint LeftPoint { get; }

        // -(EILOrientedPoint * _Nonnull)rightPoint;
        [Export("rightPoint")]
        EILOrientedPoint RightPoint { get; }

        // -(double)length;
        [Export("length")]
        double Length { get; }

        // -(EILOrientedLineSegment * _Nonnull)lineTranslatedBydX:(double)dX dY:(double)dY;
        [Export("lineTranslatedBydX:dY:")]
        EILOrientedLineSegment LineTranslatedBy(double dX, double dY);
    }

    // @interface EILLocationLinearObject : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    public interface EILLocationLinearObject : INSCoding
    {
        // @property (readonly, assign, nonatomic) EILLocationLinearObjectType type;
        [Export("type", ArgumentSemantic.Assign)]
        EILLocationLinearObjectType Type { get; }

        // @property (readonly, nonatomic, strong) EILOrientedLineSegment * _Nonnull position;
        [Export("position", ArgumentSemantic.Strong)]
        EILOrientedLineSegment Position { get; }

        // -(instancetype _Nonnull)initWithType:(EILLocationLinearObjectType)type position:(EILOrientedLineSegment * _Nonnull)position;
        [Export("initWithType:position:")]
        IntPtr Constructor(EILLocationLinearObjectType type, EILOrientedLineSegment position);

        // -(instancetype _Nonnull)linearObjectTranslatedByDX:(double)dX dY:(double)dY;
        [Export("linearObjectTranslatedByDX:dY:")]
        EILLocationLinearObject LinearObjectTranslatedBy(double dX, double dY);
    }

    // @interface EILPositionedBeacon : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    public interface EILPositionedBeacon : INSCoding
    {
        // @property (readonly, nonatomic, strong) EILOrientedPoint * _Nonnull position;
        [Export("position", ArgumentSemantic.Strong)]
        EILOrientedPoint Position { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
        [Export("identifier", ArgumentSemantic.Strong)]
        string Identifier { get; }

        // @property (readonly, assign, nonatomic) EILColor color;
        [Export("color", ArgumentSemantic.Assign)]
        EILColor Color { get; }

        // @property (readonly, nonatomic, strong) NSUUID * _Nullable proximityUUID;
        [NullAllowed, Export("proximityUUID", ArgumentSemantic.Strong)]
        NSUuid ProximityUUID { get; }

        // @property (readonly, nonatomic, strong) NSNumber * _Nullable major;
        [NullAllowed, Export("major", ArgumentSemantic.Strong)]
        NSNumber Major { get; }

        // @property (readonly, nonatomic, strong) NSNumber * _Nullable minor;
        [NullAllowed, Export("minor", ArgumentSemantic.Strong)]
        NSNumber Minor { get; }

        // -(instancetype _Nonnull)initWithBeaconIdentifier:(NSString * _Nonnull)identifier position:(EILOrientedPoint * _Nonnull)position;
        [Export("initWithBeaconIdentifier:position:")]
        IntPtr Constructor(string identifier, EILOrientedPoint position);

        // -(instancetype _Nonnull)initWithBeaconIdentifier:(NSString * _Nonnull)identifier position:(EILOrientedPoint * _Nonnull)position color:(EILColor)color;
        [Export("initWithBeaconIdentifier:position:color:")]
        IntPtr Constructor(string identifier, EILOrientedPoint position, EILColor color);

        // -(instancetype _Nonnull)initWithBeaconIdentifier:(NSString * _Nonnull)identifier position:(EILOrientedPoint * _Nonnull)position color:(EILColor)color proximityUUID:(NSUUID * _Nullable)proximityUUID major:(NSNumber * _Nullable)major minor:(NSNumber * _Nullable)minor;
        [Export("initWithBeaconIdentifier:position:color:proximityUUID:major:minor:")]
        IntPtr Constructor(string identifier, EILOrientedPoint position, EILColor color, [NullAllowed] NSUuid proximityUUID, [NullAllowed] NSNumber major, [NullAllowed] NSNumber minor);

        // -(instancetype _Nonnull)beaconTranslatedByDX:(double)dX dY:(double)dY;
        [Export("beaconTranslatedByDX:dY:")]
        EILPositionedBeacon BeaconTranslatedBy(double dX, double dY);
    }

    // @interface EILLocation : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    public interface EILLocation : INSCoding
    {
        // @property (readonly, nonatomic, strong) NSString * _Nullable identifier;
        [NullAllowed, Export("identifier", ArgumentSemantic.Strong)]
        string Identifier { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; }

        // @property (readonly, nonatomic, strong) NSDictionary * _Nullable geographicalLocation;
        [NullAllowed, Export("geographicalLocation", ArgumentSemantic.Strong)]
        NSDictionary GeographicalLocation { get; }

        // @property (readonly, nonatomic, strong) NSNumber * _Nullable latitude;
        [NullAllowed, Export("latitude", ArgumentSemantic.Strong)]
        NSNumber Latitude { get; }

        // @property (readonly, nonatomic, strong) NSNumber * _Nullable longitude;
        [NullAllowed, Export("longitude", ArgumentSemantic.Strong)]
        NSNumber Longitude { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nullable owner;
        [NullAllowed, Export("owner", ArgumentSemantic.Strong)]
        string Owner { get; }

        // @property (readonly, assign, nonatomic) BOOL isPublic;
        [Export("isPublic")]
        bool IsPublic { get; }

        // @property (readonly, nonatomic, strong) NSArray<EILOrientedLineSegment *> * _Nonnull boundarySegments;
        [Export("boundarySegments", ArgumentSemantic.Strong)]
        EILOrientedLineSegment[] BoundarySegments { get; }

        // @property (readonly, nonatomic, strong) UIBezierPath * _Nonnull shape;
        [Export("shape", ArgumentSemantic.Strong)]
        UIBezierPath Shape { get; }

        // @property (readonly, nonatomic, strong) NSArray<EILPoint *> * _Nullable polygon;
        [NullAllowed, Export("polygon", ArgumentSemantic.Strong)]
        EILPoint[] Polygon { get; }

        // @property (readonly, assign, nonatomic) double area;
        [Export("area")]
        double Area { get; }

        // @property (readonly, assign, nonatomic) CGRect boundingBox;
        [Export("boundingBox", ArgumentSemantic.Assign)]
        CGRect BoundingBox { get; }

        // @property (readonly, nonatomic, strong) NSArray<EILLocationLinearObject *> * _Nonnull linearObjects;
        [Export("linearObjects", ArgumentSemantic.Strong)]
        EILLocationLinearObject[] LinearObjects { get; }

        // @property (readonly, nonatomic, strong) NSArray<EILPositionedBeacon *> * _Nonnull beacons;
        [Export("beacons", ArgumentSemantic.Strong)]
        EILPositionedBeacon[] Beacons { get; }

        // @property (readonly, assign, nonatomic) double orientation;
        [Export("orientation")]
        double Orientation { get; }

        // @property (readonly, nonatomic, strong) NSArray<EILLocationPin *> * _Nullable locationPins;
        [NullAllowed, Export("locationPins", ArgumentSemantic.Strong)]
        EILLocationPin[] LocationPins { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nonnull creationDate;
        [Export("creationDate", ArgumentSemantic.Strong)]
        NSDate CreationDate { get; }

        // -(instancetype _Nonnull)initWithLocation:(EILLocation * _Nonnull)location;
        [Export("initWithLocation:")]
        IntPtr Constructor(EILLocation location);

        // -(EILLocation * _Nonnull)locationTranslatedByDX:(double)x dY:(double)y;
        [Export("locationTranslatedByDX:dY:")]
        EILLocation LocationTranslatedBy(double x, double y);

        // -(BOOL)containsPointWithX:(double)x y:(double)y;
        [Export("containsPointWithX:y:")]
        bool ContainsPointWithX(double x, double y);

        // -(BOOL)containsPoint:(EILPoint * _Nonnull)point;
        [Export("containsPoint:")]
        bool ContainsPoint(EILPoint point);

        // -(EILPoint * _Nonnull)randomPointInside;
        [Export("randomPointInside")]
        EILPoint RandomPointInside { get; }

        // -(NSArray<EILLocationLinearObject *> * _Nonnull)linearObjectsWithType:(EILLocationLinearObjectType)type;
        [Export("linearObjectsWithType:")]
        EILLocationLinearObject[] LinearObjectsWithType(EILLocationLinearObjectType type);
    }

    // @interface EILLocationPin : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    public interface EILLocationPin : INSCoding
    {
        // @property (readonly, nonatomic, strong) NSString * _Nonnull name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull type;
        [Export("type", ArgumentSemantic.Strong)]
        string Type { get; }

        // @property (readonly, nonatomic) NSNumber * _Nonnull identifier;
        [Export("identifier")]
        NSNumber Identifier { get; }

        // @property (readonly, nonatomic, strong) EILOrientedPoint * _Nonnull position;
        [Export("position", ArgumentSemantic.Strong)]
        EILOrientedPoint Position { get; }

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name type:(NSString * _Nonnull)type identifier:(NSNumber * _Nullable)identifier position:(EILOrientedPoint * _Nonnull)position;
        [Export("initWithName:type:identifier:position:")]
        IntPtr Constructor(string name, string type, [NullAllowed] NSNumber identifier, EILOrientedPoint position);

        // -(instancetype _Nonnull)locationPinTranslatedBydX:(double)dX dY:(double)dY;
        [Export("locationPinTranslatedBydX:dY:")]
        EILLocationPin LocationPinTranslatedBy(double dX, double dY);
    }

    // @interface EILLocationBuilder : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILLocationBuilder
    {
        // -(void)setLocationBoundaryPoints:(NSArray<EILPoint *> * _Nonnull)boundaryPoints;
        [Export("setLocationBoundaryPoints:")]
        void SetLocationBoundaryPoints(EILPoint[] boundaryPoints);

        // -(void)setLocationOrientation:(double)orientation;
        [Export("setLocationOrientation:")]
        void SetLocationOrientation(double orientation);

        // -(void)addBeaconWithIdentifier:(NSString * _Nonnull)identifier atBoundarySegmentIndex:(NSUInteger)boundarySegmentIndex inDistance:(double)distance fromSide:(EILLocationBuilderSide)side;
        [Export("addBeaconWithIdentifier:atBoundarySegmentIndex:inDistance:fromSide:")]
        void AddBeacon(string identifier, nuint boundarySegmentIndex, double distance, EILLocationBuilderSide side);

        // -(void)addBeaconWithIdentifier:(NSString * _Nonnull)identifier withPosition:(EILOrientedPoint * _Nonnull)position;
        [Export("addBeaconWithIdentifier:withPosition:")]
        void AddBeacon(string identifier, EILOrientedPoint position);

        // -(void)addBeaconWithIdentifier:(NSString * _Nonnull)identifier withPosition:(EILOrientedPoint * _Nonnull)position andColor:(EILColor)color;
        [Export("addBeaconWithIdentifier:withPosition:andColor:")]
        void AddBeacon(string identifier, EILOrientedPoint position, EILColor color);

        // -(void)addLocationPinWithName:(NSString * _Nonnull)name type:(NSString * _Nonnull)type position:(EILOrientedPoint * _Nonnull)position;
        [Export("addLocationPinWithName:type:position:")]
        void AddLocationPin(string name, string type, EILOrientedPoint position);

        // -(void)addDoorsWithLength:(double)length atBoundarySegmentIndex:(NSUInteger)boundarySegmentIndex inDistance:(double)distance fromSide:(EILLocationBuilderSide)side;
        [Export("addDoorsWithLength:atBoundarySegmentIndex:inDistance:fromSide:")]
        void AddDoors(double length, nuint boundarySegmentIndex, double distance, EILLocationBuilderSide side);

        // -(void)addWindowWithLength:(double)length atBoundarySegmentIndex:(NSUInteger)boundarySegmentIndex inDistance:(double)distance fromSide:(EILLocationBuilderSide)side;
        [Export("addWindowWithLength:atBoundarySegmentIndex:inDistance:fromSide:")]
        void AddWindow(double length, nuint boundarySegmentIndex, double distance, EILLocationBuilderSide side);

        // -(void)setLocationName:(NSString * _Nonnull)locationName;
        [Export("setLocationName:")]
        void SetLocationName(string locationName);

        // -(void)setLocationCreationDate:(NSDate * _Nonnull)date;
        [Export("setLocationCreationDate:")]
        void SetLocationCreationDate(NSDate date);

        // -(EILLocation * _Nullable)build;
        [NullAllowed, Export("build")]
        EILLocation Build();
    }

    // @interface EILPositionView : UIImageView
    [BaseType(typeof(UIImageView))]
    public interface EILPositionView
    {
        // -(instancetype _Nonnull)initWithImage:(UIImage * _Nonnull)image location:(EILLocation * _Nonnull)location showAccuracyCircle:(BOOL)showAccuracyCircle forViewWithBounds:(CGRect)bounds;
        [Export("initWithImage:location:showAccuracyCircle:forViewWithBounds:")]
        IntPtr Constructor(UIImage image, EILLocation location, bool showAccuracyCircle, CGRect bounds);

        // -(instancetype _Nonnull)initWithImage:(UIImage * _Nonnull)image location:(EILLocation * _Nonnull)location showAccuracyCircle:(BOOL)showAccuracyCircle regionOfInterest:(CGRect)regionOfInterest forViewWithBounds:(CGRect)bounds;
        [Export("initWithImage:location:showAccuracyCircle:regionOfInterest:forViewWithBounds:")]
        IntPtr Constructor(UIImage image, EILLocation location, bool showAccuracyCircle, CGRect regionOfInterest, CGRect bounds);

        // -(void)updateAccuracy:(EILPositionAccuracy)accuracy;
        [Export("updateAccuracy:")]
        void UpdateAccuracy(EILPositionAccuracy accuracy);
    }

    // typedef void (^EILRequestAddLocationBlock)(EILLocation * _Nullable, NSError * _Nullable);
    public delegate void EILRequestAddLocationBlock([NullAllowed] EILLocation location, [NullAllowed] NSError error);

    // @interface EILRequestAddLocation : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestAddLocation
    {
        // -(instancetype _Nonnull)initWithLocation:(EILLocation * _Nonnull)location;
        [Export("initWithLocation:")]
        IntPtr Constructor(EILLocation location);

        // -(void)sendRequestWithCompletion:(EILRequestAddLocationBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestAddLocationBlock completion);
    }

    // typedef void (^EILRequestFetchLocationBlock)(EILLocation * _Nullable, NSError * _Nullable);
    public delegate void EILRequestFetchLocationBlock([NullAllowed] EILLocation location, [NullAllowed] NSError error);

    // @interface EILRequestFetchLocation : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestFetchLocation
    {
        // -(instancetype _Nonnull)initWithLocationIdentifier:(NSString * _Nonnull)identifier;
        [Export("initWithLocationIdentifier:")]
        IntPtr Constructor(string identifier);

        // -(void)sendRequestWithCompletion:(EILRequestFetchLocationBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestFetchLocationBlock completion);
    }

    // typedef void (^EILRequestFetchLocationsBlock)(NSArray<EILLocation *> * _Nullable, NSError * _Nullable);
    public delegate void EILRequestFetchLocationsBlock([NullAllowed] EILLocation[] locations, [NullAllowed] NSError error);

    // @interface EILRequestFetchLocations : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestFetchLocations
    {
        // -(void)sendRequestWithCompletion:(EILRequestFetchLocationsBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestFetchLocationsBlock completion);
    }

    // typedef void (^EILRequestFetchPublicLocationsBlock)(NSArray<EILLocation *> * _Nullable, NSError * _Nullable);
    public delegate void EILRequestFetchPublicLocationsBlock([NullAllowed] EILLocation[] locations, [NullAllowed] NSError error);

    // @interface EILRequestFetchPublicLocations : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestFetchPublicLocations
    {
        // -(instancetype _Nonnull)initWithBeaconIdentifiers:(NSArray<NSString *> * _Nonnull)identifiers;
        [Export("initWithBeaconIdentifiers:")]
        IntPtr Constructor(string[] identifiers);

        // -(void)sendRequestWithCompletion:(EILRequestFetchPublicLocationsBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestFetchPublicLocationsBlock completion);
    }

    // typedef void (^EILRequestRemoveLocationBlock)(EILLocation * _Nullable, NSError * _Nullable);
    public delegate void EILRequestRemoveLocationBlock([NullAllowed] EILLocation location, [NullAllowed] NSError error);

    // @interface EILRequestRemoveLocation : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestRemoveLocation
    {
        // -(instancetype _Nonnull)initWithLocation:(EILLocation * _Nonnull)location;
        [Export("initWithLocation:")]
        IntPtr Constructor(EILLocation location);

        // -(instancetype _Nonnull)initWithLocationIdentifier:(NSString * _Nonnull)locationIdentifier;
        [Export("initWithLocationIdentifier:")]
        IntPtr Constructor(string locationIdentifier);

        // -(void)sendRequestWithCompletion:(EILRequestRemoveLocationBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestRemoveLocationBlock completion);
    }

    // typedef void (^EILRequestModifyLocationBlock)(EILLocation * _Nullable, NSError * _Nullable);
    public delegate void EILRequestModifyLocationBlock([NullAllowed] EILLocation location, [NullAllowed] NSError error);

    // @interface EILRequestModifyLocation : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestModifyLocation
    {
        // -(instancetype _Nonnull)initWithLocation:(EILLocation * _Nonnull)location locationIdentifier:(NSString * _Nonnull)locationIdentifier;
        [Export("initWithLocation:locationIdentifier:")]
        IntPtr Constructor(EILLocation location, string locationIdentifier);

        // -(void)sendRequestWithCompletion:(EILRequestModifyLocationBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestModifyLocationBlock completion);
    }

    // typedef void (^EILRequestAddPinToLocationBlock)(EILLocationPin * _Nullable, NSError * _Nullable);
    public delegate void EILRequestAddPinToLocationBlock([NullAllowed] EILLocationPin locationPin, [NullAllowed] NSError error);

    // @interface EILRequestAddPinToLocation : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestAddPinToLocation
    {
        // -(instancetype _Nonnull)initWithLocationPin:(EILLocationPin * _Nonnull)pin locationIdentifier:(NSString * _Nonnull)identifier;
        [Export("initWithLocationPin:locationIdentifier:")]
        IntPtr Constructor(EILLocationPin pin, string identifier);

        // -(void)sendRequestWithCompletion:(EILRequestAddPinToLocationBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestAddPinToLocationBlock completion);
    }

    // typedef void (^EILRequestFetchLocationPinsBlock)(NSArray<EILLocationPin *> * _Nullable, NSError * _Nullable);
    public delegate void EILRequestFetchLocationPinsBlock([NullAllowed] EILLocationPin[] locationPin, [NullAllowed] NSError error);

    // @interface EILRequestFetchLocationPins : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestFetchLocationPins
    {
        // -(instancetype _Nonnull)initWithLocationIdentifier:(NSString * _Nonnull)identifier;
        [Export("initWithLocationIdentifier:")]
        IntPtr Constructor(string identifier);

        // -(void)sendRequestWithCompletion:(EILRequestFetchLocationPinsBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestFetchLocationPinsBlock completion);
    }

    // typedef void (^EILRequestUpdateLocationPinBlock)(EILLocationPin * _Nullable, NSError * _Nullable);
    public delegate void EILRequestUpdateLocationPinBlock([NullAllowed] EILLocationPin locationPin, [NullAllowed] NSError error);

    // @interface EILRequestUpdateLocationPin : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestUpdateLocationPin
    {
        // -(instancetype _Nonnull)initWithPin:(EILLocationPin * _Nonnull)pin pinIdentifier:(NSNumber * _Nonnull)pinIdentifier;
        [Export("initWithPin:pinIdentifier:")]
        IntPtr Constructor(EILLocationPin pin, NSNumber pinIdentifier);

        // -(void)sendRequestWithCompletion:(EILRequestUpdateLocationPinBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestUpdateLocationPinBlock completion);
    }

    // typedef void (^EILRequestRemoveLocationPinBlock)(EILLocationPin * _Nullable, NSError * _Nullable);
    public delegate void EILRequestRemoveLocationPinBlock([NullAllowed] EILLocationPin locationPin, [NullAllowed] NSError error);

    // @interface EILRequestRemoveLocationPin : NSObject
    [BaseType(typeof(NSObject))]
    public interface EILRequestRemoveLocationPin
    {
        // -(instancetype _Nonnull)initWithPin:(EILLocationPin * _Nonnull)locationPin;
        [Export("initWithPin:")]
        IntPtr Constructor(EILLocationPin locationPin);

        // -(instancetype _Nonnull)initWithPinIdentifier:(NSNumber * _Nonnull)identifier;
        [Export("initWithPinIdentifier:")]
        IntPtr Constructor(NSNumber identifier);

        // -(void)sendRequestWithCompletion:(EILRequestRemoveLocationPinBlock _Nonnull)completion;
        [Export("sendRequestWithCompletion:")]
        void SendRequest(EILRequestRemoveLocationPinBlock completion);
    }

    // @interface EILPositionNode : SKSpriteNode
    [BaseType(typeof(SKSpriteNode))]
    public interface EILPositionNode
    {
        // @property (assign, nonatomic) BOOL showAccuracy;
        [Export("showAccuracy")]
        bool ShowAccuracy { get; set; }

        // -(void)updateAccuracy:(CGFloat)accuracy;
        [Export("updateAccuracy:")]
        void UpdateAccuracy(nfloat accuracy);
    }
}

#pragma warning restore IDE1006
