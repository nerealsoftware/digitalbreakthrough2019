package nerealsoftware.digitalbreakthrough2019.mobile.pojo;

import com.google.gson.annotations.SerializedName;

public class Tag {

    @SerializedName("Id")
    private int id;

    @SerializedName("CategoryId")
    private int categoryId;

    @SerializedName("Name")
    private String name ;

    @Override
    public String toString() {
        return name;
    }
}
