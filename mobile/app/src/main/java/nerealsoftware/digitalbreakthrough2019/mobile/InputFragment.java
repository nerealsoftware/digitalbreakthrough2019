package nerealsoftware.digitalbreakthrough2019.mobile;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.PorterDuff;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.fragment.app.Fragment;

public class InputFragment extends Fragment {
    private static final String TAG = "InputFragment";
    private static final int REQUEST_IMAGE_CAPTURE = 1;


    private Spinner categories;
    private ImageView photo;
    private Button photoButton;
    private TextView comment;
    private Button sendButton;

    public static InputFragment newInstance() {
        return new InputFragment();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle state) {
        View rootView = inflater.inflate(R.layout.fragment_input_content, container, false);

        categories = rootView.findViewById(R.id.spinnerCategory);

        photo = rootView.findViewById(R.id.photo);
        photoButton = rootView.findViewById(R.id.buttonPhoto);

        comment = rootView.findViewById(R.id.editComment);

        sendButton = rootView.findViewById(R.id.buttonSend);
        sendButton.getBackground().setColorFilter(getResources().getColor(R.color.colorAccent), PorterDuff.Mode.MULTIPLY);

        return rootView;
    }

    @Override
    public void onActivityCreated(Bundle state) {
        super.onActivityCreated(state);

        final Context context = getContext();

        // инициализация категорий TODO: с сервера
        String[] arraySpinner = new String[] {
                "Дороги", "Тротуары", "Мусорная свалка", "Прочее..."
        };

        ArrayAdapter<String> adapter = new ArrayAdapter<String>(context, android.R.layout.simple_spinner_item, arraySpinner);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        categories.setAdapter(adapter);

        // прикрепление фотки
        photoButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                if (takePictureIntent.resolveActivity(context.getPackageManager()) != null) {
                    startActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE);
                }
            }
        });
    }

    @Override
    public void onSaveInstanceState(Bundle state) {
        super.onSaveInstanceState(state);
    }


    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == REQUEST_IMAGE_CAPTURE && resultCode == Activity.RESULT_OK) {
            Bundle extras = data.getExtras();
            Bitmap imageBitmap = (Bitmap) extras.get("data");
            photo.setImageBitmap(imageBitmap);
        }

    }

}
