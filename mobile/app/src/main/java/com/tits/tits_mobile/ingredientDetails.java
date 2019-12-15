package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.WindowManager;
import android.widget.TextView;

import com.tits.tits_mobile.HttpHandler.HttpGetRequest;
import com.tits.tits_mobile.models.Ingredient;

import java.util.concurrent.ExecutionException;

public class ingredientDetails extends AppCompatActivity {

    TextView ingDetails;
    String result;
    String myUrl;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ingredient_details);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        ingDetails = findViewById(R.id.ingDetails);
        String ingName = getIntent().getStringExtra("ingName");
        ingName = ingName.replace(" ", "%20");
        myUrl = "http://217.182.79.249/api/Ingredients?name=" + ingName;
        System.out.println(myUrl);

        HttpGetRequest getRequest = new HttpGetRequest(ingredientDetails.this);
        try {
            result = getRequest.execute(myUrl).get();
            ingDetails.setText(result);
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (ExecutionException e) {
            e.printStackTrace();
        }
    }
}