package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tits.tits_mobile.HttpHandler.HttpPutRequest;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class editProduct extends AppCompatActivity {

    Product prod;
    List<Ingredient> ingList;
    List<String> ingStrings;
    EditText eanEditText;
    EditText brandEditText;
    EditText productNameEditText;
    EditText ingredientsEditText;
    ListView list;
    Button sendBtn;
    String json;

    public Ingredient findIngredientByName(String name) {
        for(Ingredient ingredientObj : ingList) {
            if(ingredientObj.getEnglishName().equals(name)) {
                return ingredientObj;
            }
        }
        return null;
    }

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
        list = findViewById(R.id.listview);


        ingStrings = new ArrayList<>();

        prod = (Product) getIntent().getExtras().getSerializable("prod");
        ingList = prod.getIngredients();

        eanEditText.setText(prod.getGtin());
        brandEditText.setText(prod.getBrand());
        productNameEditText.setText(prod.getProductName());

        for(Ingredient i : ingList){
            ingStrings.add(i.getPolishName());
            //System.out.println(i.getPolishName());
        }

        final ArrayAdapter adapter = new ArrayAdapter<String>(
                this, R.layout.list_item_edit, R.id.ingName, ingStrings
        );

        list.setAdapter(adapter);


        list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                String item = ingStrings.get(position);
                Ingredient ingr = findIngredientByName(item);

                ingStrings.remove(item);
                ingList.remove(ingList.get(position));
                adapter.notifyDataSetChanged();

            }

        });

        sendBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Product editedProduct = new Product();
                ArrayList<Ingredient> editedIngredients = new ArrayList<>();

                String field = ingredientsEditText.getText().toString();
                List<String> ingredientsArr = Arrays.asList(field.split("\\s*,\\s*"));

                for (String item : ingredientsArr){
                    ingList.add(new Ingredient(item));
                }

                for(Ingredient i : ingList){
                    editedIngredients.add(i);
                }

                editedProduct.setBrand(brandEditText.getText().toString());
                editedProduct.setProductName(productNameEditText.getText().toString());
                editedProduct.setIngredients(editedIngredients);
                editedProduct.setId(prod.getId());
                editedProduct.setGtin(prod.getGtin());

                try {
                    json = new ObjectMapper().writeValueAsString(editedProduct);
                } catch (JsonProcessingException e) {
                    e.printStackTrace();
                    json = "JsonProcessingException";
                }

                HttpPutRequest putRequest = new HttpPutRequest();
                try {
                    putRequest.execute(json);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }
}
