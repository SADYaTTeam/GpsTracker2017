package com.kamanda.timon.gpstracker;

import android.provider.Settings;

import java.util.Map;
import java.util.Set;

/**
 * Created by timon on 23.05.2017.
 */

public class DataMessage {
    private double longitude;
    private double latitude;
    private String deviceId;

    double getlongtitude() {
        return longitude;
    };

    double getLatitude() {
        return longitude;
    };

    String getDeviceId(){ return deviceId;  };

    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }

    public void setDeviceId (String deviceId) { this.deviceId = deviceId;};





}
