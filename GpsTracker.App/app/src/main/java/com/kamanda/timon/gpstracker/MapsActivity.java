package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.app.Activity;
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

public class MapsActivity extends FragmentActivity implements OnMapReadyCallback,
        GoogleApiClient.ConnectionCallbacks,
        GoogleApiClient.OnConnectionFailedListener,
        GoogleMap.OnMarkerDragListener,
        GoogleMap.OnMapLongClickListener,
        GoogleMap.OnMarkerClickListener,
        View.OnClickListener {

    //region Variables
    private static final String TAG = "MapsActivity";
    private GoogleMap mMap;
    //  private double longitude;
    //  private double latitude;
    private GoogleApiClient googleApiClient;
    private DataMessage _message;
    private MyMapListener _mapListener;
    private Location _mLocation;
    private String _deviceId;

    //endregion Variables



    //region Activity
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_maps);


        _mapListener = new MyMapListener(getApplicationContext());
        _message = new DataMessage();
        _deviceId = Secure.getString(this.getContentResolver(),
                Secure.ANDROID_ID);
        _mapListener.setDeviceId(_deviceId);

      //  _mLocation = _mapListener.getLocation();

//        _message.set_latitude(_mLocation.getLatitude());
//        _message.set_longitude(_mLocation.getLongitude());

        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(map);
        mapFragment.getMapAsync(this);

        //Initializing googleApiClient
        googleApiClient = new GoogleApiClient.Builder(this)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API)
                .build();
    }

    @Override
    protected void onStart() {
        googleApiClient.connect();
        super.onStart();


    }

    @Override
    protected void onStop() {
        googleApiClient.disconnect();
        super.onStop();
    }
    //endregion Activity

    //region Google Map Initialization And Event Properties
    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;
        mMap.setMapType(GoogleMap.MAP_TYPE_NORMAL);
        // googleMapOptions.mapType(googleMap.MAP_TYPE_HYBRID)
        //    .compassEnabled(true);

        // Add a marker in Lviv and move the camera

      //  LatLng latLng = new LatLng(49.840466, 24.027845);
        LatLng latLng = new LatLng(_message.get_latitude(), _message.get_longtitude());
        mMap.addMarker(new MarkerOptions().position(latLng).title("Current Position"));
        mMap.moveCamera(CameraUpdateFactory.newLatLng(latLng));
        mMap.animateCamera(CameraUpdateFactory.zoomTo(15));
        mMap.setOnMarkerDragListener(this);
        mMap.setOnMapLongClickListener(this);

    }

    private void moveMap() {
        /**
         * Creating the latlng object to store lat, long coordinates
         * adding marker to map
         * move the camera with animation
         */
        LatLng latLng = new LatLng(_message.get_latitude(), _message.get_longtitude());
        mMap.addMarker(new MarkerOptions()
                .position(latLng)
                .draggable(false)
                .title("New Marker"));
        mMap.moveCamera(CameraUpdateFactory.newLatLng(latLng));
        mMap.animateCamera(CameraUpdateFactory.zoomTo(15));
        mMap.getUiSettings().setZoomControlsEnabled(true);
        saveLocationToFile();


    }

    @Override
    public void onClick(View view) {
        Log.v(TAG,"view click event");
    }

    @Override
    public void onConnected(@Nullable Bundle bundle) {

        getCurrentLocation();
        moveMap();
    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }

    @Override
    public void onMapLongClick(LatLng latLng) {
        // mMap.clear();
        mMap.addMarker(new MarkerOptions().position(latLng).draggable(true));
    }

    @Override
    public void onMarkerDragStart(Marker marker) {
        Toast.makeText(MapsActivity.this, "onMarkerDragStart", Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onMarkerDrag(Marker marker) {
        Toast.makeText(MapsActivity.this, "onMarkerDrag", Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onMarkerDragEnd(Marker marker) {
        // getting the Co-ordinates
        ///latitude = marker.getPosition().latitude;
       // longitude = marker.getPosition().longitude;

        //move to current position
        moveMap();
    }

    @Override
    public boolean onMarkerClick(Marker marker) {
        Toast.makeText(MapsActivity.this, "onMarkerClick", Toast.LENGTH_SHORT).show();
        return true;
    }
    //endregion Google Map Initialization And Event Properties

    //region Get current location & move to this marker

    //Get current location
    private void getCurrentLocation() {
        mMap.clear();
        if (ActivityCompat.checkSelfPermission(this, android.Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, android.Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return;
        }
        Location location = LocationServices.FusedLocationApi.getLastLocation(googleApiClient);
        if (location != null) {
            //Getting longitude and latitude
            _message.set_longitude(location.getLongitude());
            _message.set_latitude(location.getLatitude());
            Log.i("GPS_Coordinates_Start", _message.get_latitude() + "; " + _message.get_longtitude());

            //moving the map to location
            moveMap();
        }
    }

    private String getCurrentLocationString() {
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.

        }
        Location location = LocationServices.FusedLocationApi.getLastLocation(googleApiClient);
        if (location != null) {
            //Getting longitude and latitude
            _message.set_longitude(location.getLongitude());
            _message.set_latitude(location.getLatitude());
        }
        return _message.get_latitude() + "; " + _message.get_longtitude();
    }
    //endregion Get current location

    //region Save location to file

    //Saving current location to file
    private void saveLocationToFile() {
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

            String filename = "MyLocation.txt";
//            String _deviceId = Secure.getString(this.getContentResolver(),
//                Secure.ANDROID_ID);

            String sData = getCurrentLocationString() + " \nDevice id:" + _deviceId;


        String root = Environment.getExternalStorageDirectory().toString();
        File myDir = new File(root + "/savedCoordinates");
        myDir.mkdirs();
        File file = new File (myDir, filename);
        if (file.exists ()) file.delete ();
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

    //check if you are connected or not
    private void CheckConnection() {
        try {
            if (isConnected()) {
                Toast.makeText(getBaseContext(), "You are connected", Toast.LENGTH_LONG).show();
            } else {
                Toast.makeText(getBaseContext(), "You are NOT connected", Toast.LENGTH_LONG).show();
            }
        } catch (Exception ex) {

        }

    }

    public static String POST(String url, DataMessage message){
        InputStream inputStream = null;
        String result = "";
        try {

            // 1. create HttpClient
            HttpClient httpclient = new DefaultHttpClient();

            // 2. make POST request to the given URL
            HttpPost httpPost = new HttpPost(url);

            String json = "";

            // 3. build jsonObject
            JSONObject jsonObject = new JSONObject();
            jsonObject.accumulate("longtitude", message.get_longtitude() );
            jsonObject.accumulate("latitude", message.get_latitude());
            jsonObject.accumulate("_deviceId", message.get_deviceId());

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

            // 8. Execute POST request to the given URL
            HttpResponse httpResponse = httpclient.execute(httpPost);

            // 9. receive response as inputStream
            inputStream = httpResponse.getEntity().getContent();

            // 10. convert inputstream to string
            if(inputStream != null)
                result = convertInputStreamToString(inputStream);
            else
                result = "Did not work!";

        } catch (Exception e) {
            Log.d("InputStream", e.getLocalizedMessage());
        }

        // 11. return result
        return result;
    }

    public boolean isConnected(){
        ConnectivityManager connMgr = (ConnectivityManager) getSystemService(Activity.CONNECTIVITY_SERVICE);
        NetworkInfo networkInfo = connMgr.getActiveNetworkInfo();
        if (networkInfo != null && networkInfo.isConnected())
            return true;
        else
            return false;
    }

    private class HttpAsyncTask extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(String... urls) {

            return POST(urls[0], _mapListener.getDataMessage());
        }
        // onPostExecute displays the results of the AsyncTask.
        @Override
        protected void onPostExecute(String result) {
            Toast.makeText(getBaseContext(), "Data Sent!", Toast.LENGTH_LONG).show();
        }
    }

    private static String convertInputStreamToString(InputStream inputStream) throws IOException {
        BufferedReader bufferedReader = new BufferedReader( new InputStreamReader(inputStream));
        String line = "";
        String result = "";
        while((line = bufferedReader.readLine()) != null)
            result += line;

        inputStream.close();
        return result;

    }

    //TODO Complete validate DataMessage object
//    private boolean validate(){
//        if(_message.get_latitude() ==  Double.isNaN() )
//            return false;
//        else if(etCountry.getText().toString().trim().equals(""))
//            return false;
//        else if(etTwitter.getText().toString().trim().equals(""))
//            return false;
//        else
//            return true;
    //endregion SendJSON

    //region SOS_BUTTON
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        switch(keyCode){
            case KeyEvent.KEYCODE_VOLUME_UP:
                event.startTracking();
                saveLocationToFile();
                //TODO Input Server URL here in future
                new HttpAsyncTask().execute("172.16.0.88");
                return true;
        }
        return super.onKeyDown(keyCode, event);
    }
    //endregion SOS_BUTTON
}