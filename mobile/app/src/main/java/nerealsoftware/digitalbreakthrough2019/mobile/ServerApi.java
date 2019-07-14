package nerealsoftware.digitalbreakthrough2019.mobile;

import java.util.List;

import nerealsoftware.digitalbreakthrough2019.mobile.pojo.Category;
import nerealsoftware.digitalbreakthrough2019.mobile.pojo.IssueData;
import nerealsoftware.digitalbreakthrough2019.mobile.pojo.Tag;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface ServerApi {
    @GET("auth")
    Call<String> getSession(@Query("deviceId") String deviceId);

    @GET("category")
    Call<List<Category>> getCategories();

    @GET("tag")
    Call<List<Tag>> getTags(@Query("categoryId") int category);

    @POST("issue")
    Call<IssueData> addIssue(@Query("sessionId") String session, @Body IssueData data);
}
