package nerealsoftware.digitalbreakthrough2019.mobile.pojo;

import com.google.gson.annotations.SerializedName;

public class Category {

    @SerializedName("Id")
    private int id;

    @SerializedName("Name")
    private String name ;

    @Override
    public String toString() {
        return name;
    }
}
