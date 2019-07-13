package nerealsoftware.digitalbreakthrough2019.mobile;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.fragment.app.Fragment;

public class InputFragment extends Fragment {
    private static final String TAG = "InputFragment";

    private Spinner categories;
    private TextView comment;

    public static InputFragment newInstance() {
        return new InputFragment();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle state) {
        View rootView = inflater.inflate(R.layout.fragment_input_content, container, false);

        categories = rootView.findViewById(R.id.spinnerCategory);
        comment = rootView.findViewById(R.id.editComment);

        return rootView;
    }

    @Override
    public void onActivityCreated(Bundle state) {
        super.onActivityCreated(state);

        String[] arraySpinner = new String[] {
                "Дороги", "Тротуары", "Мусорная свалка", "Прочее..."
        };


        ArrayAdapter<String> adapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, arraySpinner);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        categories.setAdapter(adapter);
    }

    @Override
    public void onSaveInstanceState(Bundle state) {
        super.onSaveInstanceState(state);
        //state.putCharSequence(DB.VALUE, valueTV.getText());
        //state.putInt(DB.CATEGORY, categories.getSelected());
    }


    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {

    }

}
