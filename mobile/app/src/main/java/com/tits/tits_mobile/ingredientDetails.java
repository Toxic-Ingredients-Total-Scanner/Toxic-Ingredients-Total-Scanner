package com.tits.tits_mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.WindowManager;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.squareup.picasso.Picasso;
import com.tits.tits_mobile.HttpHandler.HttpGetRequest;
import com.tits.tits_mobile.models.HazardStatement;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.concurrent.ExecutionException;

public class ingredientDetails extends AppCompatActivity {

    ImageView structureImage;
    TextView polishName;
    TextView englishName;
    TextView molecularFormula;
    TextView pubchemUrl;
    TextView wikiUrl;
    ListView hazardStatementsList;
    String result;
    String myUrl;


    ArrayList<HazardStatement> hazardStatements;
    List<String> hazardStatementsString;
    HashMap<Character, String> polishChars;

    Ingredient ing;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ingredient_details);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        structureImage = findViewById(R.id.structureImage);
        polishName = findViewById(R.id.polishName);
        englishName = findViewById(R.id.englishName);
        molecularFormula = findViewById(R.id.molecularFormula);
        pubchemUrl = findViewById(R.id.pubchemUrl);
        wikiUrl = findViewById(R.id.wikiUrl);
        hazardStatementsList = findViewById(R.id.hazardStatementsList);

        polishChars = new HashMap<>();
        polishChars.put('ą', "%C4%85");
        polishChars.put('ć', "%C4%87");
        polishChars.put('ę', "%C4%99");
        polishChars.put('ł', "%C5%82");
        polishChars.put('ń', "%C5%84");
        polishChars.put('ó', "%C3%B3");
        polishChars.put('ś', "%C5%9B");
        polishChars.put('ż', "%C5%BC");
        polishChars.put('ź', "%C5%BA");

        String ingName = getIntent().getStringExtra("ingName");
        ingName = ingName.replace(" ", "%20");
        for (int i = 0; i < ingName.length(); i++) {
            char ch = ingName.charAt(i);
            if (polishChars.containsKey(ch)) {
                ingName = ingName.replace(String.valueOf(ch), polishChars.get(ch));
            }
        }

            myUrl = "http://217.182.79.249/api/Ingredients?name=" + ingName;
            System.out.println(myUrl);

            HttpGetRequest getRequest = new HttpGetRequest(ingredientDetails.this);
            try {
                result = getRequest.execute(myUrl).get();
                ing = new ObjectMapper().configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false).readValue(result, Ingredient.class);

                Picasso.get().load(ing.getStrucutreImageUrl()).into(structureImage);
                polishName.setText(ing.getPolishName());
                englishName.setText(ing.getEnglishName());
                molecularFormula.setText(ing.getMolecularFormula());
                pubchemUrl.setText(ing.getPubChemUrl());
                wikiUrl.setText(ing.getWikiUrl());

                hazardStatements = ing.getHazardStatements();
                hazardStatementsString = new ArrayList<>();

                for (HazardStatement hzrd : hazardStatements) {
                    hazardStatementsString.add(hzrd.getCode() + " - " + hzrd.getDescriptionPolish());
                }
                System.out.println(hazardStatementsString);

                final ArrayAdapter adapter = new ArrayAdapter<>(
                        this, R.layout.list_item_hazard_details, R.id.hazardDetails, hazardStatementsString
                );

                hazardStatementsList.setAdapter(adapter);

            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (ExecutionException e) {
                e.printStackTrace();
            } catch (JsonMappingException e) {
                e.printStackTrace();
            } catch (JsonProcessingException e) {
                e.printStackTrace();
            }

    }
}