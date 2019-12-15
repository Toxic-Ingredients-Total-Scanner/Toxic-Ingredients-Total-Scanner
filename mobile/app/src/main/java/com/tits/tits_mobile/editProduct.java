package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tits.tits_mobile.HttpHandler.HttpGetRequest;
import com.tits.tits_mobile.HttpHandler.HttpPostRequest;
import com.tits.tits_mobile.HttpHandler.HttpPutRequest;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Objects;
import java.util.concurrent.ExecutionException;

public class editProduct extends AppCompatActivity {

    Product prod;
    List<Ingredient> ingList;
    List<String> ingStrings;
    EditText eanEditText;
    EditText brandEditText;
    EditText productNameEditText;
    AutoCompleteTextView ingredientsEditText;
    ListView list;
    Button sendBtn;
    Button confirmIng;
    String json;
    ArrayList<String> autocomplete;
    String result;
    String EAN;
    ArrayList<String> reqHints;
    String[] reqHintsArr;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_edit_product);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        eanEditText = findViewById(R.id.ean);
        brandEditText = findViewById(R.id.brand);
        productNameEditText = findViewById(R.id.productName);
        ingredientsEditText = findViewById(R.id.ingredients);
        sendBtn = findViewById(R.id.sendBtn);
        confirmIng = findViewById(R.id.confirmIng);
        list = findViewById(R.id.listview);


        ingStrings = new ArrayList<>();
        autocomplete = new ArrayList<>();
        reqHints = new ArrayList<>();
        reqHintsArr = new String[]{};

        final String myUrl = "http://vps756014.ovh.net/api/Ingredients/names";

        EAN = getIntent().getStringExtra("EAN");
        prod = (Product) getIntent().getExtras().getSerializable("prod");
        if (prod != null) {
            if(prod.getIngredients() != null){
                ingList = prod.getIngredients();
            } else ingList = new ArrayList<Ingredient>();
        } else ingList = new ArrayList<Ingredient>();

        if(prod!=null){
            eanEditText.setText(prod.getGtin());
            brandEditText.setText(prod.getBrand());
            productNameEditText.setText(prod.getProductName());
        } else {
            eanEditText.setText(EAN);
        }


        HttpGetRequest getRequest = new HttpGetRequest(editProduct.this);
        try {
            result = getRequest.execute(myUrl).get();
            ObjectMapper objectMapper = new ObjectMapper();
            reqHints = objectMapper.readValue(result,objectMapper.getTypeFactory().constructCollectionType(List.class, String.class));
            System.out.println(reqHints.size());
        } catch (ExecutionException | InterruptedException | JsonProcessingException e) {
            e.printStackTrace();
        }

        if(ingList != null){
            for(Ingredient i : ingList){
                ingStrings.add(i.getPolishName());
                //System.out.println(i.getPolishName());
            }
        }


        final ArrayAdapter<String> hintAdapter = new ArrayAdapter<>(this,
                android.R.layout.simple_dropdown_item_1line, reqHints.toArray(new String[0]));

        ingredientsEditText.setAdapter(hintAdapter);


        final ArrayAdapter adapter = new ArrayAdapter<>(
                this, R.layout.list_item_edit, R.id.ingName, ingStrings
        );

        list.setAdapter(adapter);


        list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                String item = ingStrings.get(position);

                ingStrings.remove(item);
                ingList.remove(ingList.get(position));
                adapter.notifyDataSetChanged();

            }

        });

        confirmIng.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ingStrings.add(ingredientsEditText.getText().toString());
                ingredientsEditText.setText("");
                adapter.notifyDataSetChanged();
                System.out.println(ingStrings);
            }
        });

        sendBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Product editedProduct = new Product();
                ArrayList<Ingredient> ingredientsList = new ArrayList<>();

                for (String item : ingStrings){
                    ingList.add(new Ingredient(item));
                    ingredientsList.add(new Ingredient(item));
                }


                editedProduct.setBrand(brandEditText.getText().toString());
                editedProduct.setProductName(productNameEditText.getText().toString());
                editedProduct.setIngredients(ingredientsList);
                if(prod!=null) {
                    editedProduct.setId(prod.getId());
                    editedProduct.setGtin(prod.getGtin());
                } else {
                    editedProduct.setGtin(EAN);
                }

                try {
                    json = new ObjectMapper().writeValueAsString(editedProduct);
                } catch (JsonProcessingException e) {
                    e.printStackTrace();
                    json = "JsonProcessingException";
                }

                System.out.println(json);

                if(prod!=null){
                    HttpPutRequest putRequest = new HttpPutRequest();
                    try {
                        putRequest.execute(json);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                } else {
                    HttpPostRequest postRequest = new HttpPostRequest();
                    try {
                        result = postRequest.execute(json).get();
                        //System.out.println(result);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }

                startActivity(new Intent(editProduct.this, MainActivity.class));

            }
        });
    }
}
