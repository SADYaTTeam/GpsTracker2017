
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
//            DataMessage message = new DataMessage();
//            message.setLatitude(49.840466);
//            message.setLongitude(24.027845);
//            message.setDeviceId("0123456789ABCDEF");
            String latitude = "49.840466";
            String longtitude = "24.027845";
            String deviceId = "0123456789ABCDEF";
            jsonObject.put("latitude", latitude);
            jsonObject.put("longtitude", longtitude);
            jsonObject.put("deviceId", deviceId);


            DataOutputStream wr = new DataOutputStream(httpURLConnection.getOutputStream());
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

}