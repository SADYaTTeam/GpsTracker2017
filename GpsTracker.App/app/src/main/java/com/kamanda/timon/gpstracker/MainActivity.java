
package com.kamanda.timon.gpstracker;

import android.app.Activity;
import android.content.Intent;
import android.os.Environment;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;
import android.provider.Settings.Secure;


import java.io.File;
import java.io.FileOutputStream;

public class MainActivity extends Activity {

    //region Variables
    private static final String TAG = "MainActivity";
    private static String deviceId;
    private DataMessage message;
    private Button button;
    //endregion Variables

    //region Activity
    @Override
    protected void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.i(TAG, "onCreate");
        setContentView(R.layout.myscreen);

        message = new DataMessage();
        deviceId = Secure.getString(this.getContentResolver(),
                Secure.ANDROID_ID);
        Log.i("deviceId", deviceId.toString());
        saveAndroidIdToFile();
        startService(new Intent(this, MyService.class));
        Intent i = new Intent(getApplicationContext(), SettingsActivity.class);
        i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        getApplicationContext().startActivity(i);
        //minimizeApp();


    }

    @Override
    protected void onStart() {
        Log.i(TAG, "onStart");
        super.onStart();
    }

    @Override
    protected void onStop() {
        Log.i(TAG, "onStop");
        super.onStop();
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
                    //TODO Send SOS message in JSON to server
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

    public void dumbFuck(View v){
        button = (Button) findViewById(R.id.buttonSettings);
        button.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Intent i = new Intent(getApplicationContext(), SettingsActivity.class);
                i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                getApplicationContext().startActivity(i);
            }
        });
    }
}
