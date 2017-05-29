package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.util.Log;

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
        return this.message; }

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
    public  void findLocation() {
        try {

            locationManager = (LocationManager) context.getSystemService(Context.LOCATION_SERVICE);
            isGPSEnabled = locationManager.isProviderEnabled(locationManager.GPS_PROVIDER);
            isNetworkEnabled = locationManager.isProviderEnabled(locationManager.NETWORK_PROVIDER);

            if (ContextCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
                if (isGPSEnabled) {
                    if (location == null) {
                        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, INTERVAL_MILISECONDS, 0, this);
                        if (locationManager != null) {
                            location = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
                        }

                    }

                }
                if (location == null) {
                    if (isNetworkEnabled) {
                        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, INTERVAL_MILISECONDS, 0, this);
                        if (locationManager != null) {
                            location = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
                        }
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
       // asyncT.execute();
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
        //TODO Complete event handler and show message about GPS disconnect
        // Toast.makeText(getBaseContext(), "You are dissconnected", Toast.LENGTH_LONG).show();
        Log.w("MyMapListener", "GPS is disabled!");
    }

   // public IBinder onBind(Intent arg0){
   //     return null;
    //}
}
