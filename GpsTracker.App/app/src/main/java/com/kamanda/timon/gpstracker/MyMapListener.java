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

    private Context _context;

    private boolean _isGPSEnabled;
    private boolean _isNetworkEnabled;
    private boolean _canGetLocation;

    private DataMessage _message;

    private Location _location;
    protected LocationManager locationManager;

    public MyMapListener(Context _context) {
        this._context = _context;
        _message = new DataMessage();
        findLocation();
    }

    public DataMessage getDataMessage() {return this._message;}
    public void setDeviceId(String _deviceId) {
        this._message.set_deviceId(_deviceId);
    }
    public void setMessageType(int _messageType) {
        this._message.set_messageType(_messageType);
    }

    public  void findLocation() {
        try {

            locationManager = (LocationManager) _context.getSystemService(Context.LOCATION_SERVICE);
            _isGPSEnabled = locationManager.isProviderEnabled(locationManager.GPS_PROVIDER);
            _isNetworkEnabled = locationManager.isProviderEnabled(locationManager.NETWORK_PROVIDER);

            if(ContextCompat.checkSelfPermission(_context, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
                if(_isGPSEnabled) {
                    if (_location == null) {
                        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 0, this);
                        if (locationManager != null) {
                            _location = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
                        }

                    }

                }
                if (_location ==null) {
                    if(_isNetworkEnabled) {
                        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 0, this);
                        if (locationManager != null) {
                            _location = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
                        }
                    }
                }
            }

        } catch(Exception ex) {

        }

        _message.set_longitude(_location.getLongitude());
        _message.set_latitude(_location.getLatitude());

        Log.i("GPS_Coordinates_findLoc", _location.getLatitude()+ "; " + _location.getLongitude());
    }


    @Override
    public void onLocationChanged(Location location) {
        findLocation();
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
