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

        ArrayList<IngredientEntry> ingEntryList = new ArrayList<>();

        for(Ingredient i : ingList){
            ingStrings.add(i.getPolishName());

            if(i.getHazardStatements() != null) {
                for (HazardStatement hs : i.getHazardStatements()) {
                    if (hs.getCode().equals("X404")) {
                        ingEntryList.add(new IngredientEntry(i.getPolishName(), "kciuk"));
                    } else {
                        ingEntryList.add(new IngredientEntry(i.getPolishName(), "skull"));
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




//        ArrayAdapter adapter = new ArrayAdapter(
//                this,R.layout.list_item ,R.id.itemName, ingStrings
//        );





//        adapter = new ArrayAdapter<String>(
//                this, R.layout.list_item, R.id.ingName, ingStrings
//        );
//
//
//
//        list.setAdapter(adapter);
//
//
//        for(int i=0; i<list.getCount(); i++){
//            list.getItemAtPosition(1);
//            View v = list.getAdapter().getView(i, null, null);
//            hazardImg = (ImageView) v.findViewById(R.id.hazardImg);
//            hazardText = (TextView) v.findViewById(R.id.ingName);
//            //System.out.println(hazardText.getText().toString());
//            Ingredient temp = findIng(hazardText.getText().toString());
//            System.out.println(temp.getPolishName());
//            hazardImg.setImageResource(R.drawable.kciuk);
//            System.out.println(hazardImg.getMaxHeight());
//            //System.out.println(temp.getHazardStatements().);
//
////            ArrayList<HazardStatement> hzrd = temp.getHazardStatements();
////            for(HazardStatement hz : hzrd){
////                System.out.println(hz.getCode());
////            }
//
//            if(temp.getHazardStatements() != null) {
//                for(HazardStatement hs : temp.getHazardStatements()){
//                    if(hs.getCode().equals("X404")){
//                        hazardImg.setImageResource(R.drawable.kciuk);
//                        //hazardImg.setImageDrawable(ContextCompat.getDrawable(this, R.drawable.kciuk));
//                        System.out.println("found x404");
//
//
//                        list.invalidateViews();
////                        v.refreshDrawableState();
////                        getWindow().getDecorView().findViewById(android.R.id.content).invalidate();
//
//
//
//                    } else hazardImg.setImageResource(R.drawable.skull);
//                }
//            }
//            adapter.notifyDataSetChanged();
//            list.setAdapter(adapter);
//
//
//        }


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
