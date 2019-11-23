package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;


import org.json.JSONArray;

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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_product_form);

        eanEditText = findViewById(R.id.ean);
        brandEditText = findViewById(R.id.brand);
        productNameEditText = findViewById(R.id.productName);
        ingredientsEditText = findViewById(R.id.ingredients);
        sendBtn = findViewById(R.id.sendBtn);

        ingredientsArr = new ArrayList<>();
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
                }
            }
        });


    }
}
