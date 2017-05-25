package com.kamanda.timon.gpstracker;

import android.provider.Settings;

/**
 * Created by timon on 23.05.2017.
 */

public class DataMessage {
    private double _longitude;
    private double _latitude;
    private String _deviceId;
    private int _messageType;

    public DataMessage() {
//        this._deviceId = Settings.Secure.getString(this.getContentResolver(),
//                Settings.Secure.ANDROID_ID);

    }

    public double get_longtitude() { return _longitude; };

    public double get_latitude() {
        return _latitude;
    };

    public String get_deviceId(){ return _deviceId;  };

    public void get_deviceId(String _deviceId) { this._deviceId = _deviceId;};

    public int get_messageType(){ return _messageType;  };

    public void set_longitude(double _longitude) {
        this._longitude = _longitude;
    }

    public void set_latitude(double _latitude) {
        this._latitude = _latitude;
    }

    public void set_deviceId(String _deviceId) { this._deviceId = _deviceId;};

    public void set_messageType(int _messageType){ this._messageType = _messageType;  };





}
