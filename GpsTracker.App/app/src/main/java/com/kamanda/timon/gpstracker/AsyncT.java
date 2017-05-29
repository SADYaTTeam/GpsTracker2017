
package com.kamanda.timon.gpstracker;

import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.DataOutputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import static android.R.id.message;

/**
 * Created by timon on 29.05.2017.
 */

class AsyncT extends AsyncTask<Void, Void, Void> {
    private double latitude;
    private double longitude;

    @Override
    protected Void doInBackground(final Void... params) {

        try {
            URL url = new URL("http://gpstrackerservice.azurewebsites.net/api/app"); //Enter URL here
            HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
            httpURLConnection.setDoOutput(true);
            httpURLConnection.setRequestMethod("POST"); // here you are telling that it is a POST request, which can be changed into "PUT", "GET", "DELETE" etc.
            httpURLConnection.setRequestProperty("Content-Type", "application/json"); // here you are setting the `Content-Type` for the data you are sending which is `application/json`
            httpURLConnection.connect();

            JSONObject jsonObject = new JSONObject();

            jsonObject.put("latitude", latitude);
            jsonObject.put("longtitude", longitude);
            jsonObject.put("deviceId", "621hecq422xxs");
            jsonObject.put("messageId", 3);
            //TODO Put deviceId in JSON here
          //  jsonObject.put("deviceId", deviceId);
            Log.i("JSON_Array", jsonObject.toString());

            DataOutputStream wr = new DataOutputStream(httpURLConnection.getOutputStream());
            int ResponceCode = httpURLConnection.getResponseCode();
            Log.i("Responce", "" + ResponceCode + " " + httpURLConnection.getResponseMessage().toString());
            wr.writeBytes(jsonObject.toString());
            wr.flush();
            wr.close();

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