
package com.kamanda.timon.gpstracker;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/**
 * Created by timon on 25.05.2017.
 */

public class StartMyActivityAtBootReceiver extends BroadcastReceiver {
    /**
     * @param context
     * @param intent
     */
    @Override
    public void onReceive(final Context context, final Intent intent) {

        Intent i = new Intent(context, MapsActivity.class);
        i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        context.startActivity(i);

    }
}
