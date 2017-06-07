package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.app.Service;
import android.content.Intent;
import android.content.IntentSender;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Bundle;
import android.os.Environment;
import android.os.IBinder;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.view.KeyEvent;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.location.LocationSettingsRequest;
import com.google.android.gms.location.LocationSettingsResult;
import com.google.android.gms.location.LocationSettingsStates;
import com.google.android.gms.location.LocationSettingsStatusCodes;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

/**
 * Created by dasha_000 on 29.05.2017.
 */

public class MyService extends Service implements GoogleApiClient.ConnectionCallbacks,
        GoogleApiClient.OnConnectionFailedListener,
         LocationListener {

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
    private static int DISPLACEMENT = 50; // 10 meters


    @Override
    public void onCreate() {
        super.onCreate();
        Log.i(logTag, "OnCreate");
        loadAndroidIdFromFile();
        // Building the GoogleApi client
        buildGoogleApiClient();
        createLocationRequest();
//        Intent i = new Intent(Intent.ACTION_MEDIA_BUTTON);
//        synchronized (this) {
//            i.putExtra(Intent.EXTRA_KEY_EVENT, new KeyEvent(KeyEvent.ACTION_DOWN, KeyEvent.KEYCODE_VOLUME_UP));
//            sendOrderedBroadcast(i, null);
//
//            i.putExtra(Intent.EXTRA_KEY_EVENT, new KeyEvent(KeyEvent.ACTION_UP, KeyEvent.KEYCODE_MEDIA_NEXT));
//            sendOrderedBroadcast(i, null);
//        }
    }

    @Override
    public int onStartCommand(final Intent intent, final int flags, final int startId) {
        Log.i(logTag, "onStartCommand");
//        if (mGoogleApiClient != null) {
//            Log.i(logTag, "mGoogleApiClient is not null");
//            mGoogleApiClient.connect();
//        }
        try {
            mGoogleApiClient.connect();
        }
        catch (Exception exc) {
            Log.i(logTag, "mGoogleApiClient is null");
            Log.e(logTag, exc.getMessage().toString());
        }
        //TODO Get location here, form message, send it in JSON with messageType 1 (SOS). Will be executed by onKeyEvent in MainActivity
//        message.setMessageType(1);
//        Log.i("Test", message.getLatitude() + "; " + message.getLongitude() + " DeviceId: " + deviceId + " MessageType: " + message.getMessageType());
//        createJson();


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

    @Nullable
    @Override
    public IBinder onBind(final Intent intent) {
        Log.i(logTag, "onBind");
        return null;
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
        createJson();
    }
    //endregion Listeners interfaces

    private void createJson() {
        AsyncT asyncT = new AsyncT();
        //asyncT.setData(mapListener.getDataMessage().getLatitude(), mapListener.getDataMessage().getLongitude(), deviceId);
        asyncT.setData(mLastLocation.getLatitude(), mLastLocation.getLongitude(), deviceId, message.getMessageType());
        Log.i(logTag, "createJson; mLastLocation: " + mLastLocation.getLatitude() + "; " + mLastLocation.getLongitude());
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
     * */
    protected synchronized void buildGoogleApiClient() {

        mGoogleApiClient = new GoogleApiClient.Builder(this)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API).build();
    }
    /**
     * Creating location request object
     * */
    protected void createLocationRequest() {
        Log.i(logTag, "createLocationRequest");
        mLocationRequest = new LocationRequest();
        mLocationRequest.setInterval(UPDATE_INTERVAL);
        mLocationRequest.setFastestInterval(FATEST_INTERVAL);
        //TODO Sort out how change location request priority depending on current smartphone settings
        //mLocationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        mLocationRequest.setPriority(LocationRequest.PRIORITY_BALANCED_POWER_ACCURACY);
        mLocationRequest.setSmallestDisplacement(DISPLACEMENT);
    }

    /**
     * Starting the location updates
     * */
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



}
