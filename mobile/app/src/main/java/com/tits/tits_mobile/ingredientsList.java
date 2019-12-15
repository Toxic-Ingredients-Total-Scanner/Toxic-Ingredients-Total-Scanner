package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.ContextCompat;

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
import com.tits.tits_mobile.models.HazardStatement;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.List;

public class ingredientsList extends AppCompatActivity {

    List<Ingredient> ingList;
    List<String> ingStrings;
    ListView list;
    String json;
    ImageView hazardImg;
    TextView hazardText;
    ArrayAdapter adapter;
    IngredientAdapter ingAdapter;
    boolean alreadyRecreated = false;

    public Ingredient findIngredientByName(String name) {
        for(Ingredient ingredientObj : ingList) {
            if(ingredientObj.getEnglishName().equals(name)) {
                return ingredientObj;
            }
        }
        return null;
    }

    public Ingredient findIng(String name){
        for(Ingredient ingob : ingList){
            if(ingob.getPolishName().equals(name)){
                return ingob;
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
        //hazardImg = findViewById(R.id.hazardImg);
        //ingList = new ArrayList<>();
        ingStrings = new ArrayList<>();
        //View v;

        Bundle extra = getIntent().getBundleExtra("ingList");
        ingList = (ArrayList<Ingredient>) extra.getSerializable("arr");

        for(Ingredient ing : ingList) System.out.println("Got from previous activity: " + ing.getPolishName());

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

        for(IngredientEntry ingEnt : ingEntryList){
            System.out.println(ingEnt.getmIngredientName());
            //System.out.println(ingEnt.getmIngredientHazardImage());
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
