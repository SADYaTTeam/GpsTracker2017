
package com.kamanda.timon.gpstracker;

import android.app.Activity;
import android.app.NotificationManager;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Build;
import android.os.Environment;
import android.os.Bundle;
import android.os.IBinder;
import android.support.annotation.NonNull;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.app.NotificationCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import android.provider.Settings.Secure;


import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;

import java.io.File;
import java.io.FileOutputStream;
import java.util.Objects;

public class MainActivity extends AppCompatActivity {

    //region Variables
    private static final String TAG = "MainActivity";
    private static final int LAYOUT = R.layout.activity_main;

    private static String deviceId;
    private DataMessage message;
    private EditText editTextUpdateInterval;
    private EditText editTextFatestInterval;
    private EditText editTextDisplacement;
    private Button button;
    private Button buttonSaveSettings;

    private Toolbar toolbar;
    private DrawerLayout drawerLayout;

    private int updateInterval;
    private int fatestInterval;
    private int displacement;

    boolean mBounded;
    SendLocationToUrlService mServer;



    //endregion Variables

    //region Activity
    @Override
    protected void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.i(TAG, "onCreate");

        setContentView(LAYOUT);

        initToolbar();
        initNavigationView();

        message = new DataMessage();
        deviceId = Secure.getString(this.getContentResolver(),
                Secure.ANDROID_ID);
        Log.i("deviceId", deviceId.toString());
        sendDeviceIdNotification();
        //saveAndroidIdToFile();
        Intent intent = new Intent(this, SendLocationToUrlService.class);
        this.startService(intent);
        //startService(new Intent(this, SendLocationToUrlService.class));
        //minimizeApp();

//        if (savedInstanceState == null) {
//            // Begin the transaction
//            FragmentTransaction ft = getSupportFragmentManager().beginTransaction();
//            // Replace the contents of the container with the new fragment
//            ft.replace(R.id.container, new PlaceholderFragment());
//            // or ft.add(R.id.your_placeholder, new FooFragment());
//            // Complete the changes added above
//            ft.commit();
//        }
    }

    private void initToolbar() {
        toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle(R.string.app_name);
        toolbar.setOnMenuItemClickListener(new Toolbar.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                return false;
            }
        });

        toolbar.inflateMenu(R.menu.menu_navigation);
    }

    private void initNavigationView() {
        drawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {

            case R.id.map:
                // Do onlick on menu action here

                return true;
        }
        return false;
    }

    private void showMap(){

    }

    @Override
    protected void onStart() {
        Log.i(TAG, "onStart");
        super.onStart();
        Intent mIntent = new Intent(this, SendLocationToUrlService.class);
        bindService(mIntent, mConnection, BIND_AUTO_CREATE);
    }

    ServiceConnection mConnection = new ServiceConnection() {

        public void onServiceDisconnected(ComponentName name) {
            Toast.makeText(MainActivity.this, "Service is disconnected", Toast.LENGTH_SHORT).show();
            mBounded = false;
            mServer = null;
        }

        public void onServiceConnected(ComponentName name, IBinder service) {
            Toast.makeText(MainActivity.this, "Service is connected", Toast.LENGTH_SHORT).show();
            mBounded = true;
            SendLocationToUrlService.LocalBinder mLocalBinder = (SendLocationToUrlService.LocalBinder) service;
            mServer = mLocalBinder.getMyServiceInstance();
        }
    };


    @Override
    protected void onStop() {
        Log.i(TAG, "onStop");
        super.onStop();
        if (mBounded) {
            unbindService(mConnection);
            mBounded = false;
        }
        NotificationManager mNotificationManager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        mNotificationManager.cancel(001);

    }


    //endregion Activity

    private void saveAndroidIdToFile() {
        String filename = "MyAndroidId.txt";
        String sData = MainActivity.deviceId;
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

    public void minimizeApp() {
        Intent startMain = new Intent(Intent.ACTION_MAIN);
        startMain.addCategory(Intent.CATEGORY_HOME);
        startMain.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(startMain);
    }

    //region SOS_BUTTON
    @Override
    public boolean dispatchKeyEvent(final KeyEvent event) {
        Log.i(TAG, "dispatchKeyEvent");
        int action = event.getAction();
        int keyCode = event.getKeyCode();
        switch (keyCode) {
            case KeyEvent.KEYCODE_VOLUME_UP:
                if (action == KeyEvent.ACTION_DOWN) {
                    //TODO Send SOS message in JSON to mServer
                    try {
                        Toast.makeText(getApplicationContext(), "JSON sended to mServer",
                                Toast.LENGTH_LONG).show();
                        mServer.sendSOS_JSON();
                        // startService(new Intent(this, SendLocationToUrlService.class));
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

    public void ApplyChanges(View v) {
        editTextUpdateInterval = (EditText) findViewById(R.id.editTextUpdateInterval);
        editTextFatestInterval = (EditText) findViewById(R.id.editTextFatestInterval);
        editTextDisplacement = (EditText) findViewById(R.id.editTextDisplacement);
        buttonSaveSettings = (Button) findViewById(R.id.buttonSettingsSave);
        buttonSaveSettings.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
                    if (Objects.equals(editTextUpdateInterval.getText().toString(), "")) {
                        updateInterval = 10000;
                    } else {
                        updateInterval = Integer.valueOf(editTextUpdateInterval.getText().toString());
                    }
                }
                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
                    if (Objects.equals(editTextFatestInterval.getText().toString(), "")) {
                        fatestInterval = 5000;
                    } else {
                        fatestInterval = Integer.valueOf(editTextFatestInterval.getText().toString());
                    }
                }
                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
                    if (Objects.equals(editTextDisplacement.getText().toString(), "")) {
                        displacement = 50;
                    } else {
                        displacement = Integer.valueOf(editTextDisplacement.getText().toString());
                    }
                }

                StartMyService();
                Log.i("SETTINGS", "updateInterval: " + updateInterval + "fatestInterval: " + fatestInterval + "displacement: " + displacement);
            }
        });
    }

    public void StartMyService() {
        this.stopService(new Intent(this, SendLocationToUrlService.class));
        Intent intent = new Intent(this, SendLocationToUrlService.class);
        intent.putExtra("updateInterval", updateInterval);
        intent.putExtra("fatestInterval", fatestInterval);
        intent.putExtra("displacement", displacement);
        this.startService(intent);
    }


    public void sendDeviceIdNotification() {

        //Get an instance of NotificationManager//

        NotificationCompat.Builder mBuilder =
                new NotificationCompat.Builder(this)
                        .setSmallIcon(R.drawable.ic_notifications_black_24dp)
                        .setContentTitle("Device ID: ")
                        .setOngoing(true)
                        .setContentText(deviceId);


        // Gets an instance of the NotificationManager service//

        NotificationManager mNotificationManager =

                (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);

        //When you issue multiple notifications about the same type of event, it’s best practice for your app to try to update an existing notification with this new information, rather than immediately creating a new notification. If you want to update this notification at a later date, you need to assign it an ID. You can then use this ID whenever you issue a subsequent notification. If the previous notification is still visible, the system will update this existing notification, rather than create a new one. In this example, the notification’s ID is 001//

        mNotificationManager.notify(001, mBuilder.build());

    }
}

