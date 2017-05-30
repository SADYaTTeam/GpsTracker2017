package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.widget.Toast;

/**
 * Created by timon on 24.05.2017.
 */

public class MyMapListener implements LocationListener {

    //region Variables
    /**
     *
     */
    private Context context;

    /**
     *
     */
    private boolean isGPSEnabled;
    /**
     *
     */
    private boolean isNetworkEnabled;
    /**
     *
     */
    private boolean canGetLocation;

    /**
     *
     */
    private DataMessage message;

    /**
     *
     */
    private Location location;
    /**
     *
     */
    private LocationManager locationManager;
    /**
     *
     */
    private static final int INTERVAL_MILISECONDS = 1000;
    AsyncT asyncT = new AsyncT();

    //endregion Variables

    /**
     * @param context
     */
    public MyMapListener(final Context context) {
        this.context = context;
        message = new DataMessage();
        findLocation();
    }

    /**
     * @return
     */
    public DataMessage getDataMessage() {
        return this.message;
    }

    /**
     * @param deviceId
     */
    public void setDeviceId(final String deviceId) {
        this.message.setDeviceId(deviceId);
    }

    /**
     * @param messageType
     */
    public void setMessageType(final int messageType) {
        this.message.setMessageType(messageType);
    }

    /**
     *
     */
    public void findLocation() {
        try {

            locationManager = (LocationManager) context.getSystemService(Context.LOCATION_SERVICE);
            isGPSEnabled = locationManager.isProviderEnabled(locationManager.GPS_PROVIDER);
            isNetworkEnabled = locationManager.isProviderEnabled(locationManager.NETWORK_PROVIDER);

            if (ContextCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
                if (isGPSEnabled) {
                        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, INTERVAL_MILISECONDS, 0, this);
                        if (locationManager != null) {
                            location = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
                        }

                }
                    if (isNetworkEnabled) {
                        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, INTERVAL_MILISECONDS, 0, this);
                        if (locationManager != null) {
                            location = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
                        }
                    }
            }

        } catch (Exception ex) {

        }

        message.setLongitude(location.getLongitude());
        message.setLatitude(location.getLatitude());

        Log.i("GPS_Coordinates_findLoc", location.getLatitude() + "; " + location.getLongitude());
    }


    /**
     * @param location
     */
    @Override
    public void onLocationChanged(final Location location) {
        findLocation();
        try {
            Log.i("AsyncT", "Location Changed; JSON was sended to server");
            //TODO Send asyncT with actual data here
        } catch (Exception exc) {
            Log.e("AsyncT", exc.getMessage(), exc);
        }
    }

    @Override
    public void onStatusChanged(final String provider, final int status, final Bundle extras) {

    }

    /**
     * @param provider
     */
    @Override
    public void onProviderEnabled(final String provider) {
        Log.w("MyMapListener", "GPS is enabled!");
    }

    /**
     * @param provider
     */
    @Override
    public void onProviderDisabled(final String provider) {
        Log.w("MyMapListener", "GPS is disabled!");
    }

}
