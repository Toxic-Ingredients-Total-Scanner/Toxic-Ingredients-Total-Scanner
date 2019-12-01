package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;

public class productWithoutIndgredients extends AppCompatActivity {

    List<String> ingredientsArr;
    HashMap<String, String> ingredientsMap;
    EditText ingredientsEditText;
    Button sendBtn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_product_without_indgredients);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        ingredientsEditText = findViewById(R.id.ingredients);
        sendBtn = findViewById(R.id.sendBtn);

        ingredientsArr = new ArrayList<>();
        ingredientsMap = new HashMap<>();

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
