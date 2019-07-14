package nerealsoftware.digitalbreakthrough2019.mobile.fragments;

import android.content.Context;
import android.graphics.Color;
import android.location.Location;
import android.os.Bundle;
import android.provider.Settings;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.fragment.app.Fragment;

import nerealsoftware.digitalbreakthrough2019.mobile.MainActivity;
import nerealsoftware.digitalbreakthrough2019.mobile.R;

public class DebugFragment extends Fragment {
    private static final String TAG = "DebugFragment";

    private TextView phone;
    private TextView session;
    private TextView coordinates;

    public static DebugFragment newInstance() {
        return new DebugFragment();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle state) {
        View rootView = inflater.inflate(R.layout.fragment_debug_content, container, false);

        phone = rootView.findViewById(R.id.debug_phoneid);
        session = rootView.findViewById(R.id.debug_session);
        coordinates = rootView.findViewById(R.id.debug_coordinates);
        return rootView;
    }

    @Override
    public void onActivityCreated(Bundle state) {
        super.onActivityCreated(state);
        final Context context = getContext();

        String android_id = Settings.Secure.getString(context.getContentResolver(), Settings.Secure.ANDROID_ID);
        phone.setTextColor(Color.BLACK);
        phone.setText(String.format("DeviceId = %s", android_id));

        String sessionId = ((MainActivity)context).getSessionId();
        if (sessionId == null) {
            session.setTextColor(context.getResources().getColor(R.color.colorAccent));
            session.setText("сессия не получена");
        }
        else {
            session.setTextColor(Color.BLACK);
            session.setText(String.format("SessionId = %s", sessionId));
        }

        // определение координат (отладка)
        Location location = ((MainActivity)context).currentLocation;
        if (location == null) {
            coordinates.setTextColor(context.getResources().getColor(R.color.colorAccent));
            coordinates.setText("местоположение не определилось");
        }
        else {
            coordinates.setTextColor(Color.BLACK);
            coordinates.setText(String.format("широта = %s; долгота = %s", location.getLatitude(), location.getLongitude()));
        }


    }
}
