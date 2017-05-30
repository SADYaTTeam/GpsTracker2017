
package com.kamanda.timon.gpstracker;

import android.Manifest;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Environment;
import android.os.IBinder;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;

/**
 * Created by dasha_000 on 29.05.2017.
 */

public class MyService extends Service {

    final String LOG_TAG = "serviceLogs";
    private MyMapListener mapListener;
    private String deviceId;
    void createJson() {
        AsyncT asyncT = new AsyncT();
        asyncT.setData(mapListener.getDataMessage().getLatitude(), mapListener.getDataMessage().getLongtitude(),deviceId);
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
        }
        catch (IOException e) {
            //You'll need to add proper error handling here
        }

        deviceId = text.toString();



    }

    @Override
    public void onCreate(){
        super.onCreate();
        Log.i(LOG_TAG, "OnCreate");
        mapListener = new MyMapListener(getApplicationContext());
        loadAndroidIdFromFile();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.i(LOG_TAG, "onStartCommand");
        createJson();
        return super.onStartCommand(intent, flags, startId);
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        Log.i(LOG_TAG, "onDestroy");
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        Log.i(LOG_TAG, "onBind");
        return null;
    }

}
