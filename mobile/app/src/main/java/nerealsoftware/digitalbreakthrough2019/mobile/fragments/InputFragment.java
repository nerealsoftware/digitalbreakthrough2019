package nerealsoftware.digitalbreakthrough2019.mobile.fragments;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.PorterDuff;
import android.graphics.drawable.BitmapDrawable;
import android.location.Location;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import androidx.fragment.app.Fragment;

import java.io.ByteArrayOutputStream;
import java.util.ArrayList;
import java.util.List;

import co.lujun.androidtagview.TagContainerLayout;
import co.lujun.androidtagview.TagView;
import nerealsoftware.digitalbreakthrough2019.mobile.Const;
import nerealsoftware.digitalbreakthrough2019.mobile.MainActivity;
import nerealsoftware.digitalbreakthrough2019.mobile.NetworkService;
import nerealsoftware.digitalbreakthrough2019.mobile.R;
import nerealsoftware.digitalbreakthrough2019.mobile.pojo.Category;
import nerealsoftware.digitalbreakthrough2019.mobile.pojo.IssueData;
import nerealsoftware.digitalbreakthrough2019.mobile.pojo.Tag;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class InputFragment extends Fragment {
    private static final String TAG = "InputFragment";
    private static final int REQUEST_IMAGE_CAPTURE = 1;


    private Spinner categories;
    private TagContainerLayout tags;

    private ImageView photo;
    private Button photoButton;
    private TextView comment;
    private Button sendButton;

    private int selectedCategory = -1;

    public static InputFragment newInstance() {
        return new InputFragment();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle state) {
        View rootView = inflater.inflate(R.layout.fragment_input_content, container, false);

        categories = rootView.findViewById(R.id.spinnerCategory);
        tags = rootView.findViewById(R.id.tagsLayout);

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

        // инициализация категорий
        NetworkService.getInstance().api().getCategories().enqueue(new Callback<List<Category>>() {
            @Override
            public void onResponse(Call<List<Category>> call, Response<List<Category>> response) {
                if (response.body() == null) return;

                int count = response.body().size();
                String[] arraySpinner = new String[count];
                for (int i = 0; i < count; i++) {
                    arraySpinner[i] = response.body().get(i).toString();
                }

                ArrayAdapter<String> adapter = new ArrayAdapter<String>(context, android.R.layout.simple_spinner_item, arraySpinner);
                adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                categories.setAdapter(adapter);

            }

            @Override
            public void onFailure(Call<List<Category>> call, Throwable t) {
                Toast.makeText(context, "Ошибка при получении категорий: " + t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

        categories.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                // запрос тэгов по категории.
                // TODO: Надо брать id из самой категории, а не позиция в списке + 1, но для хакатона и так сойдет
                selectedCategory = i + 1;
                NetworkService.getInstance().api().getTags(selectedCategory).enqueue(new Callback<List<Tag>>() {
                    @Override
                    public void onResponse(Call<List<Tag>> call, Response<List<Tag>> response) {
                        if (response.body() == null) return;

                        int count = response.body().size();
                        String[] arrayTags = new String[count];
                        for (int i = 0; i < count; i++) {
                            arrayTags[i] = response.body().get(i).toString();
                        }

                        tags.setTags(arrayTags);
                    }

                    @Override
                    public void onFailure(Call<List<Tag>> call, Throwable t) {

                    }
                });
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });


        // выбор категорий к отправке
        // TODO: можно только выбрать
        tags.setOnTagClickListener(new TagView.OnTagClickListener() {

            @Override
            public void onTagClick(int position, String text) {
                tags.getTagView(position).setTagBackgroundColor(getResources().getColor(R.color.colorAccent));
            }

            @Override
            public void onTagLongClick(final int position, String text) {
            }

            @Override
            public void onSelectedTagDrag(int position, String text) {

            }

            @Override
            public void onTagCrossClick(int position) {
            }
        });


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

        // отправка запроса
        sendButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String sessionId = ((MainActivity)context).getSessionId();
                if (sessionId == null) {
                    Toast.makeText(context, "Отправка невозможна, не определена сессия", Toast.LENGTH_SHORT).show();
                    return;
                }

                if (selectedCategory < 0) {
                    Toast.makeText(context, "Отправка невозможна, не определена категория", Toast.LENGTH_SHORT).show();
                    return;
                }


                // сжатие фотки перед отправкой
                Bitmap bitmap = ((BitmapDrawable)photo.getDrawable()).getBitmap();
                ByteArrayOutputStream baos = new ByteArrayOutputStream();
                bitmap.compress(Bitmap.CompressFormat.JPEG, 95, baos);

                // передача координат места отправки сообщения
                double lat = Const.DEF_LAT;
                double lon = Const.DEF_LON;
                Location location = ((MainActivity)context).currentLocation;
                if (location != null) {
                    lat = location.getLatitude();
                    lon = location.getLongitude();
                }

                List<String> tagValues = tags.getTags();
                List<Integer> tagIds = new ArrayList<>();
                for (int i = 0; i < tagValues.size() ; i++) {
                    int color = tags.getTagView(i).getTagBackgroundColor();
                    if (color == -44462) {
                        tagIds.add(i);
                    }
                    Log.d("NEREAL_POST", "tag = " + i + " color = " + color);
                }


                NetworkService.getInstance().api().addIssue(sessionId,
                        new IssueData(selectedCategory, baos.toByteArray(),
                                lat, lon, comment.getText().toString(), tagIds))
                        .enqueue(new Callback<IssueData>() {
                    @Override
                    public void onResponse(Call<IssueData> call, Response<IssueData> response) {
                        Log.d("NEREAL_POST", "response = " + response.raw().toString());

                        Toast.makeText(context, "хоба!", Toast.LENGTH_SHORT).show();
                    }

                    @Override
                    public void onFailure(Call<IssueData> call, Throwable t) {
                        Log.d("NEREAL_POST", "onFailure = " + t.getMessage());
                        Toast.makeText(context, "Ошибка при отправке обращения: " + t.getMessage(), Toast.LENGTH_SHORT).show();
                    }
                });
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
