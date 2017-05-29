package com.kamanda.timon.gpstracker;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.support.annotation.Nullable;
import android.util.Log;

/**
 * Created by dasha_000 on 29.05.2017.
 */

public class MyService extends Service {

    final String LOG_TAG = "serviceLogs";
    private MyMapListener mapListener;

    void startListener() {
        mapListener = new MyMapListener(getApplicationContext());
    }

    @Override
    public void onCreate(){
        super.onCreate();
        Log.i(LOG_TAG, "OnCreate");

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.i(LOG_TAG, "onStartCommand");
        startListener();
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
