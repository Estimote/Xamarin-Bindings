using System;
using ObjCRuntime;

namespace Estimote.iOS.Indoor
{
    [Native]
    public enum EILIndoorLocationManagerMode : long
    {
        Standard,
        ExperimentalWithInertia,
        ExperimentalWithARKit
    }

    [Native]
    public enum EILLocationState : long
    {
        Unknown,
        Inside,
        Outside
    }

    [Native]
    public enum EILIndoorErrorCode : long
    {
        IndoorGenericError,
        IndoorPositionOutsideLocationError,
        IndoorMagnetometerInitializationError,
        BluetoothOffError,
        UnauthorizedToUseBluetoothError,
        BluetoothNotSupportedError
    }

    [Native]
    public enum EILPositionAccuracy : long
    {
        VeryHigh = 0,
        High = 1,
        Medium = 2,
        Low = 3,
        VeryLow = 4,
        Unknown = 5
    }

    [Native]
    public enum EILColor : long
    {
        Unknown,
        MintCocktail,
        IcyMarshmallow,
        BlueberryPie,
        SweetBeetroot,
        CandyFloss,
        LemonTart,
        White,
        Black,
        CoconutPuff,
        Transparent
    }

    [Native]
    public enum EILIndoorLocationSceneZPosition : long
    {
        Background = -10,
        LocationShape = 0,
        Trace = 20,
        Foreground = 30,
        Beacons = 40,
        Avatar = 50
    }

    [Native]
    public enum EILLocationLinearObjectType : long
    {
        Door,
        Window
    }

    public enum EILLocationBuilderSide : long
    {
        LeftSide,
        RightSide
    }
}
