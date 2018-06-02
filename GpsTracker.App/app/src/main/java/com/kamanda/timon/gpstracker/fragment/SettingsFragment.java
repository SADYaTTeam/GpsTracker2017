package com.kamanda.timon.gpstracker.fragment;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import com.kamanda.timon.gpstracker.R;
import com.kamanda.timon.gpstracker.SendLocationToUrlService;

import java.util.Objects;

public class SettingsFragment extends Fragment {
    private static final int LAYOUT = R.layout.fragment_settings;

    private View view;

    private EditText editTextUpdateInterval;
    private EditText editTextFatestInterval;
    private EditText editTextDisplacement;
    private Button buttonSaveSettings;

    private int updateInterval;
    private int fatestInterval;
    private int displacement;

    public  static SettingsFragment getInstanse(){
        Bundle args = new Bundle();
        SettingsFragment fragment = new SettingsFragment();
        fragment.setArguments(args);

        return fragment;
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        view = inflater.inflate(LAYOUT,container,false);
        return view;
    }

    public void ApplyChanges(View v) {
        editTextUpdateInterval = (EditText) getView().findViewById(R.id.editTextUpdateInterval);
        editTextFatestInterval = (EditText) getView().findViewById(R.id.editTextFatestInterval);
        editTextDisplacement = (EditText) getView().findViewById(R.id.editTextDisplacement);
        buttonSaveSettings = (Button) getView().findViewById(R.id.buttonSettingsSave);
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
        getActivity().stopService(new Intent(getActivity(), SendLocationToUrlService.class));
        Intent intent = new Intent(getActivity(), SendLocationToUrlService.class);
        intent.putExtra("updateInterval", updateInterval);
        intent.putExtra("fatestInterval", fatestInterval);
        intent.putExtra("displacement", displacement);
        getActivity().startService(intent);
    }
}
