package nerealsoftware.digitalbreakthrough2019.mobile.fragments;

import android.location.Location;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.fragment.app.Fragment;

import org.osmdroid.api.IMapController;
import org.osmdroid.tileprovider.tilesource.TileSourceFactory;
import org.osmdroid.util.GeoPoint;
import org.osmdroid.views.MapView;
import org.osmdroid.views.overlay.Marker;

import nerealsoftware.digitalbreakthrough2019.mobile.Const;
import nerealsoftware.digitalbreakthrough2019.mobile.MainActivity;
import nerealsoftware.digitalbreakthrough2019.mobile.R;

public class MapFragment extends Fragment {
    private static final String TAG = "MapFragment";

    MapView map = null;

    public static MapFragment newInstance() {
        return new MapFragment();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle state) {
        View rootView = inflater.inflate(R.layout.fragment_map_content, container, false);

        map = rootView.findViewById(R.id.map);
        return rootView;
    }

    @Override
    public void onActivityCreated(Bundle state) {
        super.onActivityCreated(state);

        map.setTileSource(TileSourceFactory.MAPNIK);
        map.setHasTransientState(true);
        map.setMultiTouchControls(true);

        IMapController mapController = map.getController();
        mapController.setZoom(15.0);

        // координаты корпуса политеха по-умолчанию
        GeoPoint startPoint = new GeoPoint(Const.DEF_LAT, Const.DEF_LON);

        // определенное местоположение (если успешно)
        Location location = ((MainActivity)getActivity()).currentLocation;
        if (location != null) {
            startPoint.setLatitude(location.getLatitude());
            startPoint.setLongitude(location.getLongitude());
            //startPoint.setAltitude(location.getAltitude());
        }


        mapController.setCenter(startPoint);

        Marker m = new Marker(map);
        m.setPosition(startPoint);
        m.setAnchor(Marker.ANCHOR_CENTER, Marker.ANCHOR_BOTTOM);
        m.setIcon(getResources().getDrawable(R.mipmap.pin));
        m.setTitle("Вы находитесь здесь");
        map.getOverlays().add(m);

    }

    @Override
    public void onSaveInstanceState(Bundle state) {
        super.onSaveInstanceState(state);
    }

    public void onResume(){
        super.onResume();
        map.onResume();
    }

    public void onPause(){
        super.onPause();
        map.onPause();
    }
}
