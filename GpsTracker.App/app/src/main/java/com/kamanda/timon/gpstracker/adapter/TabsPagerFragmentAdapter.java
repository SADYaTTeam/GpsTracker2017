package com.kamanda.timon.gpstracker.adapter;

import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

import com.kamanda.timon.gpstracker.fragment.ExampleFragment;
import com.kamanda.timon.gpstracker.fragment.MapViewFragment;
import com.kamanda.timon.gpstracker.fragment.SettingsFragment;

public class TabsPagerFragmentAdapter extends FragmentPagerAdapter{

    private String[] tabs;

    public TabsPagerFragmentAdapter(FragmentManager fm) {
        super(fm);
        tabs = new String[]{
                "Карта",
                "Налаштування"
        };
    }

    @Override
    public CharSequence getPageTitle(int position) {
        return tabs[position];
    }

    @Override
    public Fragment getItem(int position) {
        switch (position) {
            case  0:
                return MapViewFragment.getInstanse();
            case  1:
                return SettingsFragment.getInstanse();
        }
        return null;
    }

    @Override
    public int getCount() {
        return tabs.length;
    }
}
