package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.IBinder;
import android.support.v4.content.ContextCompat;
import android.util.Log;

/**
 * Created by timon on 24.05.2017.
 */

public class MyMapListener implements LocationListener {

    Context context;

    boolean isGPSEnabled;
    boolean isNetworkEnabled;
    boolean canGetLocation;

    Location location;
    protected LocationManager locationManager;

    public MyMapListener(Context context) {
        this.context = context;
    }

    public Location getLocation() {
        try {

            locationManager = (LocationManager)context .getSystemService(Context.LOCATION_SERVICE);
            isGPSEnabled = locationManager.isProviderEnabled(locationManager.GPS_PROVIDER);
            isNetworkEnabled = locationManager.isProviderEnabled(locationManager.NETWORK_PROVIDER);

            if(ContextCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
                if(isGPSEnabled) {
                    if (location == null) {
                        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 0, this);
                        if (locationManager != null) {
                            location = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
                        }

                    }

                }
                if (location==null) {
                    if(isNetworkEnabled) {
                        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 0, this);
                        if (locationManager != null) {
                            location = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
                        }
                    }
                }
            }

        } catch(Exception ex) {

        }
        Log.i("GPS_Coordinates", location.getLatitude()+ "; " + location.getLongitude());
        return location;
    }
    @Override
    public void onLocationChanged(Location location) {
        try {

            locationManager = (LocationManager)context .getSystemService(Context.LOCATION_SERVICE);
            isGPSEnabled = locationManager.isProviderEnabled(locationManager.GPS_PROVIDER);
            isNetworkEnabled = locationManager.isProviderEnabled(locationManager.NETWORK_PROVIDER);

            if(ContextCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
                if(isGPSEnabled) {

                        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 0, this);
                        if (locationManager != null) {
                            locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
                    }

                }

                    if(isNetworkEnabled) {
                        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 0, this);
                        if (locationManager != null) {
                            locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
                        }
                }
            }

        } catch(Exception ex) {

        }
        Log.i("GPS_Coordinates", location.getLatitude()+ "; " + location.getLongitude());

        //message.set_latitude(location.getLatitude());
        //message.set_longitude(location.getLongitude());
        //Log.i("GPS_Coordinates", message.get_latitude()+ "; " + message.get_longtitude());
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {

    }

    @Override
    public void onProviderEnabled(String provider) {
        Log.w("MyMapListener","GPS is enabled!");
    }

    @Override
    public void onProviderDisabled(String provider) {
        //TODO Complete event handler and show message about GPS disconnect
        // Toast.makeText(getBaseContext(), "You are dissconnected", Toast.LENGTH_LONG).show();
        Log.w("MyMapListener","GPS is disabled!");
    }

   // public IBinder onBind(Intent arg0){
   //     return null;
    //}
}
