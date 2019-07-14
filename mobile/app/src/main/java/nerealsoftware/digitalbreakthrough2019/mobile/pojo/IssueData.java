package nerealsoftware.digitalbreakthrough2019.mobile.pojo;

import android.util.Base64;

import com.google.gson.annotations.SerializedName;

public class IssueData {

    @SerializedName("CategoryId")
    private int category;

    // TODO: Position

    @SerializedName("Photo")
    private String photoDecoded;

    @SerializedName("Comment")
    private String comment;

    //TODO: Tags;

    public IssueData(int category, byte[] photo, String comment)
    {
        this.category = category;
        this.photoDecoded = Base64.encodeToString(photo, Base64.DEFAULT);
        this.comment = comment;
    }
}
