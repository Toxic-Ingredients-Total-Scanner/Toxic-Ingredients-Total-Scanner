package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tits.tits_mobile.HttpHandler.HttpPostRequest;
import com.tits.tits_mobile.HttpHandler.HttpPutRequest;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;

public class productWithoutIndgredients extends AppCompatActivity {

    List<String> ingredientsArr;
    HashMap<String, String> ingredientsMap;
    EditText ingredientsEditText;
    Button sendBtn;
    Product prod;
    ArrayList<Ingredient> ingModelList;
    String json;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_product_without_indgredients);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        ingredientsEditText = findViewById(R.id.ingredients);
        sendBtn = findViewById(R.id.sendBtn);

        prod = (Product) getIntent().getExtras().getSerializable("prod");

        ingredientsArr = new ArrayList<>();
        ingredientsMap = new HashMap<>();
        ingModelList = new ArrayList<>();

        sendBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String field = ingredientsEditText.getText().toString();
                ingredientsArr = Arrays.asList(field.split("\\s*,\\s*"));

                for (String item : ingredientsArr){
                    ingredientsMap.put(item, null); //null should be replaced with translated item
                    ingModelList.add(new Ingredient(item));
                }

                prod.setIngredients(ingModelList);

                try {
                    json = new ObjectMapper().writeValueAsString(prod);
                } catch (JsonProcessingException e) {
                    e.printStackTrace();
                    json = "JsonProcessingException";
                }

                System.out.println(json);
                HttpPutRequest putRequest = new HttpPutRequest();
                try {
                    putRequest.execute(json);
                } catch (Exception e) {
                    e.printStackTrace();
                }
                startActivity(new Intent(productWithoutIndgredients.this, MainActivity.class));
            }
        });
    }
}
