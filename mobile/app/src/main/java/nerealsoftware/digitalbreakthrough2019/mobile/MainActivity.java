package nerealsoftware.digitalbreakthrough2019.mobile;

import android.Manifest;
import android.content.Context;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;

import androidx.core.app.ActivityCompat;
import androidx.core.view.GravityCompat;
import androidx.appcompat.app.ActionBarDrawerToggle;

import android.util.Log;
import android.view.MenuItem;

import com.google.android.material.navigation.NavigationView;

import androidx.drawerlayout.widget.DrawerLayout;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;

import android.view.Menu;
import android.widget.Toast;

import org.osmdroid.config.Configuration;

import nerealsoftware.digitalbreakthrough2019.mobile.fragments.DebugFragment;
import nerealsoftware.digitalbreakthrough2019.mobile.fragments.InputFragment;
import nerealsoftware.digitalbreakthrough2019.mobile.fragments.MapFragment;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener,
        LocationListener {


    private String sessionId = null;
    private LocationManager lm;
    public Location currentLocation = null;

    public String getSessionId()
    {
        return sessionId;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        NavigationView navigationView = findViewById(R.id.nav_view);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();
        navigationView.setNavigationItemSelectedListener(this);

        // для работы osmdroid надо передавать им юзер-агент
        Configuration.getInstance().setUserAgentValue(BuildConfig.APPLICATION_ID);
        ActivityCompat.requestPermissions(MainActivity.this,
                new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE, Manifest.permission.ACCESS_FINE_LOCATION}, 1);
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        Fragment fragment = null;

        int id = item.getItemId();

        if (id == R.id.nav_home) {
            fragment = InputFragment.newInstance();

        } else if (id == R.id.nav_gallery) {
            fragment = MapFragment.newInstance();

        } else if (id == R.id.nav_debug) {
            fragment = DebugFragment.newInstance();

        } else if (id == R.id.nav_share) {

        } else if (id == R.id.nav_send) {

        }

        FragmentManager fragmentManager = getSupportFragmentManager();
        if (fragment != null) {
            fragmentManager.beginTransaction().replace(R.id.content_frame, fragment).commit();
        }

        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    @Override
    public void onPause() {
        super.onPause();
        try {
            lm.removeUpdates(this);
        }
        catch (Exception ex){}
    }

    @Override
    public void onResume(){
        super.onResume();
        lm = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
        try {
            //this fails on AVD 19s, even with the appcompat check, says no provided named gps is available
            lm.requestLocationUpdates(LocationManager.GPS_PROVIDER,0l,0f,this);
        }
        catch (Exception ex) {
            Log.w("NEREAL_GPS", "requestLocationUpdates GPS_PROVIDER failed with " + ex.getMessage());
        }


        try {
            lm.requestLocationUpdates(LocationManager.NETWORK_PROVIDER,0l,0f,this);
        }
        catch (Exception ex) {
            Log.w("NEREAL_GPS", "requestLocationUpdates NETWORK_PROVIDER failed with " + ex.getMessage());
        }
    }
    @Override
    public void onLocationChanged(Location location) {
        currentLocation = location;
        Toast.makeText(this, "определено местоположение: " + currentLocation.getLatitude() + " " + currentLocation.getLongitude(), Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onStatusChanged(String s, int i, Bundle bundle) {

    }

    @Override
    public void onProviderEnabled(String s) {

    }

    @Override
    public void onProviderDisabled(String s) {

    }

}
