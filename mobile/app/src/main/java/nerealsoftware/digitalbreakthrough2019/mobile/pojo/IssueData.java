package nerealsoftware.digitalbreakthrough2019.mobile.pojo;

import android.util.Base64;

import com.google.gson.annotations.SerializedName;

import org.json.JSONArray;

import java.util.List;

public class IssueData {

    @SerializedName("CategoryId")
    private int category;

    @SerializedName("Latitude")
    private double latitude;

    @SerializedName("Longitude")
    private double longitude;

    @SerializedName("Photo")
    private String photoDecoded;

    @SerializedName("Comment")
    private String comment;

    @SerializedName("Tags")
    private String tags;

    public IssueData(int category, byte[] photo, double lat, double lon, String comment, List<Integer> tags)
    {
        this.category = category;
        this.latitude = lat;
        this.longitude = lon;
        this.photoDecoded = Base64.encodeToString(photo, Base64.DEFAULT);
        this.comment = comment;
        this.tags = new JSONArray(tags).toString();
    }
}
