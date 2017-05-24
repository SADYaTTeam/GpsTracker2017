package com.kamanda.timon.gpstracker;

/**
 * Created by timon on 23.05.2017.
 */

public class DataMessage {
    private double _longitude;
    private double _latitude;
    private String _deviceId;
    private int _messageType;

    double getlongtitude() {
        return _longitude;
    };

    double get_latitude() {
        return _latitude;
    };

    String get_deviceId(){ return _deviceId;  };

    public void set_longitude(double _longitude) {
        this._longitude = _longitude;
    }

    public void set_latitude(double _latitude) {
        this._latitude = _latitude;
    }

    public void set_deviceId(String _deviceId) { this._deviceId = _deviceId;};





}
