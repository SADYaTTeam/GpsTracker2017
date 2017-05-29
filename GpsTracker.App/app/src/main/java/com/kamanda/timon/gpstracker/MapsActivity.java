
package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.app.Activity;
import android.content.Intent;
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
    private MyMapListener mapListener;
    /**
     *
     */
    private Location mLocation;
    /**
     *
     */
    private String deviceId;
    private AsyncT asyncT = new AsyncT();
    //endregion Variables


    /**
     * @param savedInstanceState
     */
    //region Activity
    @Override
    protected void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_maps);
        mapListener = new MyMapListener(getApplicationContext());
        message = new DataMessage();
        deviceId = Secure.getString(this.getContentResolver(),
                Secure.ANDROID_ID);
        mapListener.setDeviceId(deviceId);

      //  mLocation = mapListener.getLocation();

//        message.setLatitude(mLocation.getLatitude());
//        message.setLongitude(mLocation.getLongitude());

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
        asyncT.execute();
        minimizeApp();
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
        saveLocationToFile();


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

        getCurrentLocation();
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
        ///latitude = marker.getPosition().latitude;
       // longitude = marker.getPosition().longitude;

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

    //region Get current location & move to this marker

    /**
     *
     */
    //Get current location
    private void getCurrentLocation() {
        mMap.clear();
        if (ActivityCompat.checkSelfPermission(this,
                android.Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, android.Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }
        Location location = LocationServices.FusedLocationApi.getLastLocation(googleApiClient);
        if (location != null) {
            //Getting longitude and latitude
            message.setLongitude(location.getLongitude());
            message.setLatitude(location.getLatitude());
            Log.i("GPS_Coordinates_Start", message.getLatitude() + "; " + message.getLongtitude());

            //moving the map to location
            moveMap();
        }
    }

    /**
     * @return
     */
    private String getCurrentLocationString() {
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
        }
        Location location = LocationServices.FusedLocationApi.getLastLocation(googleApiClient);
        if (location != null) {
            //Getting longitude and latitude
            message.setLongitude(location.getLongitude());
            message.setLatitude(location.getLatitude());
        }
        return message.getLatitude() + "; " + message.getLongtitude();
    }
    //endregion Get current location

    //region Save location to file

    /**
     *
     */
    //Saving current location to file
    private void saveLocationToFile() {
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }

            String filename = "MyLocation.txt";
//            String deviceId = Secure.getString(this.getContentResolver(),
//                Secure.ANDROID_ID);

            String sData = getCurrentLocationString() + " \nDevice id:" + deviceId;


        String root = Environment.getExternalStorageDirectory().toString();
        File myDir = new File(root + "/savedCoordinates");
        try {
            myDir.mkdirs();
        } catch (Exception e) {
            e.printStackTrace();
        }
        File file = new File(myDir, filename);
        try {
            if (file.exists()) {
                try {
                    file.delete();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        try {
            FileOutputStream out = new FileOutputStream(file);
            out.write(sData.getBytes());
            out.flush();
            out.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    //endregion Save location to file

    //region Generate JSON and send it to URL

    /**
     *
     */
    //check if you are connected or not
    private void checkConnection() {
        try {
            if (isConnected()) {
                Toast.makeText(getBaseContext(), "You are connected", Toast.LENGTH_LONG).show();
            } else {
                Toast.makeText(getBaseContext(), "You are NOT connected", Toast.LENGTH_LONG).show();
            }
        } catch (Exception ex) {

        }

    }

    /**
     * @param url
     * @param message
     * @return
     */
    public static String post(final String url, final DataMessage message) {
        InputStream inputStream = null;
        String result = "";
        try {

            // 1. create HttpClient
            HttpClient httpclient = new DefaultHttpClient();

            // 2. make post request to the given URL
            HttpPost httpPost = new HttpPost(url);

            String json = "";

            // 3. build jsonObject
            JSONObject jsonObject = new JSONObject();
            jsonObject.accumulate("longtitude", message.getLongtitude());
            jsonObject.accumulate("latitude", message.getLatitude());
            jsonObject.accumulate("deviceId", message.getDeviceId());

            // 4. convert JSONObject to JSON to String
            json = jsonObject.toString();
//          jsonSave = json;


            // ** Alternative way to convert Person object to JSON string usin Jackson Lib
            // ObjectMapper mapper = new ObjectMapper();
            // json = mapper.writeValueAsString(person);

            // 5. set json to StringEntity
            StringEntity se = new StringEntity(json);

            // 6. set httpPost Entity
            httpPost.setEntity(se);

            // 7. Set some headers to inform server about the type of the content
            httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-type", "application/json");

            // 8. Execute post request to the given URL
            HttpResponse httpResponse = httpclient.execute(httpPost);

            // 9. receive response as inputStream
            inputStream = httpResponse.getEntity().getContent();

            // 10. convert inputstream to string
            if (inputStream != null) {
                result = convertInputStreamToString(inputStream); }
            else {
                result = "Did not work!";
            }
        } catch (Exception e) {
            Log.d("InputStream", e.getLocalizedMessage());
        }

        // 11. return result
        return result;
    }

    /**
     * @return
     */
    public boolean isConnected() {
        ConnectivityManager connMgr = (ConnectivityManager) getSystemService(Activity.CONNECTIVITY_SERVICE);
        NetworkInfo networkInfo = connMgr.getActiveNetworkInfo();
        if (networkInfo != null && networkInfo.isConnected()) {
            return true; }
        else {
            return false;
        }
    }

    private class HttpAsyncTask extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(final String... urls) {

            return post(urls[0], mapListener.getDataMessage());
        }
        // onPostExecute displays the results of the AsyncTask.
        @Override
        protected void onPostExecute(final String result) {
            Toast.makeText(getBaseContext(), "Data Sent!", Toast.LENGTH_LONG).show();
        }
    }

    private static String convertInputStreamToString(final InputStream inputStream) throws IOException {
        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
        String line = "";
        String result = "";
        StringBuffer buf = new StringBuffer();
        while ((line = bufferedReader.readLine()) != null) {
            buf.append(line);
        }
        result = buf.toString();
        inputStream.close();
        return result;

    }

    //TODO Complete validate method DataMessage object and place
//    private boolean validate(){
//        if(message.getLatitude() ==  Double.isNaN() )
//            return false;
//        else if(etCountry.getText().toString().trim().equals(""))
//            return false;
//        else if(etTwitter.getText().toString().trim().equals(""))
//            return false;
//        else
//            return true;
    //endregion SendJSON

    /**
     * @param keyCode
     * @param event
     * @return
     */
    //region SOS_BUTTON
    @Override
    public boolean onKeyDown(final int keyCode, final KeyEvent event) {
        switch (keyCode) {
            case KeyEvent.KEYCODE_VOLUME_UP:
                event.startTracking();
                saveLocationToFile();
                //TODO Input Server URL here in future
                new HttpAsyncTask().execute("172.16.0.88");
                return true;
            default: break;
        }
        return super.onKeyDown(keyCode, event);
    }
    //endregion SOS_BUTTON
}