package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ListView;


import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tits.tits_mobile.models.HazardStatement;
import com.tits.tits_mobile.models.Ingredient;

import java.util.ArrayList;
import java.util.List;

public class ingredientsList extends AppCompatActivity {

    List<Ingredient> ingList;
    List<String> ingStrings;
    ListView list;
    String json;
    IngredientAdapter ingAdapter;

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

        list = findViewById(R.id.listvieww);

        ingStrings = new ArrayList<>();


        Bundle extra = getIntent().getBundleExtra("ingList");
        ingList = (ArrayList<Ingredient>) extra.getSerializable("arr");


        ArrayList<IngredientEntry> ingEntryList = new ArrayList<>();

        for(Ingredient i : ingList){
            ingStrings.add(i.getPolishName());

            if(i.getHazardStatements() != null) {
                for (HazardStatement hs : i.getHazardStatements()) {
                    if (hs.getCode().equals("X404")) {
                        ingEntryList.add(new IngredientEntry(i.getPolishName(), "plus"));
                        break;
                    } else {
                        ingEntryList.add(new IngredientEntry(i.getPolishName(), "minus"));
                        break;
                    }
                }
            }
        }


        ingAdapter = new IngredientAdapter(this, ingEntryList);
        list.setAdapter(ingAdapter);




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
