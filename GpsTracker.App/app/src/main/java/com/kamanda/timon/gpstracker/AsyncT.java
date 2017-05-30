
package com.kamanda.timon.gpstracker;

import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;

import static android.R.id.message;

/**
 * Created by timon on 29.05.2017.
 */

class AsyncT extends AsyncTask<Void, Void, Void> {
    private double latitude;
    private double longitude;

    @Override
    protected Void doInBackground( Void... params) {

        try {
            URL url = new URL("http://gpstrackerservice.azurewebsites.net/api/app"); //Enter URL here
            HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
            httpURLConnection.setDoOutput(true);
            httpURLConnection.setRequestMethod("POST"); // here you are telling that it is a POST request, which can be changed into "PUT", "GET", "DELETE" etc.
            httpURLConnection.setRequestProperty("Content-Type", "application/json"); // here you are setting the `Content-Type` for the data you are sending which is `application/json`
            httpURLConnection.connect();

            JSONObject jsonObject = new JSONObject();

            jsonObject.put("Latitude", latitude);
            jsonObject.put("Longitude", longitude);
            //TODO Put real deviceId in JSON here
            jsonObject.put("DeviceId", "e085e0245a8c654f");
            jsonObject.put("Type", 1);
            Log.i("JSON_Array", jsonObject.toString());

            OutputStream temp = httpURLConnection.getOutputStream();
            DataOutputStream wr = new DataOutputStream(temp);



            try {
                wr.writeBytes(jsonObject.toString(2));
                int ResponceCode = httpURLConnection.getResponseCode();
                Log.i("Responce", "" + ResponceCode + " " + httpURLConnection.getResponseMessage().toString());
                Log.i("Responce2", httpURLConnection.getHeaderFieldKey(0) + " " + httpURLConnection.getHeaderField(0));
                Log.i("Responce3", httpURLConnection.getHeaderFieldKey(1) + " " + httpURLConnection.getHeaderField(1));
                Log.i("Responce4", httpURLConnection.getHeaderFieldKey(2) + " " + httpURLConnection.getHeaderField(2));
                Log.i("Responce5", httpURLConnection.getHeaderFieldKey(3) + " " + httpURLConnection.getHeaderField(3));
                Log.i("Responce6", httpURLConnection.getHeaderFieldKey(4) + " " + httpURLConnection.getHeaderField(4));
            } catch (IOException e) {
                e.printStackTrace();
                Log.e("PostJSON", e.getMessage().toString());
                Log.i("PostJSON", "Length: " + wr.size());
            }

            wr.flush();
            wr.close();
//            httpURLConnection.disconnect();

        } catch (MalformedURLException e) {
            e.printStackTrace();
            Log.e("AsyncT", e.getMessage(), e);
        } catch (IOException e) {
            e.printStackTrace();
            Log.e("AsyncT", e.getMessage(), e);
        } catch (JSONException e) {
            e.printStackTrace();
            Log.e("AsyncT", e.getMessage(), e);
        }

        return null;
    }

    public void setData(final double latitude, final double longitude) {
        this.latitude = latitude;
        this.longitude = longitude;
    }

}