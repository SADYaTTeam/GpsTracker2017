
package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.location.Location;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Environment;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.FragmentActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Toast;
import android.provider.Settings.Secure;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;


import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import static com.kamanda.timon.gpstracker.R.id.map;

/**
 *
 */
public class MapsActivity extends FragmentActivity
        implements OnMapReadyCallback,
        GoogleApiClient.ConnectionCallbacks,
        GoogleApiClient.OnConnectionFailedListener,
        GoogleMap.OnMarkerDragListener,
        GoogleMap.OnMapLongClickListener,
        GoogleMap.OnMarkerClickListener,
        View.OnClickListener {

    /**
     *
     */
    //region Variables
    private static final String TAG = "MapsActivity";
    /**
     *
     */
    private static final int ZOOM = 15;
    public static String DEVICE_ID;
    /**
     *
     */
    private GoogleMap mMap;
    /**
     *
     */
    //  private double longitude;
    //  private double latitude;
    private GoogleApiClient googleApiClient;
    /**
     *
     */
    private DataMessage message;
    /**
     *
     */

    //endregion Variables


    /**
     * @param savedInstanceState
     */
    //region Activity
    @Override
    protected void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_maps);
        message = new DataMessage();
        DEVICE_ID = Secure.getString(this.getContentResolver(),
                Secure.ANDROID_ID);
        Log.i("deviceId", DEVICE_ID.toString());
        saveAndroidIdToFile();
        startService(new Intent(this, MyService.class));


        // Obtain the SupportMapFragment
        // and get notified when the map is ready to be used.
        SupportMapFragment mapFragment =
                (SupportMapFragment) getSupportFragmentManager()
                        .findFragmentById(map);
        mapFragment.getMapAsync(this);

        //Initializing googleApiClient
        googleApiClient = new GoogleApiClient.Builder(this)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API)
                .build();

        //minimizeApp();
    }

    private void saveAndroidIdToFile() {


        String filename = "MyAndroidId.txt";
        String deviceId = Secure.getString(this.getContentResolver(),
                Secure.ANDROID_ID);
        String sData = DEVICE_ID;
        String root = Environment.getExternalStorageDirectory().toString();
        File myDir = new File(root + "/GpsTracker");
        myDir.mkdirs();
        File file = new File(myDir, filename);
        if (file.exists()) file.delete();
        try {
            FileOutputStream out = new FileOutputStream(file);
            out.write(sData.getBytes());
            out.flush();
            out.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     *
     */
    @Override
    protected void onStart() {
        googleApiClient.connect();
        super.onStart();
    }

    /**
     *
     */
    @Override
    protected void onStop() {
        googleApiClient.disconnect();
        super.onStop();
    }

    /**
     *
     */
    public void minimizeApp() {
        Intent startMain = new Intent(Intent.ACTION_MAIN);
        startMain.addCategory(Intent.CATEGORY_HOME);
        startMain.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(startMain);
    }
    //endregion Activity

    /**
     * @param googleMap
     */
    //region Google Map Initialization And Event Properties
    @Override
    public void onMapReady(final GoogleMap googleMap) {
        mMap = googleMap;
        mMap.setMapType(GoogleMap.MAP_TYPE_NORMAL);
        // googleMapOptions.mapType(googleMap.MAP_TYPE_HYBRID)
        //    .compassEnabled(true);

        // Add a marker in Lviv and move the camera

        LatLng latLng = new LatLng(49.840466, 24.027845);
        //LatLng latLng = new LatLng(message.getLatitude(), message.getLongtitude());
        mMap.addMarker(new MarkerOptions().position(latLng).title("Current Position"));
        mMap.moveCamera(CameraUpdateFactory.newLatLng(latLng));
        mMap.animateCamera(CameraUpdateFactory.zoomTo(ZOOM));
        mMap.setOnMarkerDragListener(this);
        mMap.setOnMapLongClickListener(this);

    }

    /**
     *
     */
    private void moveMap() {
        /**
         * Creating the latlng object to store lat, long coordinates
         * adding marker to map
         * move the camera with animation
         */
        LatLng latLng = new LatLng(message.getLatitude(), message.getLongtitude());
        mMap.addMarker(new MarkerOptions()
                .position(latLng)
                .draggable(false)
                .title("New Marker"));
        mMap.moveCamera(CameraUpdateFactory.newLatLng(latLng));
        mMap.animateCamera(CameraUpdateFactory.zoomTo(ZOOM));
        mMap.getUiSettings().setZoomControlsEnabled(true);


    }

    /**
     * @param view
     */
    @Override
    public void onClick(final View view) {
        Log.v(TAG, "view click event");
    }

    /**
     * @param bundle
     */
    @Override
    public void onConnected(final @Nullable Bundle bundle) {
        moveMap();
    }

    @Override
    public void onConnectionSuspended(final int i) {

    }

    @Override
    public void onConnectionFailed(final @NonNull ConnectionResult connectionResult) {

    }

    /**
     * @param latLng
     */
    @Override
    public void onMapLongClick(final LatLng latLng) {
        // mMap.clear();
        mMap.addMarker(new MarkerOptions().position(latLng).draggable(true));
    }

    /**
     * @param marker
     */
    @Override
    public void onMarkerDragStart(final Marker marker) {
        Toast.makeText(MapsActivity.this, "onMarkerDragStart", Toast.LENGTH_SHORT).show();
    }

    /**
     * @param marker
     */
    @Override
    public void onMarkerDrag(final Marker marker) {
        Toast.makeText(MapsActivity.this, "onMarkerDrag", Toast.LENGTH_SHORT).show();
    }

    /**
     * @param marker
     */
    @Override
    public void onMarkerDragEnd(final Marker marker) {
        // getting the Co-ordinates


        //move to current position
        moveMap();
    }

    /**
     * @param marker
     * @return
     */
    @Override
    public boolean onMarkerClick(final Marker marker) {
        Toast.makeText(MapsActivity.this, "onMarkerClick", Toast.LENGTH_SHORT).show();
        return true;
    }
    //endregion Google Map Initialization And Event Properties


    //region SOS_BUTTON
    @Override
    public boolean dispatchKeyEvent(final KeyEvent event) {
        int action = event.getAction();
        int keyCode = event.getKeyCode();
        switch (keyCode) {
            case KeyEvent.KEYCODE_VOLUME_UP:
                if (action == KeyEvent.ACTION_DOWN) {
                    //TODO
                    try {
                        Toast.makeText(getApplicationContext(), "JSON sended to server",
                                Toast.LENGTH_LONG).show();
                        startService(new Intent(this, MyService.class));
                    } catch (Exception exc) {
                        Log.e("AsyncT", exc.getMessage(), exc);
                    }
                }
                return true;
            case KeyEvent.KEYCODE_VOLUME_DOWN:
                if (action == KeyEvent.ACTION_DOWN) {
                    //TODO
                }
                return true;
            default:
                return super.dispatchKeyEvent(event);
        }
    }
    //endregion SOS_BUTTON
}