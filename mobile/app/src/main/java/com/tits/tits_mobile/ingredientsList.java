package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.List;

public class ingredientsList extends AppCompatActivity {

    List<Ingredient> ingList;
    List<String> ingStrings;
    ListView list;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ingredients_list);

        list = findViewById(R.id.listview);
        //ingList = new ArrayList<>();
        ingStrings = new ArrayList<>();

        Bundle extra = getIntent().getBundleExtra("ingList");
        ingList = (ArrayList<Ingredient>) extra.getSerializable("arr");

        for(Ingredient i : ingList){
            ingStrings.add(i.getEnglishName());
        }



        ArrayAdapter adapter = new ArrayAdapter(
                this,R.layout.list_item ,R.id.itemName, ingStrings
        );

        list.setAdapter(adapter);


    }
}
