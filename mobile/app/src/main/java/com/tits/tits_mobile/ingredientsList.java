package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.List;

public class ingredientsList extends AppCompatActivity {

    List<Ingredient> ingList;
    List<String> ingStrings;
    ListView list;
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
        setContentView(R.layout.activity_ingredients_list);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        list = findViewById(R.id.listview);
        //ingList = new ArrayList<>();
        ingStrings = new ArrayList<>();

        Bundle extra = getIntent().getBundleExtra("ingList");
        ingList = (ArrayList<Ingredient>) extra.getSerializable("arr");

        for(Ingredient i : ingList){
            ingStrings.add(i.getPolishName());
        }


//        ArrayAdapter adapter = new ArrayAdapter(
//                this,R.layout.list_item ,R.id.itemName, ingStrings
//        );

        final ArrayAdapter adapter = new ArrayAdapter<String>(
                this, R.layout.list_item, R.id.ingName, ingStrings
        );

        list.setAdapter(adapter);

        list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                String item = ingStrings.get(position);
                Ingredient ingr = findIngredientByName(item);

                try {
                    json = new ObjectMapper().writeValueAsString(ingr);
                } catch (JsonProcessingException e) {
                    e.printStackTrace();
                }

                startActivity(new Intent(ingredientsList.this, ingredientDetails.class).putExtra("ingName", item));
            }

        });

    }


}
