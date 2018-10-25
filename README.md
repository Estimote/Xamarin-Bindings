# Estimote Xamarin Bindings

Xamarin bindings for Estimote SDKs.

Visit https://developer.estimote.com to learn more about the variety of Estimote SDKs, and for the integration tutorials. (The documentation is mostly in Swift and Java, but the Xamarin/C# APIs are almost 1:1 with the native APIs, so the docs should still be quite useful.)

## Use it in your own app

You can get all the Estimote.* packages on NuGet.

We also recommend cloning this repo and checking out the ExampleApps—for example, to see how to request the necessary Location permission on Android.

Here's a list of a few extra things that you need to configure in your projects:

**iOS**

- In your Info.plist file, you need to add

    - `NSLocationWhenInUseUsageDescription` and `NSLocationAlwaysAndWhenInUseUsageDescription`
        - for more infor about the two, see [Set the Location Services usage description](https://developer.estimote.com/proximity/ios-tutorial/#set-the-location-services-usage-description) in our integration tutorial

    - `UIBackgroundModes` => `bluetooth-central`

**Android**

- In your AndroidManifest.xml, you need to add

    - these `<uses-permission>` definitions inside the `<manifest>` tag:

        ```xml
        <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
	<uses-permission android:name="android.permission.BLUETOOTH" />
	<uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />
        ```

    - this `<service>` definition inside the `<application>` tag:

        ```xml
        <service
            android:name="com.estimote.scanning_plugin.packet_provider.service.PacketProviderWrapperService"
            android:enabled="true"
            android:exported="false" />
        ```

    - here's a full example from the bundled Example.Android.Indoor app:

        ```xml
    	<?xml version="1.0" encoding="utf-8"?>
    	<manifest xmlns:android="http://schemas.android.com/apk/res/android"
    	          android:versionCode="1"
    		      android:versionName="1.0"
    		      package="com.estimote.example.Indoor">

    	    <uses-sdk android:minSdkVersion="21" android:targetSdkVersion="28" />

    	    <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
    	    <uses-permission android:name="android.permission.BLUETOOTH" />
    	    <uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />

    	    <application android:allowBackup="true" android:label="@string/app_name">
    	        <service
    	            android:name="com.estimote.scanning_plugin.packet_provider.service.PacketProviderWrapperService"
    		        android:enabled="true"
    		        android:exported="false" />
    	    </application>
    	</manifest>
    	```

## Contact & feedback

Let us know your thoughts and feedback on [forums.estimote.com][forums].

[forums]: https://forums.estimote.com
