package com.kamanda.timon.gpstracker;

import android.location.Location;
import android.location.LocationListener;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import com.google.android.gms.location.LocationServices;
import com.kamanda.timon.gpstracker.DataMessage;
import com.kamanda.timon.gpstracker.MapsActivity;

/**
 * Created by timon on 24.05.2017.
 */

public class MyMapListener implements LocationListener {

    DataMessage message;

    public MyMapListener(DataMessage message) {
        this.message = message;
    }

    @Override
    public void onLocationChanged(Location location) {
        message.set_latitude(location.getLatitude());
        message.set_longitude(location.getLongitude());
        Log.i("GPS_Coordinates", message.get_latitude()+ "; " + message.getlongtitude());
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {

    }

    @Override
    public void onProviderEnabled(String provider) {

    }

    @Override
    public void onProviderDisabled(String provider) {

    }
}
