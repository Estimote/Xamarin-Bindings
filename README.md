# Estimote Xamarin Bindings

An example Visual Studio Solution which shows how to bind Estimote SDKs to Xamarin. Includes example apps which show how to use the generated bindings.

## Getting started

> **Note:** This repo includes submodules.<br>
Clone with `git clone --recursive`, or run `git submodule update --init` before use.

Open the Estimote-Xamarin-Bindings.sln and run the Example.Android.Proximity or Example.iOS.Proximity. Note that iOS/Android simulators don't support Bluetooth, so you need to run on a physical device.

## Use it in your own app

The easiest way is probably to add the Estimote.\*.Proximity project to your own Solution, and then add an appropriate Reference.

There's a few extra things to keep in mind:

**iOS**

- You need to install the Xamarin.Swift4 package in your app.
    - (Estimote Proximity SDK for iOS is built mostly in Swift, and so it depends on these Swift libraries.)

- In your Info.plist file, you need to add

    - `NSLocationWhenInUseUsageDescription` and `NSLocationAlwaysAndWhenInUseUsageDescription`
        - for more infor about the two, see [Set the Location Services usage description](https://developer.estimote.com/proximity/ios-tutorial/#set-the-location-services-usage-description) in our integration tutorial

    - `UIBackgroundModes` => `bluetooth-central`

**Android**

- In your AndroidManifest.xml, you need to add

    - this `<uses-permission>` definition inside the `<manifest>` tag:

        ```xml
        <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
        ```

    - this `<service>` definition inside the `<application>` tag:

        ```xml
        <service
            android:name="com.estimote.scanning_plugin.packet_provider.service.PacketProviderWrapperService"
            android:enabled="true"
            android:exported="false" />
        ```

    - here's a full example from the bundled Example.Android.Proximity app:

        ```xml
        <?xml version="1.0" encoding="utf-8"?>
        <manifest xmlns:android="http://schemas.android.com/apk/res/android"
                  android:versionCode="1"
                  android:versionName="1.0"
                  package="com.estimote.example.Proximity">
            <uses-sdk android:minSdkVersion="18" android:targetSdkVersion="27" />
            <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
            <application android:allowBackup="true" android:label="@string/app_name">
                <service
                    android:name="com.estimote.scanning_plugin.packet_provider.service.PacketProviderWrapperService"
                    android:enabled="true"
                    android:exported="false" />
            </application>
        </manifest>
        ```

- Make sure your project includes these support packages, as the Proximity SDK depends on them:

    - Xamarin.Android.Support.Annotations
    - Xamarin.Android.Support.Compat
    - Xamarin.Android.Support.Core.Utils

## Contact & feedback

Let us know your thoughts and feedback on [forums.estimote.com][forums].

[forums]: https://forums.estimote.com
