package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;


import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tits.tits_mobile.HttpHandler.HttpPostRequest;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;


import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;



public class newProductForm extends AppCompatActivity {

    String EAN;
    EditText eanEditText;
    EditText brandEditText;
    EditText productNameEditText;
    EditText ingredientsEditText;
    Button sendBtn;
    List<String> ingredientsArr;
    HashMap<String, String> ingredientsMap;
    Product newProd;
    Ingredient newIng;
    ArrayList<Ingredient> ingModelArr;
    String json;
    String myUrl = "http://217.182.79.249/api/Products/fullRequest";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_product_form);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        eanEditText = findViewById(R.id.ean);
        brandEditText = findViewById(R.id.brand);
        productNameEditText = findViewById(R.id.productName);
        ingredientsEditText = findViewById(R.id.ingredients);
        sendBtn = findViewById(R.id.sendBtn);

        ingredientsArr = new ArrayList<>();
        ingModelArr = new ArrayList<>();
        ingredientsMap = new HashMap<>();

        EAN = getIntent().getStringExtra("EAN");
        eanEditText.setText(EAN);

        sendBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String field = ingredientsEditText.getText().toString();
                ingredientsArr = Arrays.asList(field.split("\\s*,\\s*"));

                for (String item : ingredientsArr){
                    ingredientsMap.put(item, null); //null should be replaced with translated item
                    ingModelArr.add(new Ingredient(item));
                }



                newProd = new Product();
                newIng = new Ingredient();

                newProd.setGtin(EAN);
                newProd.setBrand(brandEditText.getText().toString());
                newProd.setProductName(productNameEditText.getText().toString());
                newProd.setIngredients(ingModelArr);

                try {
                    json = new ObjectMapper().writeValueAsString(newProd);
                } catch (JsonProcessingException e) {
                    e.printStackTrace();
                    json = "JsonProcessingException";
                }

                String result;
                HttpPostRequest postRequest = new HttpPostRequest();
                try {
                    result = postRequest.execute(json).get();
                    System.out.println(result);
                } catch (Exception e) {
                    e.printStackTrace();
                }
                startActivity(new Intent(newProductForm.this, MainActivity.class));

            }
        });


    }
}
