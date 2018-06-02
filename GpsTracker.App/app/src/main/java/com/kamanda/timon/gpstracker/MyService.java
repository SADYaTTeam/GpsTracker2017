package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.app.Service;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Binder;
import android.os.Bundle;
import android.os.Environment;
import android.os.IBinder;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

/**
 * Created by timon9551 on 29.05.2017.
 */

public class MyService extends Service implements GoogleApiClient.ConnectionCallbacks,
        GoogleApiClient.OnConnectionFailedListener,
        LocationListener {

    private static final int MESSAGE_TYPE_SOS = 0;
    private final String logTag = "MyService";
    private String deviceId;

    private DataMessage message = new DataMessage();

    Location mLastLocation;

    // Google client to interact with Google API
    private GoogleApiClient mGoogleApiClient;

    // boolean flag to toggle periodic location updates
    //private boolean mRequestingLocationUpdates = false;

    private LocationRequest mLocationRequest;

    // Location updates intervals in sec
    private static int UPDATE_INTERVAL = 10000; // 10 sec
    private static int FATEST_INTERVAL = 5000; // 5 sec
    private static int DISPLACEMENT = 0; // 10 meters

    private int updateInterval = 10000;
    private int fatestInterval = 5000;
    private int displacement = 0;


    @Override
    public void onCreate() {
        super.onCreate();
        Log.i(logTag, "OnCreate");
        loadAndroidIdFromFile();
        // Building the GoogleApi client
        buildGoogleApiClient();

    }

    @Override
    public int onStartCommand(final Intent intent, final int flags, final int startId) {
        Log.i(logTag, "onStartCommand");
//        if (mGoogleApiClient != null) {
//            Log.i(logTag, "mGoogleApiClient is not null");
//            mGoogleApiClient.connect();
//        }
        if (intent.hasExtra("updateInterval"))
            updateInterval = (Integer) intent.getExtras().get("updateInterval");
        if (intent.hasExtra("fatestInterval"))
            fatestInterval = (Integer) intent.getExtras().get("fatestInterval");
        if (intent.hasExtra("displacement"))
            displacement = (Integer) intent.getExtras().get("displacement");
        Log.i("SETTINGS_SERVICE", "updateInterval: " + updateInterval + " fatestInterval: " + fatestInterval + " displacement: " + displacement);

        createLocationRequest();
        try {
            mGoogleApiClient.connect();
        } catch (Exception exc) {
            Log.i(logTag, "mGoogleApiClient is null");
            Log.e(logTag, exc.getMessage().toString());
        }
        //TODO Get location here, form message, send it in JSON with messageType 1 (SOS). Will be executed by onKeyEvent in MainActivity
//        message.setMessageType(1);
//        Log.i("Test", message.getLatitude() + "; " + message.getLongitude() + " DeviceId: " + deviceId + " MessageType: " + message.getMessageType());
//        sendGpsJson();


        return super.onStartCommand(intent, flags, startId);
    }

    @Override
    public void onDestroy() {
        if (mGoogleApiClient.isConnected()) {
            mGoogleApiClient.disconnect();
        }
        super.onDestroy();
        Toast.makeText(this, "service done", Toast.LENGTH_SHORT).show();
        Log.i(logTag, "onDestroy");
    }


    IBinder mBinder = new LocalBinder();


    @Override
    public IBinder onBind(Intent intent) {
        Log.i(logTag, "onBind");
        return mBinder;
    }

    public class LocalBinder extends Binder {
        public MyService getMyServiceInstance() {
            return MyService.this;
        }
    }


    //region Listeners interfaces
    @Override
    public void onConnected(final @Nullable Bundle bundle) {
        Log.i(logTag, "OnConnected");
        startLocationUpdates();
    }

    @Override
    public void onConnectionSuspended(final int i) {
        Log.i(logTag, "onConnectionSuspended");
        mGoogleApiClient.connect();
    }

    @Override
    public void onConnectionFailed(final @NonNull ConnectionResult connectionResult) {
        Log.i(logTag, "Connection failed: ConnectionResult.getErrorCode() = "
                + connectionResult.getErrorCode());
    }

    @Override
    public void onLocationChanged(final Location location) {
        // Assign the new location
        mLastLocation = location;
        Log.i(logTag, "onLocationChanged; New coordinates are: " + location.getLatitude() + ", " + location.getLongitude());
        message.setLatitude(mLastLocation.getLatitude());
        message.setLongitude(mLastLocation.getLongitude());
        message.setDeviceId(deviceId);
        message.setMessageType(1);
        Log.i(logTag, "onLocationChanged; mLastLocation: " + mLastLocation.getLatitude() + "; " + mLastLocation.getLongitude());
        Log.i(logTag, "onLocationChanged; message: " + message.getLatitude() + "; " + message.getLongitude() + "; " + message.getDeviceId() + "; MessageType: " + message.getMessageType());
        sendGpsJson();
    }
    //endregion Listeners interfaces

    private void sendGpsJson() {
        AsyncT asyncT = new AsyncT();
        //asyncT.setData(mapListener.getDataMessage().getLatitude(), mapListener.getDataMessage().getLongitude(), deviceId);
        asyncT.setData(mLastLocation.getLatitude(), mLastLocation.getLongitude(), deviceId, message.getMessageType());
        Log.i(logTag, "sendGpsJson; mLastLocation: " + mLastLocation.getLatitude() + "; " + mLastLocation.getLongitude());
        asyncT.execute();
    }

    private void loadAndroidIdFromFile() {
        String root = Environment.getExternalStorageDirectory().toString();
        File file = new File(root + "/GpsTracker/MyAndroidId.txt");
        StringBuilder text = new StringBuilder();
        try {
            //Read text from file

            BufferedReader br = new BufferedReader(new FileReader(file));
            String line;

            while ((line = br.readLine()) != null) {

                text.append(line);
            }
            br.close();
        } catch (IOException e) {
            //You'll need to add proper error handling here
        }
        deviceId = text.toString();
    }

    /**
     * Creating google api client object
     */
    protected synchronized void buildGoogleApiClient() {

        mGoogleApiClient = new GoogleApiClient.Builder(this)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API).build();
    }

    /**
     * Creating location request object
     */
    protected void createLocationRequest() {
//        Log.i(logTag, "createLocationRequest");
//        mLocationRequest = new LocationRequest();
//        mLocationRequest.setInterval(UPDATE_INTERVAL);
//        mLocationRequest.setFastestInterval(FATEST_INTERVAL);
//        //TODO Sort out how change location request priority depending on current smartphone settings
//        //mLocationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
//        mLocationRequest.setPriority(LocationRequest.PRIORITY_BALANCED_POWER_ACCURACY);
//        mLocationRequest.setSmallestDisplacement(DISPLACEMENT);

        Log.i(logTag, "createLocationRequest");
        mLocationRequest = new LocationRequest();
        mLocationRequest.setInterval(updateInterval);
        mLocationRequest.setFastestInterval(fatestInterval);
        //TODO Sort out how change location request priority depending on current smartphone settings
        //mLocationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        mLocationRequest.setPriority(LocationRequest.PRIORITY_BALANCED_POWER_ACCURACY);
        mLocationRequest.setSmallestDisplacement(displacement);
        Log.i("LOCATION_REQUEST", "updateInterval: " + updateInterval + " fatestInterval: " + fatestInterval + " displacement: " + displacement);
    }

    /**
     * Starting the location updates
     */
    protected void startLocationUpdates() {

        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }
        Log.i(logTag, "startLocationUpdates");
        LocationServices.FusedLocationApi.requestLocationUpdates(
                mGoogleApiClient, mLocationRequest, this);
    }

    /**
     * Stopping location updates
     */
    protected void stopLocationUpdates() {
        LocationServices.FusedLocationApi.removeLocationUpdates(
                mGoogleApiClient, this);
    }


    public void sendSOS_JSON() {
        AsyncT asyncT = new AsyncT();
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return;
        }
        LocationServices.FusedLocationApi.requestLocationUpdates(
                mGoogleApiClient, mLocationRequest, this);
        asyncT.setData(mLastLocation.getLatitude(), mLastLocation.getLongitude(), deviceId, MESSAGE_TYPE_SOS);
        Log.i("sendSOS_JSON", mLastLocation.getLatitude() + ", " + mLastLocation.getLongitude() + " Message Type: " + MESSAGE_TYPE_SOS);
        asyncT.execute();
    }
}
